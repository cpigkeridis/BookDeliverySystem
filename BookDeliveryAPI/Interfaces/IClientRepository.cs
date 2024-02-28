namespace BookDeliverySystemAPI.Interfaces
{
    public interface IClientRepository
    {
        public List<BookDeliveryCore.Client> GetClient();

        public void InsertClient(string username, string firstname, string lastname, string address, string postalcode, string phonenumber);
    }
}
