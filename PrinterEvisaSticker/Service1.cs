using GET.Printers.Vi1200;
using PrinterEvisaSticker.WebApi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Web.Http;
using System.Web.Http.SelfHost;

namespace PrinterEvisaSticker
{
    public partial class Service1 : ServiceBase
    {
        private System.Threading.Timer timer;
        Vi1200Printer Vi1200Printer;

        public Dictionary<string, Vi1200OrderStatus> OrderStatus = new Dictionary<string, Vi1200OrderStatus>();
        public Service1()
        {

            InitializeComponent();

            Program.lblMediaState = null;
            Program.lblEngineState = null;
            Program.lblMachineState = null;
            Program.lblTotalCount = null;
            Program.lblIsAuthorized = null;
            Program.lblC = null;
            Program.lblK = null;
            Program.lblM = null;
            Program.lblY = null;
            Program.ObjectConfig = null;
            Program.lblOrderStatus = null;
            Program.lblCompleted = null;
            Program.lblStarted = null;
            Program.lblError = null;
            Program.isClosing = false;
            Program.PrintingOrders = new List<Bitmap>();
            Program.ObjectConfig = null;
            Program.ObjectConfig = GetPrinter();
            UnRegisterVi100PrinterEvents();
            RegisterVi100PrinterEvents();

        }

        void RegisterVi100PrinterEvents()
        {
            if (Vi1200Printer != null)
            {
                Vi1200Printer.Start();
                Vi1200Printer.PrinterStatusUpdated += printer_PrinterStatusUpdated;
                Vi1200Printer.ConfigureNewPrintOrder += printer_ConfigureNewPrintOrder;
                Vi1200Printer.OrderStatusUpdated += printerOrderStatusUpdated;
            }
        }
        void UnRegisterVi100PrinterEvents()
        {
            if (Vi1200Printer != null)
            {
                Vi1200Printer.PrinterStatusUpdated -= printer_PrinterStatusUpdated;
                Vi1200Printer.ConfigureNewPrintOrder -= printer_ConfigureNewPrintOrder;
                Vi1200Printer.OrderStatusUpdated -= printerOrderStatusUpdated;
            }
        }

        private void printerOrderStatusUpdated(Vi1200Printer printer, Vi1200PrintingOrder args)
        {

            if (args != null &&  !Program.isClosing)
            {
               

                if (args.Status.OrderStatus != ORDER_STATE.CONFIGURING)
                {
                    Program.lblOrderStatus = args.Status.OrderStatus.ToString();
                    Program.lblCompleted = args.Status.CompletedTime.ToString();
                    Program.lblStarted = args.Status.StartedTime.ToString();
                    Program.lblError = args.Status.HasError ? Convert.ToString(args.Status.ErrorMessage.ToString()) : "";
                }

            }
        }
        private void printer_ConfigureNewPrintOrder(Vi1200Printer printer, Vi1200PrintingOrder args)
        {
          
            Console.WriteLine("Printer can configure order");
            if (Program.PrintingOrders.Count > 0)
            {
                args.FirstPage = Program.PrintingOrders[0];
                var result = args.Accept();

                Program.PrintingOrders.Remove(args.FirstPage);
                Console.WriteLine("Order is Configured");

            }
        }
        private void printer_PrinterStatusUpdated(Vi1200Printer printer, Vi1200Status args)
        {
             
            if (args != null && !Program.isClosing)
            { 
                Program.lblMediaState = args.MediaLoadStatus.ToString();
                Program.lblEngineState = args.EngineState.ToString();
                Program.lblMachineState = args.MachineState.ToString();
                Program.lblTotalCount = args.TotalPrintCount.ToString();
                Program.lblIsAuthorized = args.IsAuthorized.ToString();
                Program.lblC = args.InkLevels.Cayan.ToString();
                Program.lblK = args.InkLevels.Black.ToString();
                Program.lblM = args.InkLevels.Magenta.ToString();
                Program.lblY = args.InkLevels.Yellow.ToString();


            }
        }
        public PrinterConfigration GetPrinter()
        {
            PrinterConfigration obj = new PrinterConfigration();

            var message = string.Empty;
            try
            {
                var result = Vi1200Printer.GetPrinters(CONNECTION_TYPE.USB, out List<Vi1200Printer> printers);
                
                if (printers.Count > 0)
                {
                    foreach (var item in printers)
                    {
                        string SerialNUmber = Convert.ToString(item.PrinterName.Split(' ')[2]);
                        obj.ConnectionType = item.ConnectionType;
                        obj.PrinterName = item.PrinterName.Split(' ')[0] + " " + item.PrinterName.Split(' ')[1];
                        obj.PrinterNumber = SerialNUmber;
                        Vi1200Printer = printers[0];
                    }

                }

            }
            catch (Exception ex)
            {
                var ErrorMessagea = ex.Message;
                Logger.WriteLog("خطأ اثناء تعريف الطابعة :" +ErrorMessagea);

            }


            return obj;
        }
        protected override void OnStart(string[] args)
        {
            try
            {  
                string ServiceURLPath = ConfigurationManager.AppSettings["UrlSelfHosting"]; // url port 9999

                var config = new HttpSelfHostConfiguration(ServiceURLPath);
                config.EnableCors();
                config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));

                config.MaxReceivedMessageSize = 2147483647; // use config for this value


                config.Routes.MapHttpRoute(
                    name: "API",
                    routeTemplate: "api/{controller}/{action}/{id}",
                    defaults: new { controller = "Home", id = RouteParameter.Optional }
                    );

                HttpSelfHostServer server = new HttpSelfHostServer(config);
                server.OpenAsync().Wait();

                //WriteToFile("api http://localhost:1200 in done to calling" + DateTime.Now);

               timer = new System.Threading.Timer(TimerCallback, null, TimeSpan.Zero, TimeSpan.FromSeconds(3));
            }
            catch (Exception ex)
            {
                var stackTrace = new StackTrace(ex, true); 
                Logger.WriteLog("ErrorMessage" + Environment.NewLine + ex.Message + Environment.NewLine + stackTrace);

            }

        }

        protected override void OnStop()
        {
            timer?.Change(Timeout.Infinite, Timeout.Infinite);
            timer?.Dispose();
        }

        private void TimerCallback(object state)
        {
            Program.ObjectConfig = GetPrinter();

        }

    }

       
}
