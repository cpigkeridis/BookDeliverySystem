namespace BookDeliverySystem.Models
{
    public class RewardModel
    {
        public int REVIEW_SCORE { get; set; }
        public string REWARD { get; set; }
        public int ORDER_ID { get; set; }
        public decimal TOTAL_REWARD { get; set; }
    }
}
