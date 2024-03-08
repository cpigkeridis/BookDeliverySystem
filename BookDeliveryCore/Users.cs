using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookDeliveryCore
{
    public class Users
    {
        public long? SR_NO { get; set; }
        public string? USERNAME { get; set; }
        public string? ROLE { get; set;}
        public bool ENABLE { get; set; }
        public long? AGENCY_ID { get; set; }
        public string? VEHICLE_NO { get; set; }
        public int? STATUS { get; set; }
        public string? FIRSTNAME { get; set; }
        public string? LASTNAME { get; set; }
        public string? ADDRESS { get; set; }
        public string? POSTAL_CODE { get; set; }
        public string? PHONE_NUMBER { get; set; }
        public string? CURRENT_LOCATION { get; set; }
    }
}
