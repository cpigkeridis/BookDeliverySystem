﻿using BookDeliveryCore;

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
        public void UpdateUserEnableStatus(string username, string enable);
        public Client GetClientByUsername(string username);
        public Courier GetCourierByUsername(string username);
        public Administrator GetAdministratorByUsername(string username);
        public List<Agency> GetAgencies();
        public Users GetUser(string username, string role);
        public void EditClient(string username, string firstname, string lastname, string address, string postalcode, string phonenumber, bool enable, string NewRole);
        public void EditAdministrator(string username, string firstname, string lastname, string address, string postalcode, string phonenumber, bool enable, string NewRole);
        public void EditCourier(string username, string? agencyId, string? vehicleNo, string firstname, string lastname, string address, string postalcode, string phonenumber, bool enable, string NewRole);
        public List<Orders> GetOrderByUserName(string ClientUsername);

        public List<Orders> GetOrderByCourierUserName(string CourierUsername);
        public List<Orders> GetOrderByAgencyID(int AgencyID);

    }
}
