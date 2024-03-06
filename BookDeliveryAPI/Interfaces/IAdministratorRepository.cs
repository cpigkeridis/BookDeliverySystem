using BookDeliveryCore;

namespace BookDeliverySystemAPI.Interfaces
{
    public interface IAdministratorRepository
    {
        public void DeleteUser(string username);
        public void ChangeClientRole(string username, string role);
        public void ChangeAdministratorRole(string username, string NewRole);
        public void ChangeCourierRole(string username, string NewRole);
        public List<BookDeliveryCore.Client> GetClients();
        public List<BookDeliveryCore.Courier> GetCouriers();
        public List<BookDeliveryCore.Administrator> GetAdministrators();
        public void UpdateUserEnableStatus(string username, bool enable);
        public Client GetClientByUsername(string username);
        public Courier GetCourierByUsername(string username);
        public Administrator GetAdministratorByUsername(string username);
    }
}
