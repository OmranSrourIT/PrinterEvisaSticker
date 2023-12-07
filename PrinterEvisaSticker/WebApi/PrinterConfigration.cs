using GET.Printers.Vi1200;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrinterEvisaSticker.WebApi
{
    public class PrinterConfigration
    { 
        public CONNECTION_TYPE ConnectionType { get; internal set; }
        public string PrinterName { get; set; }
        public string PrinterNumber { get; set; }
    }
}
