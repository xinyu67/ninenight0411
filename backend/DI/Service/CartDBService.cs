using DI.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        public List<CartViewModels> Allcart()
        {
            string Sql = "SELECT * FROM ((cart inner join cart_product on cart.cart_id = cart_product.cart_id) inner join product on cart_product.product_id=product.product_id) inner join \"user\" on cart.\"user_id\"=\"user\".\"user_id\"";

            List<CartViewModels> DataList = new List<CartViewModels>();
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
                    /*

                    CartAllViewModels ProductList = DataList.GroupBy(x => new { x.cart_id })
                          .Select(n => new CartAllViewModels
                          {
                              cart_id = n.Key.cart_id,
                              product_list = n.GroupBy(x => x.new { x.product_name, x.product_img, x.product_price })
                                             .Select(n => new ProductCartViewModels
                                             {
                                                 product_name = n.Key.product_name,
                                                 product_img = n.Key.product_img,
                                                 product_price = n.Key.product_price
                                             }).Tolist();
                            }).FirstOrDefault();
                    */


                while (reader.Read())
                    {
                        CartViewModels Data = new CartViewModels();

                        cart_product_amount = (int)reader["cart_product_amount"];
                        product_price = (int)reader["product_price"];

                        Data.cart_id = (Guid)reader["cart_id"];
                        Data.product_name = reader["product_name"].ToString();
                        Data.product_img = reader["product_img"].ToString();
                        Data.product_price = (int)reader["product_price"];
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
