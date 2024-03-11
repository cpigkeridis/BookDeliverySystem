namespace BookDeliverySystemAPI.Interfaces
{
    public interface IClientRepository
    {
        public void InsertClient(string username, string firstname, string lastname, string address, string postalcode, string phonenumber);

        public List<BookDeliveryCore.Item> ShowShopItems();

        public BookDeliveryCore.AgencySelectionResp GetAgencySelectionResp(string city);
    }
}
