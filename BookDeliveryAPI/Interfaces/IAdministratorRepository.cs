namespace BookDeliverySystemAPI.Interfaces
{
    public interface IAdministratorRepository
    {
        public void DeleteUser(string username);
        public void ChangeClientRole(string username, string role);
        public void ChangeAdministratorRole(string username, string NewRole);
    }
}
