using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookDeliveryCore
{
    public class Agency
    {
        public int? AGENCY_ID { get; set; }
        public int? REWARD_ID { get; set; }
        public string? NAME { get; set; }
        public string? CITY { get; set; }
        public string? COUNTRY { get; set; }
        public string? POSTAL_CODE { get; set; }
        //public int? COURIER_ID { get; set; }
        public string? REVIEW { get; set; }
        public string? USERNAME { get; set; }
        public string? FIRSTNAME { get; set; }
        public string? LASTNAME { get; set;}
        public string? ROLE { get; set; }
    }
    public class AgencySelectionResp
    {
        public string? AGENCY_ID { get; set; }
        public string? AGENCY_NAME { get; set; }
        public string? COURIER_USERNAME { get; set; }
        public string? COURIER_FIRSTNAME { get; set; }
        public string? COURIER_LASTNAME { get; set; }
        public string? COURIER_PHONE { get; set; }
    }
}
