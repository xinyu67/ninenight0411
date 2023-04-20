using DI.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace DI.Service
{
    public class CartDBService
    {
        private readonly IConfiguration _config;
        private readonly string connectionString;
        public CartDBService(IConfiguration Configuration)
        {
            _config = Configuration;
            connectionString = _config.GetConnectionString("local");
        }

        #region 購物車總覽
        public List<CartAllViewModels> Allcart()
        {
            string Sql = "SELECT * FROM ((cart inner join cart_product on cart.cart_id = cart_product.cart_id) inner join product on cart_product.product_id=product.product_id) inner join \"user\" on cart.\"user_id\"=\"user\".\"user_id\"";

            List<CartAllViewModels> DataList = new List<CartAllViewModels>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(Sql, conn);
                try
                {
                    conn.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    int cart_product_amount;
                    int product_price;
                    int total;
                    while (reader.Read())
                    {
                        CartAllViewModels Data = new CartAllViewModels();
                        List<ProductCartViewModels> DD = new List<ProductCartViewModels>();
                        cart_product_amount = (int)reader["cart_product_amount"];
                        product_price = (int)reader["product_price"];

                        Data.cart_id = (Guid)reader["cart_id"];
                        //Data.product_id = (Guid)reader["product_id"];
                        //Data.cart_product_amount = (int)reader["cart_product_amount"];
                        //foreach (ProductCartViewModels Data2 in DD)
                        //{
                           // DD.Add(reader["product_name"].ToString());
                            //Data2.product_name = reader["product_name"].ToString();
                            //Data2.product_img = reader["product_img"].ToString();
                            //Data2.product_price = (int)reader["product_price"];
                        //}
                        Data.money = cart_product_amount * product_price;
                        Data.total += Data.money;
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
