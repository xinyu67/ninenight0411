using DI.Models;
using DI.ViewModels;
using Grpc.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;

namespace DI.Service
{
    public class ProductDBService
    {
        private readonly IConfiguration _config;
        private readonly string connectionString;
        public ProductDBService(IConfiguration configuration)
        {
            _config = configuration;
            connectionString = _config.GetConnectionString("local");
        }


        /*
        //商品總覽
        public List<ProductModels> GetProducts()
        {
            string Sql = string.Empty;
            Sql = "SELECT * FROM product";

            List<ProductModels> DataList = new List<ProductModels>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(Sql, conn);
                try
                {
                    conn.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        ProductModels Data = new ProductModels();
                        Data.product_id = (Guid)reader["product_id"];
                        Data.product_num = reader["product_num"].ToString();
                        Data.product_name = reader["product_name"].ToString();
                        Data.product_eng = reader["product_eng"].ToString();
                        Data.product_img = reader["product_img"].ToString();
                        Data.brand_id = (Guid)reader["brand_id"];
                        Data.product_price = (int)reader["product_price"];
                        Data.place_id = (Guid)reader["place_id"];
                        Data.product_ml = (int)reader["product_ml"];
                        Data.product_content = reader["product_content"].ToString();
                        Data.isdel = (bool)reader["isdel"];

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
                return DataList.OrderBy(item => item.create_time).ToList();
            }
        }*/


        //搜尋商品(關鍵字)
        public List<ProductModels> SearchProduct(string name)
        {
            string Sql = string.Empty;
            if (string.IsNullOrWhiteSpace(name) == false)
            {
                Sql = "SELECT * FROM product where product_name LIKE '%' + @name + '%' ";
            }
            else
            {
                Sql = "SELECT * FROM product";
            }

            List<ProductModels> DataList = new List<ProductModels>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(Sql, conn);
                if(name != null)
                    command.Parameters.AddWithValue("@name", name);
                try
                {
                    conn.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        ProductModels Data = new ProductModels();
                        Data.product_id = (Guid)reader["product_id"];
                        Data.product_num = reader["product_num"].ToString();
                        Data.product_name = reader["product_name"].ToString();
                        Data.product_eng = reader["product_eng"].ToString();
                        Data.product_img = reader["product_img"].ToString();
                        Data.brand_id = (Guid)reader["brand_id"];
                        Data.product_price = (int)reader["product_price"];
                        Data.place_id = (Guid)reader["place_id"];
                        Data.product_ml = (int)reader["product_ml"];
                        Data.product_content = reader["product_content"].ToString();
                        Data.isdel = (bool)reader["isdel"];

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
                return DataList.OrderBy(item => item.create_time).ToList();
            }
        }

        //新增商品
        public async Task<ProductCreateViewModels> CreateproductAsync(ProductCreateViewModels value, IFormFile file1) {

            var filename = file1.FileName;
            using (var stream = System.IO.File.Create(filename)) {
               //await file1.CopyToAsync(stream);
            }

            //string filePath = Server.MapPath("~/wwwroot/");//保存文件的路徑

            //將檔案和伺服器上路徑合併
            //string Url = Path.Combine(Server.MapPath("~/wwwroot/"), filename);
            //把檔案儲存於伺服器上
            //Data.Itemimage.saveAs(Url);
            //設定路徑
            //Data.NewData.Image = filename;


            string sql = $@"INSERT INTO product(product_id,product_num,product_name,product_eng,product_like,product_img,brand_id,product_price,place_id,product_ml,product_content,isdel,create_id,create_time) VALUES (@product_id,@product_num,@product_name,@product_eng,@product_like,@product_img,@brand_id,@product_price,@place_id,@product_ml,@product_content,@isdel,@create_id,@create_time)";
            Guid NewGuid = Guid.NewGuid();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sql, conn);

                try
                {
                    conn.Open();
                    command.Parameters.AddWithValue("@product_id", NewGuid);
                    command.Parameters.AddWithValue("@product_num", value.product_num);
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

                    return value;
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


        //修改商品
        public ProductUpdateViewModel PutProduct(ProductUpdateViewModel value)
        {
            string sql = $@"
            UPDATE product SET product_name=@product_name,product_eng=@product_eng,product_img=@product_img,product_price=@product_price,place_id=@place_id,product_ml=@product_ml,product_content=@product_content,update_id=@update_id,update_time=@update_time WHERE product_id = @product_id";
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
                    command.Parameters.AddWithValue("@place_id", value.place_id);
                    command.Parameters.AddWithValue("@product_ml", value.product_ml);
                    command.Parameters.AddWithValue("@product_content", value.product_content);
                    command.Parameters.AddWithValue("@product_id", value.product_id);
                    command.Parameters.AddWithValue("@update_time", DateTime.Now);
                    command.Parameters.AddWithValue("@update_id", "admin");
                    command.ExecuteNonQuery();
                    return value;
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

        //刪除商品
        public Guid Deleteproduct(Guid product_id)
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
                    command.ExecuteNonQuery();
                    return product_id;
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




    }
}
