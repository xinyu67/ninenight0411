using DI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using MongoDB.Bson.Serialization.IdGenerators;
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

        #region 新增購物車
        public string CreateCart(Guid product_id)
        {
            //判斷此帳號是否有購物車了
            string yesno_cart = "SELECT count(*) AS COUNT FROM cart where \"user_id\"='114aa3a7-f4d7-4a78-9eb5-0aff99d2d003' and cart_states='0'";

            //新增cart
            string sql_cart = $@"INSERT INTO cart(cart_id,user_id,cart_states,isdel,create_id,create_time) VALUES (@cart_id,@user_id,@cart_states,@isdel,@create_id,@create_time)";
            //新增cart_product
            string sql_cart_product = $@"INSERT INTO cart_product(cart_product_id,cart_id,product_id,cart_product_amount) VALUES (@cart_product_id,@cart_id,@product_id,@cart_product_amount)";
            

            //購物車編號
            Guid Cart_NewGuid = Guid.NewGuid();

            //購物車產品編號
            Guid Cart_P_NewGuid = Guid.NewGuid();

            int PP_num = 0;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand comm_yesno_cart = new SqlCommand(yesno_cart, conn);
                SqlCommand comm_cart = new SqlCommand(sql_cart, conn);
                SqlCommand comm_cart_product = new SqlCommand(sql_cart_product, conn);
                try
                {
                    conn.Open();
                    int count = (int)comm_yesno_cart.ExecuteScalar();
                    if (count == 0)
                    {
                        comm_cart.Parameters.AddWithValue("@cart_id", Cart_NewGuid);
                        comm_cart.Parameters.AddWithValue("@user_id", "114aa3a7-f4d7-4a78-9eb5-0aff99d2d003");
                        comm_cart.Parameters.AddWithValue("@cart_states", "0");
                        comm_cart.Parameters.AddWithValue("@isdel", "0");
                        comm_cart.Parameters.AddWithValue("@create_id", "114aa3a7-f4d7-4a78-9eb5-0aff99d2d003");
                        comm_cart.Parameters.AddWithValue("@create_time", DateTime.Now);
                        comm_cart.ExecuteNonQuery();
                    }
                    else {
                        //抓購物車id
                        string catr_id = "SELECT cart_id FROM cart where \"user_id\"='114aa3a7-f4d7-4a78-9eb5-0aff99d2d003' and cart_states='0'";
                        SqlCommand comm_cart_id = new SqlCommand(catr_id, conn);
                        //Guid G_cart_id = (Guid)comm_cart_id.ExecuteScalar();
                        //購物車編號
                        Cart_NewGuid = (Guid)comm_cart_id.ExecuteScalar();

                        //判斷此購物車是否有此商品了
                        string yesno_product = $"SELECT count(*) AS P_COUNT FROM cart_product where cart_id='{Cart_NewGuid}' and product_id='{product_id}'";
                        SqlCommand comm_yesno_product = new SqlCommand(yesno_product, conn);
                        PP_num = (int)comm_yesno_product.ExecuteScalar();
                    }

                    
                    if (PP_num == 0) {
                        comm_cart_product.Parameters.AddWithValue("@cart_product_id", Cart_P_NewGuid);
                        comm_cart_product.Parameters.AddWithValue("@cart_id", Cart_NewGuid);
                        comm_cart_product.Parameters.AddWithValue("@product_id", product_id);
                        comm_cart_product.Parameters.AddWithValue("@cart_product_amount", "1");
                        int cart_P_num = comm_cart_product.ExecuteNonQuery();
                        if (cart_P_num > 0)
                        {
                            return "新增成功！";
                        }
                        else {
                            return "新增失敗，請重試！";
                        }
                    }
                    else
                    {
                        return "購物車已有此商品！";
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

        #region 購物車總覽
        public List<CartAllViewModels> Allcart()
        {
            string Sql = "SELECT * FROM ((cart inner join cart_product on cart.cart_id = cart_product.cart_id) inner join product on cart_product.product_id=product.product_id) inner join \"user\" on cart.\"user_id\"=\"user\".\"user_id\" where cart.cart_states='0' ";
            //string Sql = "SELECT C.cart_id,C.\"user_id\",C.cart_states,C.isdel,CART_P.cart_product_id,CART_P.product_id,CART_P.cart_product_amount,P.product_name,P.product_img,P.product_price FROM \r\n\t((cart AS C inner join cart_product AS CART_P on C.cart_id = CART_P.cart_id)\r\n\tinner join product AS P on CART_P.product_id=P.product_id) where P.isdel='false'";

            List<CartViewModels> DataList = new List<CartViewModels>();
            List<CartAllViewModels> ProductList=new List<CartAllViewModels>();
            var img = "";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(Sql, conn);
                try
                {
                    conn.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    int cart_product_amount, product_price,total;

            
                    while (reader.Read())
                    {
                        CartViewModels Data = new CartViewModels();

                        var FilePeth = Path.Combine($"https://localhost:7094", "image");
                        img = Path.Combine(FilePeth, reader["product_img"].ToString());

                        cart_product_amount = (int)reader["cart_product_amount"];
                        product_price = (int)reader["product_price"];

                        Data.cart_id = (Guid)reader["cart_id"];
                        Data.cart_product_id = (Guid)reader["cart_product_id"];
                        Data.product_id = (Guid)reader["product_id"];
                        Data.product_name = reader["product_name"].ToString();
                        Data.product_img = img;
                        Data.product_price = (int)reader["product_price"];
                        Data.cart_product_amount = (int)reader["cart_product_amount"];

                        Data.money = (cart_product_amount * product_price);
                        Data.total += Data.money;
                        DataList.Add(Data);
                    }
                    ProductList = DataList.GroupBy(x => new { x.cart_id }).Select(n => new CartAllViewModels
                    {
                        cart_id = n.Key.cart_id,
                        product_list = n.GroupBy(x => new { x.cart_product_id,x.product_id,x.product_name, x.product_img, x.product_price})
                         .Select(n => new CartProductViewModels
                         {
                             cart_product_id=n.Key.cart_product_id,
                             product_id = n.Key.product_id,
                             product_name = n.Key.product_name,
                             product_img = n.Key.product_img,
                             product_price = n.Key.product_price,
                             cart_product_amount = n.Sum(y => y.cart_product_amount),
                             money= (n.Key.product_price * n.Sum(y => y.cart_product_amount))

                         }).ToList(),
                         total=n.Sum(y => y.total),
                    }).ToList();
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
                return ProductList;
            }
        }
        #endregion


        #region 購物車修改(商品數量)
        public string PutCart_P(CartUpdatePViewModels value)
        {
            string sql = $@"UPDATE cart_product SET cart_product_amount=@cart_product_amount WHERE cart_product_id=@cart_product_id";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sql, conn);
                try
                {
                    conn.Open();
                    command.Parameters.AddWithValue("@cart_product_id", value.cart_product_id);
                    command.Parameters.AddWithValue("@cart_product_amount", value.cart_product_amount);
                    int row = command.ExecuteNonQuery();
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


            #region 刪除購物車產品
            public string DeleteCart_P(Guid cart_product_id)
        {
            string sql = $@"DELETE FROM cart_product WHERE cart_product_id = @cart_product_id";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sql, conn);
                try
                {
                    conn.Open();
                    command.Parameters.AddWithValue("@cart_product_id", cart_product_id);
                    int num = command.ExecuteNonQuery();
                    if (num > 0)
                    {
                        return "刪除成功！";
                    }
                    else
                    {
                        return "刪除失敗，請重試！";
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
