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
        public List<ProductAllViewModels> SearchProduct(string name)
        {
            string Sql = string.Empty;
            if (string.IsNullOrWhiteSpace(name) == false)
            {
                Sql = "SELECT * FROM (product inner join place on product.place_id=place.place_id) inner join brand on product.brand_id=brand.brand_id where product_name LIKE '%' + @name + '%'";
            }
            else
            {
                Sql = "SELECT * FROM (product inner join place on product.place_id=place.place_id) inner join brand on product.brand_id=brand.brand_id";
            }

            List<ProductAllViewModels> DataList = new List<ProductAllViewModels>();
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
                        ProductAllViewModels Data = new ProductAllViewModels();
                        Data.product_id = (Guid)reader["product_id"];
                        Data.product_num = reader["product_num"].ToString();
                        Data.product_name = reader["product_name"].ToString();
                        Data.product_img = reader["product_img"].ToString();
                        Data.brand_name = reader["brand_name"].ToString();
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

            //var filename = value.product_img.FileName;
            //var filePath = Path.Combine(connectionString, filename);
            //using (var stream = System.IO.File.Create(filename)) {
            //await value.product_img.CopyToAsync(stream);
            //}

            //將檔案和伺服器上路徑合併
            //string Url = Path.Combine(Server.MapPath("~/wwwroot/"), filename);
            //把檔案儲存於伺服器上
            //Data.Itemimage.saveAs(Url);
            //設定路徑
            //Data.NewData.Image = filename;

            //string fileName = null;
            //string fileRelativePath = null;
            //if (value.product_img != null && value.product_img.Length > 0)
            //{
            //string uploadsFolder = Path.Combine(_environment.WebRootPath, "images");
            //fileName = value.product_img.FileName;
            //fileName = file1.FileName;
            //fileRelativePath = Path.Combine("images", fileName);
            //string filePath = Path.Combine(uploadsFolder, fileName);
            //using (var stream = new FileStream(filePath, FileMode.Create))
            //{
            //await value.product_img.CopyToAsync(stream);
            //}
            //}
            
            


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
            string rootRoot = _environment.ContentRootPath + @"\image\";
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

                    return "123";
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
