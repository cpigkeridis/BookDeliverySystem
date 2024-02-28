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
        public long? CLIENT_ID { get; set; }
        public long? COURIER_ID { get; set; }
        public long? ADMINISTRATOR_ID { get; set; }
        public string? ROLE { get; set; }
    }
}
