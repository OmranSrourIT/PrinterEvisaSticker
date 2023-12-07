using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 

namespace GET.Printers.Vi1200
{
    public class Vi1200PrintingOrder
    {
        public Vi1200Printer Printer { private set; get; }
        public string DocumentName { get; set; } = "Vi1200 Document";
        public Bitmap FirstPage { get; set; }
        public PaperSize PaperSize { get; set; }

        public Vi1200OrderStatus Status { get; internal set; } = new Vi1200OrderStatus();
        public bool Accept()
        {
            this.Status.OrderStatus= ORDER_STATE.CONFIGURED;
            return true;    
        }
        internal void UpdateState()
        {
            if (Printer.Status.MachineState == MACHINE_STATE.CANCELLING_PRINTING)
            {
                this.Status.CompletedTime = DateTime.Now;
                this.Status.HasError = true;
                this.Status.ErrorMessage = "CANCELLING_PRINTING";
                this.Status.OrderStatus = ORDER_STATE.CANCELLED;
                //Printer.HasPrinterJob = false;

            }
            else if (Printer.Status.MachineState == MACHINE_STATE.MEDIA_JAM)
            {
                this.Status.CompletedTime = DateTime.Now;
                this.Status.HasError = true;
                this.Status.ErrorMessage = "CANCELLING_PRINTING";
                this.Status.OrderStatus = ORDER_STATE.FAILED;
               // Printer.HasPrinterJob = false;

            }
            else if  (this.Status.OrderStatus!= ORDER_STATE.PRINTING && Printer.Status.MachineState == MACHINE_STATE.PRINTING)//Change 
            {
                this.Status.StartedTime = DateTime.Now;
                this.Status.OrderStatus = ORDER_STATE.PRINTING;
                //Printer.HasPrinterJob = true;
             
            }
            else if (this.Status.OrderStatus == ORDER_STATE.PRINTING && Printer.Status.MachineState == MACHINE_STATE.IDLE)//Change 
            {
                this.Status.CompletedTime = DateTime.Now;
                this.Status.OrderStatus = ORDER_STATE.FINISHED;
                //Printer.HasPrinterJob = false;

            }
        }
        internal void Execute()
        {
            this.Printer.ExecuteJob(this);
        }
        
        //internal Vi1200PrintingOrder(Vi1200Printer printer, CP55Card card)
        //{
        //    this.Printer = printer;
        //    this.Card = card;
        //}

      //  bool isNewOrder = false;
        internal Vi1200PrintingOrder(Vi1200Printer printer) 
        {
           // isNewOrder = true;
            this.Printer = printer;
            Status.OrderStatus = ORDER_STATE.CONFIGURING;
        }
    }
}
