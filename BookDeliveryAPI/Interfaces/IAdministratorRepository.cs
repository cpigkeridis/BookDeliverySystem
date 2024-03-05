namespace BookDeliverySystemAPI.Interfaces
{
    public interface IAdministratorRepository
    {
        public void DeleteUser(string username);
        public void ChangeClientRole(string username, string role);
        public void ChangeAdministratorRole(string username, string NewRole);
        public void ChangeCourierRole(string username, string NewRole);
        public List<BookDeliveryCore.Client> GetClient();
        public List<BookDeliveryCore.Courier> GetCourier();
        public List<BookDeliveryCore.Administrator> GetAdministrator();
        public void UpdateUserEnableStatus(string username, bool enable);
    }
}
