using PrinterEvisaSticker.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace PrinterEvisaSticker
{
    static class Program
    {  
        public static string lblMediaState;
        public static string lblEngineState;
        public static string lblMachineState;
        public static string lblTotalCount;
        public static string lblIsAuthorized;
        public static string lblC;
        public static string lblK;
        public static string lblM;
        public static string lblY;
        public static PrinterConfigration ObjectConfig;
        public static string lblOrderStatus;
        public static string lblCompleted;
        public static string lblStarted;
        public static string lblError;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new Service1()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
