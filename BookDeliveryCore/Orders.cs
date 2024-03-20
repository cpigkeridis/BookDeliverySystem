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
        public string? CLIENT_FIRSTNAME { get; set; }
        public string? CLIENT_LASTNAME { get; set; }
        public string? CLIENT_ADDRESS { get; set; }
        public string? CLIENT_POSTALCODE { get; set; }
        public string? CLIENT_PHONE { get; set; }
        public string? CLIENT_CITY { get; set; }

        public string? AGENCY_ID { get; set; }
        public string? AGENCY_NAME { get; set; }
        public string? COURIER_USERNAME { get; set; }
        public string? COURIER_FIRSTNAME { get; set; }
        public string? COURIER_LASTNAME { get; set; }
        public string? COURIER_PHONE { get; set; }
        public string? STATUS { get; set; }
        public DateTime? ORDERDATE { get; set; }
        public DateTime? DISPATCHDATE { get; set; }
        public DateTime? ESTIMATE_DT { get; set; }    
        public DateTime? DELIVEREDDATE { get; set; }
        public int? REVIEW { get; set; }
        public string? REVIEW_COMMENTS { get; set; }
    }

    public class OrderUpdate
    {
        public string OrderID { get; set; }
        public DateTime? EDD { get; set; }
        public string Status { get; set; }
        public string Role { get; set; }
    }

    public class OrderUpdateReview
    {
        public string OrderID { get; set; }
        public int Review { get; set; }
        public string ReviewComments { get; set; }
        public string Role { get; set; }
    }

    public class OrderUpdateReward
    {
        public string AgencyName { get; set; }
        public string OrderID { get; set; }
        public int Review { get; set; }
        public string Role { get; set; }
    }
}
