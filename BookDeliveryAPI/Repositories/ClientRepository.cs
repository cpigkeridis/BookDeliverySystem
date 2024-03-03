using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;
using Dapper;
namespace BookDeliverySystemAPI.Repositories
{
    public class ClientRepository : Interfaces.IClientRepository
    {
        ConfigManagerAppSettings.IConfigManager _Configuration;
        public ClientRepository(ConfigManagerAppSettings.IConfigManager oConfig)
        {
            _Configuration = oConfig;
        }
        public List<BookDeliveryCore.Client> GetClient()
        {

            //"Data Source=DESKTOP-GG5B8TO\\SQLEXPRESS;Initial Catalog=BookingDeliverySystem;Integrated Security=True;"

            SqlConnection oCnn = new SqlConnection(_Configuration.APICONSTRING);
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

            SqlConnection oCnn = new SqlConnection(_Configuration.APICONSTRING);
            oCnn.Open();
            try
            {
                var values = new
                {

                    USERNAME = username,
                    FIRSTNAME = firstname,
                    LASTNAME = lastname,
                    ADDRESS = address,
                    POSTALCODE = postalcode,
                    PHONENUMBER = phonenumber

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
