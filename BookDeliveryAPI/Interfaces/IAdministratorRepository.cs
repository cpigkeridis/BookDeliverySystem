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
        public List<Agency> GetAgencies();
        public Users GetUser(string username, string role);
        public void EditClient(string username, string firstname, string lastname, string address, string postal_code, string phonumber, bool enable);
        public void EditAdministrator(string username, string firstname, string lastname, string address, string postal_code, string phonumber, bool enable);
        public void EditCourier(string username, string agency_id, string vehicle_id, string status, string firstname, string lastname, string address, string postalcode, string phonenumber, string currentlocation, bool enable);
    }
}
