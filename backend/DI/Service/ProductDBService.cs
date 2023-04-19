using DI.Models;
using DI.ViewModels;
using Grpc.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DI.Service
{
    public class ProductDBService
    {
        private readonly IConfiguration _config;
        private readonly string connectionString;
        private readonly IWebHostEnvironment _environment;
        public ProductDBService(IConfiguration configuration, IWebHostEnvironment environment)
        {
            _config = configuration;
            _environment = environment;
            connectionString = _config.GetConnectionString("local");
        }

        #region 全部商品 & 名稱搜尋
        public List<ProductAllViewModels> SearchProduct(string search_brand, string search_place,string search_ml,string money,string search_product)
        {
            string Sql = string.Empty;
            //如果搜尋都是空值
            if (string.IsNullOrWhiteSpace(search_brand) == true & string.IsNullOrWhiteSpace(search_place) == true & string.IsNullOrWhiteSpace(search_ml) == true & string.IsNullOrWhiteSpace(money) == true & string.IsNullOrWhiteSpace(search_product) == true)
            {
                Sql = "SELECT * FROM (product inner join place on product.place_id=place.place_id) inner join brand on product.brand_id=brand.brand_id";
            }
            else if (string.IsNullOrWhiteSpace(search_brand) == true & string.IsNullOrWhiteSpace(search_place) == true & string.IsNullOrWhiteSpace(search_ml) == true  & string.IsNullOrWhiteSpace(search_product) == true & string.IsNullOrWhiteSpace(money) == false)
            {
                Sql = "SELECT * FROM (product inner join place on product.place_id=place.place_id) inner join brand on product.brand_id=brand.brand_id";
            }
            else
            {
                Sql = "SELECT * FROM (product inner join place on product.place_id=place.place_id) inner join brand on product.brand_id=brand.brand_id where ";
            }

            //如果只有search_brand有資料
            if (string.IsNullOrWhiteSpace(search_brand) == false & string.IsNullOrWhiteSpace(search_place) == true & string.IsNullOrWhiteSpace(search_ml) == true & string.IsNullOrWhiteSpace(search_product) == true )
            {
                Sql += "brand_name=@search_brand";
            }
            else if(string.IsNullOrWhiteSpace(search_brand) == false & (string.IsNullOrWhiteSpace(search_place) == false || string.IsNullOrWhiteSpace(search_ml) == false || string.IsNullOrWhiteSpace(search_product) == false) )
            {
                Sql += "brand_name=@search_brand and ";
            }

            //如果只有search_place有資料
            if (string.IsNullOrWhiteSpace(search_place) == false & string.IsNullOrWhiteSpace(search_ml) == true & string.IsNullOrWhiteSpace(search_product) == true )
            {
                Sql += "place_name=@search_place";
            }else if (string.IsNullOrWhiteSpace(search_place) == false & (string.IsNullOrWhiteSpace(search_ml) == false || string.IsNullOrWhiteSpace(search_product) == false ) )
            {
                Sql += "place_name=@search_place and ";
            }

            //如果只有search_ml有資料
            if (string.IsNullOrWhiteSpace(search_ml) == false & string.IsNullOrWhiteSpace(search_product) == true )
            {
                if (search_ml == "1")
                {
                    Sql += "(product_ml>=0 and product_ml<=300)";
                }
                else if (search_ml == "2")
                {
                    Sql += "(product_ml>=300 and product_ml<=400)";
                }
                else if (search_ml == "3")
                {
                    Sql += "(product_ml>=400 and product_ml<=500)";
                }
                else if (search_ml == "4")
                {
                    Sql += "(product_ml>=500 and product_ml<=600)";
                }
                else if (search_ml == "5")
                {
                    Sql += "(product_ml>=600 and product_ml<=700)";
                }
                else if (search_ml == "6")
                {
                    Sql += "product_ml>=700";
                }
                //Sql += "product_ml=@search_ml";
            }
            else if (string.IsNullOrWhiteSpace(search_ml) == false & string.IsNullOrWhiteSpace(search_product) == false )
            {
                if (search_ml == "1")
                {
                    Sql += "(product_ml>=0 and product_ml<=300) and ";
                }
                else if (search_ml == "2")
                {
                    Sql += "(product_ml>=300 and product_ml<=400) and ";
                }
                else if (search_ml == "3")
                {
                    Sql += "(product_ml>=400 and product_ml<=500) and ";
                }
                else if (search_ml == "4")
                {
                    Sql += "(product_ml>=500 and product_ml<=600) and ";
                }
                else if (search_ml == "5")
                {
                    Sql += "(product_ml>=600 and product_ml<=700) and ";
                }
                else if (search_ml == "6")
                {
                    Sql += "product_ml>=700 and ";
                }
                //Sql += "product_ml=@search_ml and ";
            }

            //如果search_product有資料
            if ( string.IsNullOrWhiteSpace(search_product) == false)
            {
                Sql += "product_name LIKE '%' + @search_product + '%'";
            }

            //如果money有排序
            if (money == "ASC" )
            {
                Sql += " order by product_price ASC";
            }
            else if (money == "DESC")
            {
                Sql += " order by product_price DESC";
            }

            /*
            string Sql = string.Empty;
            if (string.IsNullOrWhiteSpace(search_product) == false)
            {
                Sql = "SELECT * FROM (product inner join place on product.place_id=place.place_id) inner join brand on product.brand_id=brand.brand_id where product_name LIKE '%' + @search_product + '%'";
            }
            else
            {
                Sql = "SELECT * FROM (product inner join place on product.place_id=place.place_id) inner join brand on product.brand_id=brand.brand_id";
            }*/

            List<ProductAllViewModels> DataList = new List<ProductAllViewModels>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(Sql, conn);
                if (search_brand != null)
                    command.Parameters.AddWithValue("@search_brand", search_brand);
                if (search_place != null)
                    command.Parameters.AddWithValue("@search_place", search_place);
                if (search_ml != null)
                    command.Parameters.AddWithValue("@search_ml", search_ml);
                if (search_product != null)
                    command.Parameters.AddWithValue("@search_product", search_product);
                try
                {
                    conn.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        ProductAllViewModels Data = new ProductAllViewModels();
                        Data.product_id = (Guid)reader["product_id"];
                        //Data.product_num = reader["product_num"].ToString();
                        Data.product_name = reader["product_name"].ToString();
                        Data.product_img = reader["product_img"].ToString();
                        //Data.brand_name = reader["brand_name"].ToString();
                        Data.place_name = reader["place_name"].ToString();
                        Data.product_ml = (int)reader["product_ml"];
                        Data.product_price = (int)reader["product_price"];

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
                return DataList.OrderBy(item => item.product_name).ToList();
            }
        }
        #endregion

        #region 單一商品總覽資料
        public List<ProductIdViewModels> Allproduct_id(Guid product_id)
        {
            string Sql = string.Empty;
            Sql = "SELECT * FROM ((product inner join place on product.place_id=place.place_id) inner join brand on product.brand_id=brand.brand_id) where product_id=@product_id";
            List<ProductIdViewModels> DataList = new List<ProductIdViewModels>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(Sql, conn);
                command.Parameters.AddWithValue("@product_id", product_id);
                try
                {
                    conn.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        ProductIdViewModels Data = new ProductIdViewModels();
                        Data.product_id = (Guid)reader["product_id"];
                        Data.product_num = reader["product_num"].ToString();
                        Data.product_name = reader["product_name"].ToString();
                        Data.product_eng = reader["product_eng"].ToString();
                        Data.product_num = reader["product_num"].ToString();
                        Data.brand_name = reader["brand_name"].ToString();
                        Data.brand_eng = reader["brand_eng"].ToString();
                        Data.place_name = reader["place_name"].ToString();
                        Data.place_eng = reader["place_eng"].ToString();
                        Data.product_ml = (int)reader["product_ml"];
                        Data.product_price = (int)reader["product_price"];
                        Data.product_img = reader["product_img"].ToString();
                        Data.product_content = reader["product_content"].ToString();


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
        
        #region 新增商品
        public string CreateproductAsync(ProductCreateViewModels value) {

            //商品編號
            string allChar = "0,1,2,3,4,5,6,7,8,9";
            string[] allCharArray = allChar.Split(',');
            string randomCode = "";
            bool yesno = false;

            Random rand = new Random();
            SqlConnection conn_num = new SqlConnection(connectionString);
            conn_num.Open();
            var sql_num = "SELECT COUNT(*) FROM product where product_num='@num'";
            SqlCommand command_num = new SqlCommand(sql_num, conn_num);
            command_num.Parameters.AddWithValue("@num", randomCode);
            Int32 count = (Int32)command_num.ExecuteScalar();
            do
            {
                for (int i = 0; i < 4; i++)
                {
                    int t = rand.Next(10);
                    randomCode += allCharArray[t];
                }
            } while (count == '0');
            string num_randomCode = "NN" + randomCode;
            conn_num.Close();


            //圖片存入資料夾
            string rootRoot = _environment.ContentRootPath + @"\wwwroot\image\";
            var filename = "";
            if (value.product_img.Length > 0)
            {
                filename = num_randomCode + value.product_img.FileName;
                using (var stream = System.IO.File.Create(rootRoot + filename))
                {
                    value.product_img.CopyTo(stream);
                }
            }


            string sql = $@"INSERT INTO product(product_id,product_num,product_name,product_eng,product_like,product_img,brand_id,product_price,place_id,product_ml,product_content,isdel,create_id,create_time) VALUES (@product_id,@product_num,@product_name,@product_eng,@product_like,@product_img,@brand_id,@product_price,@place_id,@product_ml,@product_content,@isdel,@create_id,@create_time)";
            Guid NewGuid = Guid.NewGuid();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sql, conn);

                try
                {
                    conn.Open();
                    command.Parameters.AddWithValue("@product_id", NewGuid);
                    command.Parameters.AddWithValue("@product_num", num_randomCode);
                    command.Parameters.AddWithValue("@product_name", value.product_name);
                    command.Parameters.AddWithValue("@product_eng", value.product_eng);
                    command.Parameters.AddWithValue("@product_like", '0');
                    command.Parameters.AddWithValue("@product_img", filename);
                    command.Parameters.AddWithValue("@brand_id", value.brand_id);
                    command.Parameters.AddWithValue("@product_price", value.product_price);
                    command.Parameters.AddWithValue("@place_id", value.place_id);
                    command.Parameters.AddWithValue("@product_ml", value.product_ml);
                    command.Parameters.AddWithValue("@product_content", value.product_content);
                    command.Parameters.AddWithValue("@isdel", '0');
                    command.Parameters.AddWithValue("@create_id", "admin");
                    command.Parameters.AddWithValue("@create_time", DateTime.Now);
                    command.ExecuteNonQuery();

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


        #region 修改商品
        public string PutProduct(ProductUpdateViewModel value)
        {
            string sql = $@"
            UPDATE product SET product_name=@product_name,product_eng=@product_eng,product_img=@product_img,product_price=@product_price,brand_id=@brand_id,place_id=@place_id,product_ml=@product_ml,product_content=@product_content,update_id=@update_id,update_time=@update_time WHERE product_id = @product_id";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sql, conn);
                try
                {
                    conn.Open();
                    command.Parameters.AddWithValue("@product_name", value.product_name);
                    command.Parameters.AddWithValue("@product_eng", value.product_eng);
                    command.Parameters.AddWithValue("@product_img", value.product_img);
                    command.Parameters.AddWithValue("@product_price", value.product_price);
                    command.Parameters.AddWithValue("@brand_id", value.brand_id);
                    command.Parameters.AddWithValue("@place_id", value.place_id);
                    command.Parameters.AddWithValue("@product_ml", value.product_ml);
                    command.Parameters.AddWithValue("@product_content", value.product_content);
                    command.Parameters.AddWithValue("@product_id", value.product_id);
                    command.Parameters.AddWithValue("@update_time", DateTime.Now);
                    command.Parameters.AddWithValue("@update_id", "admin");
                    int num = command.ExecuteNonQuery();
                    if(num > 0)
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

        #region 刪除商品
        public string Deleteproduct(Guid product_id)
        {
            string sql = $@"
            UPDATE product SET isdel=@isdel WHERE product_id = @product_id";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sql, conn);
                try
                {
                    conn.Open();
                    command.Parameters.AddWithValue("@isdel", '1');
                    command.Parameters.AddWithValue("@product_id", product_id);
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
