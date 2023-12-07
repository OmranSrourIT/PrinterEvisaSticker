using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GET.Printers.Vi1200
{
    public delegate void PrinterEventHandler<T>(Vi1200Printer printer, T args);

    public abstract class Vi1200Printer
    {
        public event PrinterEventHandler<Vi1200Status> PrinterStatusUpdated = new PrinterEventHandler<Vi1200Status>((x, y) => { });
        public event PrinterEventHandler<Vi1200PrintingOrder> ConfigureNewPrintOrder = new PrinterEventHandler<Vi1200PrintingOrder>((x, y) => { });
        public event PrinterEventHandler<Vi1200PrintingOrder> OrderStatusUpdated = new PrinterEventHandler<Vi1200PrintingOrder>((x, y) => { });
        public Vi1200Status Status { get; protected set; }
        public CONNECTION_TYPE ConnectionType { get; protected set; }
        public string PrinterName { get; protected set; }
        public int PrinterNumber { get; protected set; }
        bool IsStarted { get; set; } = false;

        public string WindowsPrinterName { get; private set; } = "GETGroup Vi1200";

        public void SetWindowsPrinterName(string windowsPrinterName)
        {
            WindowsPrinterName = windowsPrinterName;
        }
        public static API_RESULT GetPrinters(CONNECTION_TYPE connectionType, out List<Vi1200Printer> printers)
        {
            var result = API_RESULT.RESULT_OK;
            printers = new List<Vi1200Printer>();

            if (connectionType == CONNECTION_TYPE.NETWORK)
            {
                result=Vi1200PrinterEthernet.GetPrintersList(out List<Vi1200Printer>EthernetPrinters);
                printers.AddRange(EthernetPrinters);
            }
            else if (connectionType == CONNECTION_TYPE.USB)
            {
                result=Vi1200PrinterUsb.GetPrinters(out List<Vi1200Printer> USBPrinters);
                printers.AddRange(USBPrinters);
            }
            else
            {
                result = Vi1200PrinterUsb.GetPrinters(out List<Vi1200Printer> USBPrinters);
                result = Vi1200PrinterUsb.GetPrinters(out List<Vi1200Printer> EthernetPrinters); 
                printers.AddRange(USBPrinters);
                printers.AddRange(EthernetPrinters);

            }

            return result;
        }

        internal void ExecuteJob(Vi1200PrintingOrder vi1200PrintingOrder)
        {
            PrintDocument document = new PrintDocument();


            document.PrintPage += (x, args) =>
            {
                System.Drawing.Image img = vi1200PrintingOrder.FirstPage;

                args.Graphics.DrawImage(img, new Point(0, 0));

            };
            document.EndPrint += (x, args) =>
            {
                //End Print
            };
            var printerName = WindowsPrinterName;
            Print(document, printerName, vi1200PrintingOrder);
        }
        void Print(PrintDocument document, string printerName, Vi1200PrintingOrder vi1200PrintingOrder)
        {
            var printerSettings = new PrinterSettings { PrinterName = printerName };

            var pageSettings = new PageSettings(printerSettings) { };
            document.PrinterSettings = printerSettings;
            //document.DefaultPageSettings.PaperSize = new PaperSize() { Width = 827, Height = 1169 };//A4 Size
            if (vi1200PrintingOrder.PaperSize != null && vi1200PrintingOrder.PaperSize.Width != 0 && vi1200PrintingOrder.PaperSize.Height != 0)
                document.DefaultPageSettings.PaperSize = vi1200PrintingOrder.PaperSize;
            document.DefaultPageSettings = pageSettings;
            document.PrintController = new StandardPrintController();

            document.DefaultPageSettings.Landscape = false;
            if (document.PrinterSettings.IsValid)
            {
                document.Print();

                //this.HasPrinterJob = true;
            }

        }

        public void Start()
        {
            IsStarted = true;
           FireNewOrderEvent();

        }

        public void Stop()
        {
            IsStarted = false;
            

        }
        //public abstract bool OpenConnection();
        //public abstract bool MoveToCameraCommand();
        //public abstract bool MoveToPrintCommand();
        //public abstract bool CaptureImage(out byte[] image);
        //public abstract List<string> GetWarningErrors();
         protected abstract bool GetPrinterStatus(out Vi1200Status status);
        //public abstract bool GetInkLevels(out ViInkLevel inkLevels);
        //public abstract bool LedOnOff(bool isLedOn);
        //public abstract bool CloseConnection();

        private Thread JobStatusMonitorThread = null;
        public Vi1200PrintingOrder PrintingOrder { get; set; }

        object thisLock = new object();
        protected void FireNewOrderEvent()
        {
            if (JobStatusMonitorThread == null || !JobStatusMonitorThread.IsAlive)
            {
                JobStatusMonitorThread = new Thread(
                    () =>
                    {
                        lock (thisLock)
                        {
                            while (IsStarted)
                            {

                                var result = this.GetPrinterStatus(out Vi1200Status Vi1200Status);

                                Status = Vi1200Status;

                                if (CanAcceptNewOrder())
                                {
                                    PrintingOrder = new Vi1200PrintingOrder(this);
                                    ConfigureNewPrintOrder(this, PrintingOrder);
                                    if (PrintingOrder.Status.OrderStatus ==  ORDER_STATE.CONFIGURED )
                                    {
                                        PrintingOrder.Execute();

                                    }

                                }

                                UpdateOrder();

                                PrinterStatusUpdated(this, Vi1200Status);
                                Thread.Sleep(250);

                            }

                        }
                    });

                JobStatusMonitorThread.SetApartmentState(ApartmentState.STA);
                JobStatusMonitorThread.Name = "Vi1200 Status";
                JobStatusMonitorThread.IsBackground = true;
                JobStatusMonitorThread.Start();
            }
        }

        private void UpdateOrder()
        {
            if (PrintingOrder != null &&PrintingOrder.Status.OrderStatus!= ORDER_STATE.NONE)
            {
                PrintingOrder?.UpdateState();
                OrderStatusUpdated(this, PrintingOrder);
            }
        }

        private bool CanAcceptNewOrder()
        {
            return (this.Status.MachineState == MACHINE_STATE.IDLE
                && Status.EngineState == ENGINE_STATE.READY 
                && !HasPrinterJob()
               
                && Status.MediaLoadStatus== MEDIA_LOAD_STATUS.NO_MEDIA_LOADED
                && Status.IsAuthorized);
        }

        private bool HasPrinterJob()
        {
            if (PrintingOrder == null) return false;
            else return 
                PrintingOrder.Status.OrderStatus== ORDER_STATE.CONFIGURED
                || PrintingOrder.Status.OrderStatus == ORDER_STATE.PRINTING
                

                ;
        }
    }
   

}