using BookDeliveryCore;

namespace BookDeliverySystemAPI.Interfaces
{
    public interface IClientRepository
    {
        public void InsertClient(string username, string firstname, string lastname, string address, string postalcode, string phonenumber);

        public List<BookDeliveryCore.Item> ShowShopItems();

        public string InsertOrderByCity(ShopForm data);

        public void InsertOrderUpdate(OrderUpdate data);
        public void InsertOrderReviewUpdate(OrderUpdateReview data);
        public void updateReward(OrderUpdateReward data);

    }
}
