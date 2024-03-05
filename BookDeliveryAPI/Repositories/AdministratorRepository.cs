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

        public void ChangeCourierRole(string username, string NewRole) 
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
                oCnn.Execute("[dbo].[SP_CHANGE_COURIER_ROLE]", values, commandType: System.Data.CommandType.StoredProcedure);
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

        public List<BookDeliveryCore.Client> GetClient()
        {

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
        }

        public List<BookDeliveryCore.Courier> GetCourier()
        {
            SqlConnection oCnn = new SqlConnection(_Configuration.APICONSTRING);
            oCnn.Open();
            try
            {
                var values = new { };
                List<BookDeliveryCore.Courier> obj = oCnn.Query<BookDeliveryCore.Courier>("[dbo].[SP_GETCOURIER]", values, commandType: System.Data.CommandType.StoredProcedure).ToList();
                return obj;
            }catch (Exception ex) 
            {
                throw new Exception(ex.Message); 
            }
            finally
            {
                oCnn.Close();
                oCnn.Dispose();
            }
        }

        public List<BookDeliveryCore.Administrator> GetAdministrator()
        {
            SqlConnection oCnn = new SqlConnection(_Configuration.APICONSTRING);
            oCnn.Open();
            try
            {
                var values = new { };
                List<BookDeliveryCore.Administrator> obj = oCnn.Query<BookDeliveryCore.Administrator>("[dbo].[SP_GETADMINISTRATOR]", values, commandType: System.Data.CommandType.StoredProcedure).ToList();
                return obj;
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

         public void UpdateUserEnableStatus(string username, bool enable)
         {
            SqlConnection oCnn = new SqlConnection(_Configuration.APICONSTRING);
            oCnn.Open();
            try
            {
                var values = new 
                {
                    USERNAME = username,
                    ENABLE = enable
                };

                oCnn.Execute("[dbo].[SP_UPDATE_USER_ENABLE_STATUS]", values, commandType: System.Data.CommandType.StoredProcedure);
            }catch (Exception ex)
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
