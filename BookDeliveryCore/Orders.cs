using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookDeliveryCore
{
    public class Orders
    {
        public string? ORDER_ID { get; set; }
        public string? CLIENT_USERNAME { get; set; }
        public string? AGENCY_ID { get; set; }
        public string? COURIER_USERNAME { get; set; }
        public string? STATUS { get; set; }
        public DateTime ORDERDATE { get; set; }
        public DateTime DISPATCHDATE { get; set; }
        public DateTime DELIVEREDDATE { get; set; }
        public int REVIEW { get; set; }
        public string? REVIEW_COMMENTS { get; set; }
    }
}
