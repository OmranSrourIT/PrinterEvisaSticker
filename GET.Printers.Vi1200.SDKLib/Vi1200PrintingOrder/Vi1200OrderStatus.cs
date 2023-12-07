using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GET.Printers.Vi1200
{
    public enum ORDER_STATE
    {
        NONE,
        CONFIGURING,
        CONFIGURED,
        PRINTING,
        CANCELLED,
        FINISHED,
        FAILED,
       
    }
    public class Vi1200OrderStatus
    {

        public DateTime StartedTime { get; set; }
        public DateTime CompletedTime { get; set; }

        public ORDER_STATE OrderStatus { get; set; }

        public bool HasError { get; set; }
        public string ErrorMessage { get; set; }
    }
}
