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
            string cart_sql = $@"UPDATE cart SET cart_states='5',update_id='814aa3a7-f4d7-4a78-9eb5-0aff99d2d003',update_time=@cart_upd_time WHERE cart_id = '{value.cart_id}'";

            string product_num = "SELECT SUM(cart_product.cart_product_amount) AS num FROM (cart inner join cart_product on cart.cart_id = cart_product.cart_id)  where cart.cart_id=@P_cart_id and cart.cart_states=0";

            string sql = $@"INSERT INTO ""order""(order_id,cart_id,order_name,order_num,order_price,order_date,order_picktime,order_pick,order_address,order_phone,order_state,isdel,create_id,create_time,update_id,update_time) VALUES (@order_id,@cart_id,@order_name,@order_num,@order_price,@order_date,@order_picktime,@order_pick,@order_address,@order_phone,@order_state,@isdel,@create_id,@create_time,@update_id,@update_time)";
            Guid NewGuid = Guid.NewGuid();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sql, conn);
                SqlCommand command_num = new SqlCommand(product_num, conn);
                SqlCommand command_cart_sql = new SqlCommand(cart_sql, conn);
                try
                {
                    conn.Open();
                    command_cart_sql.Parameters.AddWithValue("@cart_upd_time", DateTime.Now);
                    command_num.Parameters.AddWithValue("@P_cart_id", value.cart_id);
                    int P_num = (int)command_num.ExecuteScalar();
                    command.Parameters.AddWithValue("@order_id", NewGuid);
                    command.Parameters.AddWithValue("@cart_id", value.cart_id);
                    command.Parameters.AddWithValue("@order_name", value.order_name);
                    command.Parameters.AddWithValue("@order_num", P_num);
                    command.Parameters.AddWithValue("@order_price", value.order_price);
                    command.Parameters.AddWithValue("@order_date", DateTime.Now);
                    command.Parameters.AddWithValue("@order_picktime", value.order_picktime);
                    command.Parameters.AddWithValue("@order_pick", value.order_pick);
                    command.Parameters.AddWithValue("@order_address", value.order_address);
                    command.Parameters.AddWithValue("@order_phone", value.order_phone);
                    command.Parameters.AddWithValue("@order_state", '0');
                    command.Parameters.AddWithValue("@isdel", "false");
                    command.Parameters.AddWithValue("@create_id", value.user_id);
                    command.Parameters.AddWithValue("@create_time", DateTime.Now);
                    command.Parameters.AddWithValue("@update_id", value.user_id);
                    command.Parameters.AddWithValue("@update_time", DateTime.Now);
                    int num = command.ExecuteNonQuery();
                    command_cart_sql.ExecuteNonQuery();
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
        
        #region 總覽訂單
        public List<Order_F_AllViewModels> AllOrder(Guid user_id)
        {
            string Sql = $@"SELECT * FROM ""order"" AS O inner join cart AS C on O.cart_id=C.cart_id  where C.""user_id""='{user_id}' and O.isdel='false'";

            List<Order_F_AllViewModels> DataList = new List<Order_F_AllViewModels>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(Sql, conn);
                try
                {
                    conn.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Order_F_AllViewModels Data = new Order_F_AllViewModels();
                        Data.order_id = (Guid)reader["order_id"];
                        Data.order_num = (int)reader["order_num"];
                        Data.order_price = (int)reader["order_price"];
                        //Data.order_picktime = reader["order_picktime"].ToString();
                        //Data.order_pick = (bool)reader["order_pick"];
                        //Data.order_address = reader["order_address"].ToString();
                        Data.order_state = (int)reader["order_state"];
                        Data.create_time = (DateTime)reader["create_time"];
                        Data.update_time = (DateTime)reader["update_time"];
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
                return DataList.OrderByDescending(e => e.update_time).ThenByDescending(cc => cc.create_time).ThenByDescending(item => item.order_state).ToList();
            }
        }
        #endregion

        #region 單一訂單總覽資料(id)
        public List<Order_F_OneIdAllViewModels> Allorder_id(Guid order_id)
        {
            string Sql = $"SELECT * FROM ((\"order\" AS O inner join cart_product AS C on O.cart_id=C.cart_id) inner join product AS P on C.product_id=P.product_id) where order_id='{order_id}' and O.isdel='false'";

            var img = "";
            List<Order_F_OneIdAllViewModels> DataList = new List<Order_F_OneIdAllViewModels>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(Sql, conn);
                try
                {
                    conn.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var FilePeth = Path.Combine($"https://localhost:7094", "image");
                        img = Path.Combine(FilePeth, reader["product_img"].ToString());
                        Order_F_OneIdAllViewModels Data = new Order_F_OneIdAllViewModels();
                        Data.order_name = reader["order_name"].ToString();
                        Data.order_phone = reader["order_phone"].ToString();
                        Data.order_picktime = reader["order_picktime"].ToString();
                        Data.order_pick = (bool)reader["order_pick"];
                        Data.order_address = reader["order_address"].ToString();
                        Data.product_img = img;
                        Data.product_name = reader["product_name"].ToString();
                        Data.product_ml = (int)reader["product_ml"];
                        Data.product_price = (int)reader["product_price"];
                        Data.cart_product_amount = (int)reader["cart_product_amount"];
                        Data.money = (int)reader["product_price"] * (int)reader["cart_product_amount"];
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
    }
}
