﻿using System.Data;
using System.Data.SqlClient;
using Dapper;
namespace BookDeliverySystemAPI.Repositories
{
    public class ClientRepository : Interfaces.IClientRepository
    {
        public List<BookDeliveryCore.Client> GetClient()
        {

            SqlConnection oCnn = new SqlConnection("Data Source=DESKTOP-GG5B8TO\\SQLEXPRESS;Initial Catalog=BookingDeliverySystem;Integrated Security=True;");
            oCnn.Open();
            try
            {
                var values = new
                {

                };
                List<BookDeliveryCore.Client> obj = oCnn.Query<BookDeliveryCore.Client>("[dbo].[SP_GETCLIENT]", values, commandType: System.Data.CommandType.StoredProcedure).ToList();
                return obj;

            }
            finally
            {
                oCnn.Close();
                oCnn.Dispose();
            }
            //a
        }

        public void InsertClient(string username, string firstname, string lastname, string address, string postalcode, string phonenumber)
        {

            SqlConnection oCnn = new SqlConnection("Data Source=DESKTOP-GG5B8TO\\SQLEXPRESS;Initial Catalog=BookingDeliverySystem;Integrated Security=True;");
            oCnn.Open();
            try
            {
                var values = new
                {

                    USERNAME = username,
                    FIRSTNAME = firstname,
                    LASTNAME = lastname,
                    ADDRESS = address,
                    POSTAL_CODE = postalcode,
                    PHONE_NUMBER = phonenumber

                };
                oCnn.ExecuteScalar("[dbo].[SP_INSERT_CLIENT]", values, commandType: System.Data.CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                oCnn.Close();
                oCnn.Dispose();
            }
            //TODO RETURN STATUS

        }


    }
}
