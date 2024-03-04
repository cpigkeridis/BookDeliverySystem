using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using Dapper;
namespace BookDeliverySystemAPI.Repositories
{
    public class AdministratorRepository : Interfaces.IAdministratorRepository
    {
        ConfigManagerAppSettings.IConfigManager _Configuration;
        public AdministratorRepository(ConfigManagerAppSettings.IConfigManager oConfig)
        {
            _Configuration = oConfig;
        }
        public void DeleteUser(string username)
        {
            SqlConnection oCnn = new SqlConnection(_Configuration.APICONSTRING);
            oCnn.Open();
            try {

                var values = new
                {
                    USERNAME = username
                };
                oCnn.Execute("[dbo].[SP_DELETE_USER]", values, commandType: System.Data.CommandType.StoredProcedure);

            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                oCnn.Close();
                oCnn.Dispose();
            }

        }

        public void ChangeClientRole(string username,string NewRole)
        {
            SqlConnection oCnn = new SqlConnection(_Configuration.APICONSTRING);
            oCnn.Open();
            try
            {
                var values = new
                {
                    USERNAME = username,
                    NEWROLE = NewRole
                };
                oCnn.Execute("[dbo].[SP_CHANGE_CLIENT_ROLE]", values, commandType: System.Data.CommandType.StoredProcedure);

            }catch(Exception ex)
            {
                throw new Exception (ex.Message);
            }
            finally 
            {
                oCnn.Close();
                oCnn.Dispose();
            }
        }

        public void ChangeAdministratorRole(string username, string NewRole)
        {
            SqlConnection oCnn = new SqlConnection(_Configuration.APICONSTRING);
            oCnn.Open();
            try
            {
                var values = new
                {
                    USERNAME = username,
                    NEWROLE = NewRole
                };
                oCnn.Execute("[dbo].[SP_CHANGE_ADMINISTRATOR_ROLE]", values, commandType: System.Data.CommandType.StoredProcedure);

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
        }
    }
}
