namespace BookDeliverySystem.Models
{
    public class UpdateOrderModel
    {
        public string? OrderID { get; set; }
        public DateTime? EDD { get; set; } = null;
        public string? Status { get; set; }
    }
}
