using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using BookDeliveryCore;
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

        public List<BookDeliveryCore.Client> GetClients()
        {

            SqlConnection oCnn = new SqlConnection(_Configuration.APICONSTRING);
            oCnn.Open();
            try
            {
                var values = new
                {

                };
                List<BookDeliveryCore.Client> obj = oCnn.Query<BookDeliveryCore.Client>("[dbo].[SP_GETCLIENTS]", values, commandType: System.Data.CommandType.StoredProcedure).ToList();
                return obj;

            }
            finally
            {
                oCnn.Close();
                oCnn.Dispose();
            }
        }

        public List<BookDeliveryCore.Courier> GetCouriers()
        {
            SqlConnection oCnn = new SqlConnection(_Configuration.APICONSTRING);
            oCnn.Open();
            try
            {
                var values = new { };
                List<BookDeliveryCore.Courier> obj = oCnn.Query<BookDeliveryCore.Courier>("[dbo].[SP_GETCOURIERS]", values, commandType: System.Data.CommandType.StoredProcedure).ToList();
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

        public List<BookDeliveryCore.Administrator> GetAdministrators()
        {
            SqlConnection oCnn = new SqlConnection(_Configuration.APICONSTRING);
            oCnn.Open();
            try
            {
                var values = new { };
                List<BookDeliveryCore.Administrator> obj = oCnn.Query<BookDeliveryCore.Administrator>("[dbo].[SP_GETADMINISTRATORS]", values, commandType: System.Data.CommandType.StoredProcedure).ToList();
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

        public Client GetClientByUsername(string username)
        {
            SqlConnection oCnn = new SqlConnection(_Configuration.APICONSTRING);
            oCnn.Open();
            try
            {
                var parameters = new
                {
                    Username = username
                };

                BookDeliveryCore.Client obj = oCnn.QuerySingleOrDefault<BookDeliveryCore.Client>("[dbo].[SP_GETCLIENTBYUSERNAME]",parameters,commandType: System.Data.CommandType.StoredProcedure);
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

        public Administrator GetAdministratorByUsername(string username)
        {
            SqlConnection oCnn = new SqlConnection(_Configuration.APICONSTRING);
            oCnn.Open();
            try
            {
                var parameters = new
                {
                    Username = username
                };

                Administrator obj = oCnn.QuerySingleOrDefault<Administrator>("[dbo].[SP_GETADMINISTRATORBYUSERNAME]", parameters, commandType: System.Data.CommandType.StoredProcedure);
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

        public Courier GetCourierByUsername(string username)
        {
            SqlConnection oCnn = new SqlConnection(_Configuration.APICONSTRING);
            oCnn.Open();
            try
            {
                var parameters = new
                {
                    Username = username
                };

                Courier obj = oCnn.QuerySingleOrDefault<Courier>("[dbo].[SP_GETCOURIERBYUSERNAME]", parameters, commandType: System.Data.CommandType.StoredProcedure);
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
        public List<Agency> GetAgencies()
        {
            SqlConnection oCnn = new SqlConnection(_Configuration.APICONSTRING);
            oCnn.Open();
            try
            {
                var values = new { };
                List<Agency> agencies = oCnn.Query<Agency>("[dbo].[SP_GETAGENCIES]", values, commandType: System.Data.CommandType.StoredProcedure).ToList();
                return agencies;
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
