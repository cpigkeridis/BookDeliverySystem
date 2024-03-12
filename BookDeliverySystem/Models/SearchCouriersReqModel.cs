namespace BookDeliverySystem.Models
{
    public class SearchCouriersReqModel
    {
        public string Username { get; set; }
        public string Agency { get; set; }
        public string Vehicle { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string Role { get; set; }
        public string PhoneNumber { get; set; }
        public string Enabled { get; set; }
        public string OldRole { get; set; }
    }
}
