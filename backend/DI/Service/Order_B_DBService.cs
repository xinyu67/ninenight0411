using DI.ViewModels;
using System.Data.SqlClient;

namespace DI.Service
{
    public class Order_B_DBService
    {
        private readonly IConfiguration _config;
        private readonly string connectionString;
        private readonly IWebHostEnvironment _environment;
        public Order_B_DBService(IConfiguration configuration, IWebHostEnvironment environment)
        {
            _config = configuration;
            _environment = environment;
            connectionString = _config.GetConnectionString("local");
        }

        #region 總覽訂單
        public List<Order_B_AllViewModels> AllB_Order(string search)
        {
            string Sql = string.Empty;
            //如果搜尋都是空值
            if (string.IsNullOrWhiteSpace(search) == true)
            {
                Sql = "SELECT * FROM \"order\" where isdel='0'";
            }
            else if (string.IsNullOrWhiteSpace(search) == false )
            {
                Sql = $"SELECT * FROM \"order\" where (order_phone LIKE '%{search}%' or order_name LIKE '%{search}%') and isdel='0'";
            }

            List<Order_B_AllViewModels> DataList = new List<Order_B_AllViewModels>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(Sql, conn);
                try
                {
                    conn.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Order_B_AllViewModels Data = new Order_B_AllViewModels();
                        Data.order_id = (Guid)reader["order_id"];
                        Data.order_name = reader["order_name"].ToString();
                        Data.order_phone = reader["order_phone"].ToString();
                        Data.order_price = (int)reader["order_price"];
                        Data.order_picktime = reader["order_picktime"].ToString();
                        Data.order_pick = (bool)reader["order_pick"];
                        Data.order_address = reader["order_address"].ToString();
                        Data.order_state = (int)reader["order_state"];
                        Data.create_time = (DateTime)reader["create_time"];
                        DataList.Add(Data);
                    }
                }
                catch (Exception e)
                {
                    //丟出錯誤
                    throw new Exception(e.Message.ToString());
                }
                finally
                {
                    conn.Close();
                }
                //return DataList;
                return DataList.OrderBy(item => item.order_state).ThenByDescending(e => e.create_time).ThenBy(e => e.order_name).ToList();
            }
        }
        #endregion
        
        #region 單一訂單總覽資料
        public List<Order_B_ID_ViewModels> All_ID_B_Order(Guid order_id)
        {
            string Sql = string.Empty;
            Sql = @"SELECT * FROM ((""order"" AS O inner join cart_product AS C on O.cart_id=C.cart_id) inner join product AS P on C.product_id=P.product_id) inner join cart AS CART on C.cart_id=CART.cart_id where O.order_id=@order_id and O.isdel='false'";
            List<Order_B_ID_ViewModels> DataList = new List<Order_B_ID_ViewModels>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(Sql, conn);
                command.Parameters.AddWithValue("@order_id", order_id);
                try
                {
                    conn.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Order_B_ID_ViewModels Data = new Order_B_ID_ViewModels();
                        Data.cart_id = (Guid)reader["cart_id"];
                        Data.order_id = (Guid)reader["order_id"];
                        Data.user_id = (Guid)reader["user_id"];
                        Data.order_pick = (bool)reader["order_pick"];
                        Data.order_address = reader["order_address"].ToString();
                        Data.order_date = (DateTime)reader["order_date"];
                        Data.product_id = (Guid)reader["product_id"];
                        Data.product_name = reader["product_name"].ToString();
                        Data.product_ml = (int)reader["product_ml"];
                        Data.cart_product_amount = (int)reader["cart_product_amount"];
                        Data.product_price = (int)reader["product_price"];
                        Data.order_price = (int)reader["order_price"];

                        DataList.Add(Data);
                    }
                }
                catch (Exception e)
                {
                    //丟出錯誤
                    throw new Exception(e.Message.ToString());
                }
                finally
                {
                    conn.Close();
                }
                return DataList;
            }
        }
        #endregion

        #region 修改訂單狀態
        public string PutOrder_B(OrderUpdateViewModel value)
        {
            string cart_id_sql = "SELECT cart_id FROM \"order\" where order_id = @order_id";
            string order_sql = $@"UPDATE ""order"" SET order_state=@order_state,update_id=@update_id,update_time=@update_time WHERE order_id = @order_id";
            string cart_sql = $@"UPDATE cart SET cart_states=@cart_states,update_id=@update_id,update_time=@update_time WHERE cart_id = @cart_id";

            int cart_states=0;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand command_cart_id_sql = new SqlCommand(cart_id_sql, conn);
                SqlCommand command = new SqlCommand(order_sql, conn);
                SqlCommand command_cart_sql = new SqlCommand(cart_sql, conn);
                try
                {
                    conn.Open();
                    command_cart_id_sql.Parameters.AddWithValue("@order_id", value.order_id);
                    string cart_id = command_cart_id_sql.ExecuteScalar().ToString();

                    command.Parameters.AddWithValue("@order_id", value.order_id);
                    command.Parameters.AddWithValue("@order_state", value.order_state);
                    command.Parameters.AddWithValue("@update_id", "admin");
                    command.Parameters.AddWithValue("@update_time", DateTime.Now);

                    if (value.order_state == 1) {
                        cart_states = 1;
                        command_cart_sql.Parameters.AddWithValue("@cart_id", cart_id);
                        command_cart_sql.Parameters.AddWithValue("@cart_states", cart_states);
                        command_cart_sql.Parameters.AddWithValue("@update_id", "admin");
                        command_cart_sql.Parameters.AddWithValue("@update_time", DateTime.Now);
                    } else if (value.order_state == 2) {
                        cart_states = 2;
                        command_cart_sql.Parameters.AddWithValue("@cart_id", cart_id);
                        command_cart_sql.Parameters.AddWithValue("@cart_states", cart_states);
                        command_cart_sql.Parameters.AddWithValue("@update_id", "admin");
                        command_cart_sql.Parameters.AddWithValue("@update_time", DateTime.Now);
                    }
                    else if (value.order_state == 5)
                    {
                        cart_states = 3;
                        command_cart_sql.Parameters.AddWithValue("@cart_id", cart_id);
                        command_cart_sql.Parameters.AddWithValue("@cart_states", cart_states);
                        command_cart_sql.Parameters.AddWithValue("@update_id", "admin");
                        command_cart_sql.Parameters.AddWithValue("@update_time", DateTime.Now);
                    }
                    

                    int row = command.ExecuteNonQuery();
                    int row_cart = command_cart_sql.ExecuteNonQuery();
                    if (row > 0)
                    {
                        return "修改成功！";
                    }
                    else
                    {
                        return "修改失敗，請重試！";
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

        #region 刪除訂單
        public string Deleteorder(Guid order_id)
        {
            string sql = $@"
            UPDATE ""order"" SET isdel=@isdel WHERE order_id = @order_id";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sql, conn);
                try
                {
                    conn.Open();
                    command.Parameters.AddWithValue("@isdel", '1');
                    command.Parameters.AddWithValue("@order_id", order_id);
                    int num = command.ExecuteNonQuery();
                    if (num > 0)
                    {
                        return "修改成功！";
                    }
                    else
                    {
                        return "修改失敗，請重試！";
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
