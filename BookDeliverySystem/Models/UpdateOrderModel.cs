namespace BookDeliverySystem.Models
{
    public class UpdateOrderModel
    {
        public string? OrderID { get; set; }
        public DateTime? EDD { get; set; } = null;
        public string? Status { get; set; }
    }

    public class UpdateOrderReviewModel
    {
        public string? OrderID { get; set; } 

        public int? Review { get; set;}
        public string? ReviewComments { get; set; }
    }

    public class UpdateOrderRewardModel
    {
        public float? totalPrice { get; set; }
        public string? OrderID { get; set; }

        public string? AgencyName { get; set; }

        public int? Review { get; set; }
    }
}
