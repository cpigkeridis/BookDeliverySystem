using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using BookDeliveryCore;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;


namespace BookDeliverySystemAPI.Repositories
{
    public class ClientRepository : Interfaces.IClientRepository
    {
        ConfigManagerAppSettings.IConfigManager _Configuration;
        public ClientRepository(ConfigManagerAppSettings.IConfigManager oConfig)
        {
            _Configuration = oConfig;
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

        public List<BookDeliveryCore.Item> ShowShopItems()
        {

            SqlConnection oCnn = new SqlConnection(_Configuration.APICONSTRING);
            oCnn.Open();
            try
            {
                var values = new
                {

                };
                List<BookDeliveryCore.Item> obj = oCnn.Query<BookDeliveryCore.Item>("[dbo].[SP_SHOW_SHOP_ITEMS]", values, commandType: System.Data.CommandType.StoredProcedure).ToList();
                return obj;

            }
            finally
            {
                oCnn.Close();
                oCnn.Dispose();
            }
        }

        public string InsertOrderByCity(ShopForm data)
        {
            using (SqlConnection connection = new SqlConnection(_Configuration.APICONSTRING))
            {
                using (SqlCommand command = new SqlCommand("SP_FIND_AGENCY_BY_CITY", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters
                    command.Parameters.AddWithValue("@p_input_city", data.CITY);
                    command.Parameters.AddWithValue("@p_client_username", data.USERNAME);
                    command.Parameters.AddWithValue("@p_client_firstname", data.FIRSTNAME);
                    command.Parameters.AddWithValue("@p_client_lastname", data.LASTNAME);
                    command.Parameters.AddWithValue("@p_client_phone", data.PHONE_NUMBER);
                    command.Parameters.AddWithValue("@p_client_postal", data.POSTAL_CODE);
                    command.Parameters.AddWithValue("@p_client_address", data.ADDRESS);

                    // Create DataTable for ItemsList
                    DataTable itemsTable = new DataTable();
                    itemsTable.Columns.Add("ITEM_NAME", typeof(string));
                    itemsTable.Columns.Add("ITEM_PRICE", typeof(decimal));
                    itemsTable.Columns.Add("ORDER_ID", typeof(int));



                    foreach (var item in data.Items)
                    {
                        itemsTable.Rows.Add(item.ITEM_NAME, item.ITEM_PRICE, item.ITEM_ID);
                    }

                    // Add table-valued parameter
                    SqlParameter itemsParam = command.Parameters.AddWithValue("@ItemsList", itemsTable);
                    itemsParam.SqlDbType = SqlDbType.Structured;
                    itemsParam.TypeName = "dbo.ItemListType";

                    connection.Open();
                    command.ExecuteNonQuery();
                    return "ok";
                }
            }
        }

        public void InsertOrderUpdate(OrderUpdate data)
        {

            SqlConnection oCnn = new SqlConnection(_Configuration.APICONSTRING);
            oCnn.Open();
            try
            {
                var values = new
                {

                    orderId = data.OrderID,
                    newStatus = data.Status,
                    newEstimateDate = data.EDD
                };
                oCnn.ExecuteScalar("[dbo].[SP_UPDATE_ORDER_STATUS]", values, commandType: System.Data.CommandType.StoredProcedure);
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

        public void InsertOrderReviewUpdate(OrderUpdateReview data)
        {

            SqlConnection oCnn = new SqlConnection(_Configuration.APICONSTRING);
            oCnn.Open();
            try
            {
                var values = new
                {

                    orderId = data.OrderID,
                    review = data.Review,
                    reviewComments = data.ReviewComments
                };
                oCnn.ExecuteScalar("[dbo].[SP_UPDATE_ORDER_REVIEW]", values, commandType: System.Data.CommandType.StoredProcedure);
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

        public void updateReward(OrderUpdateReward data)
        {
            SqlConnection oCnn = new SqlConnection(_Configuration.APICONSTRING);
            oCnn.Open();
            try
            {
                var values = new
                {
                    AGENCY_NAME = data.AgencyName,
                    REVIEW = data.Review,
                    ORDER_ID = data.OrderID,
                    total_price = data.TotalPrice,
                };

                oCnn.ExecuteScalar("[dbo].[SP_UPDATE_REWARD_AND_ORDER]", values, commandType: System.Data.CommandType.StoredProcedure);
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

