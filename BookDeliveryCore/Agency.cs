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
        public int? COURIER_ID { get; set; }
        public bool? COURIER_AVAILABLE { get; set; }
        public string? REVIEW { get; set; }
    }
}
