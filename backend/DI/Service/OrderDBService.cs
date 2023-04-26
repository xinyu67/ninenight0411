using DI.ViewModels;
using System.Data.SqlClient;

namespace DI.Service
{
    public class OrderDBService
    {
        private readonly IConfiguration _config;
        private readonly string connectionString;
        public OrderDBService(IConfiguration Configuration)
        {
            _config = Configuration;
            connectionString = _config.GetConnectionString("local");
        }

        #region 建立訂單(購物車送出後)
        public string CreateOrder(Order_F_CreateViewModels value)
        {
            string sql = $@"INSERT INTO order(order_id,cart_id,order_name,order_price,order_date,order_picktime,order_pick,order_address,order_phone,order_state,isdel,create_id,create_time) VALUES (@order_id,@cart_id,@order_name,@order_price,@order_date,@order_picktime,@order_pick,@order_address,@order_phone,@order_state,@isdel,@create_id,@create_time)";
            Guid NewGuid = Guid.NewGuid();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sql, conn);
                try
                {
                    conn.Open();
                    command.Parameters.AddWithValue("@order_id", NewGuid);
                    command.Parameters.AddWithValue("@cart_id", value.cart_id);
                    command.Parameters.AddWithValue("@order_name", value.order_name);
                    command.Parameters.AddWithValue("@order_price", value.order_price);
                    command.Parameters.AddWithValue("@order_date", DateTime.Now);
                    command.Parameters.AddWithValue("@order_picktime", value.order_picktime);
                    command.Parameters.AddWithValue("@order_pick", value.order_pick);
                    command.Parameters.AddWithValue("@order_address", value.order_address);
                    command.Parameters.AddWithValue("@order_phone", "0937377323");
                    command.Parameters.AddWithValue("@order_state", '0');
                    command.Parameters.AddWithValue("@isdel", "false");
                    command.Parameters.AddWithValue("@create_id", "admin");
                    command.Parameters.AddWithValue("@create_time", DateTime.Now);
                    int num = command.ExecuteNonQuery();
                    if (num > 0)
                    {
                        return "新增成功！";
                    }
                    else
                    {
                        return "新增失敗，請重試！";
                    }
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message.ToString());
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        #endregion


    }
}
