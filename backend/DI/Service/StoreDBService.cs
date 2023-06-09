﻿using DI.ViewModels;
using System.Data.SqlClient;
using System.Xml.Linq;

namespace DI.Service
{
    public class StoreDBService
    {
        private readonly IConfiguration _config;
        private readonly string connectionString;
        private readonly IWebHostEnvironment _environment;
        public StoreDBService(IConfiguration Configuration, IWebHostEnvironment environment)
        {
            _config = Configuration;
            _environment = environment;
            connectionString = _config.GetConnectionString("local");
        }


        #region 總覽門市
        public List<StoreAllViewModels> AllStore()
        {
            string Sql = "SELECT * FROM store where isdel='false'";
            var img = "";
            List<StoreAllViewModels> DataList = new List<StoreAllViewModels>();
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
                        img = Path.Combine(FilePeth, reader["store_img"].ToString());
                        StoreAllViewModels Data = new StoreAllViewModels();
                        Data.store_id = (Guid)reader["store_id"];
                        Data.store_name = reader["store_name"].ToString();
                        Data.store_address = reader["store_address"].ToString();
                        Data.store_email = reader["store_email"].ToString();
                        Data.store_phone = reader["store_phone"].ToString();
                        Data.store_time = reader["store_time"].ToString();
                        Data.create_time = (DateTime)reader["create_time"];
                        Data.store_img = img;

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
                return DataList.OrderByDescending(item => item.create_time).ToList();
            }
        }
        #endregion

        #region 單一門市資料(id查詢)
        public List<StoreAllViewModels> IdStore(Guid store_id)
        {
            string Sql = string.Empty;
            if (store_id != Guid.Empty)
            {
                Sql = "SELECT * FROM store where store_id=@store_id and isdel='false'";
            }
            else
            {
                Sql = "SELECT * FROM store where isdel='false'";
            }
            var img = "";
            List<StoreAllViewModels> DataList = new List<StoreAllViewModels>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(Sql, conn);

                if (store_id != Guid.Empty)
                {
                    command.Parameters.AddWithValue("@store_id", store_id);
                }

                try
                {
                    conn.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        var FilePeth = Path.Combine($"https://localhost:7094", "image");
                        img = Path.Combine(FilePeth, reader["store_img"].ToString());
                        StoreAllViewModels Data = new StoreAllViewModels();
                        Data.store_id = (Guid)reader["store_id"];
                        Data.store_name = reader["store_name"].ToString();
                        Data.store_address = reader["store_address"].ToString();
                        Data.store_email = reader["store_email"].ToString();
                        Data.store_phone = reader["store_phone"].ToString();
                        Data.store_time = reader["store_time"].ToString();
                        Data.store_img = img;

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

        #region 新增門市
        public string CreateStore(StoreCreateViewModels value)
        {
            //圖片存入資料夾
            string rootRoot = _environment.ContentRootPath + @"\wwwroot\image\";
            var filename = "";
            string date = DateTime.Now.ToString("yyyyMMddHHmmss");
            if (value.store_img.Length > 0)
            {
                filename = date + value.store_img.FileName;
                using (var stream = System.IO.File.Create(rootRoot + filename))
                {
                    value.store_img.CopyTo(stream);
                }
            }


            string sql = $@"INSERT INTO store(store_id,store_name,store_address,store_email,store_phone,store_time,store_img,isdel,create_id,create_time,update_id,update_time) VALUES (@store_id,@store_name,@store_address,@store_email,@store_phone,@store_time,@store_img,@isdel,@create_id,@create_time,@update_id,@update_time)";
            Guid NewGuid = Guid.NewGuid();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sql, conn);
                try
                {
                    conn.Open();
                    command.Parameters.AddWithValue("@store_id", NewGuid);
                    command.Parameters.AddWithValue("@store_name", value.store_name);
                    command.Parameters.AddWithValue("@store_address", value.store_address);
                    command.Parameters.AddWithValue("@store_email", value.store_email);
                    command.Parameters.AddWithValue("@store_phone", value.store_phone);
                    command.Parameters.AddWithValue("@store_time", value.store_time);
                    command.Parameters.AddWithValue("@store_img", filename);
                    command.Parameters.AddWithValue("@isdel", "0");
                    command.Parameters.AddWithValue("@create_id", "admin");
                    command.Parameters.AddWithValue("@create_time", DateTime.Now);
                    command.Parameters.AddWithValue("@update_id", "admin");
                    command.Parameters.AddWithValue("@update_time", DateTime.Now);
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


        #region 修改品牌故事
        public string UpdStore(StoreUpdateViewModels value)
        {
            var filename = "";
            if (value.store_img != null)
            {
                //圖片存入資料夾
                string rootRoot = _environment.ContentRootPath + @"\wwwroot\image\";
                string date = DateTime.Now.ToString("yyyyMMddHHmmss");
                if (value.store_img.Length > 0)
                {
                    filename = date + value.store_img.FileName;
                    using (var stream = System.IO.File.Create(rootRoot + filename))
                    {
                        value.store_img.CopyTo(stream);
                    }
                }
            }

            string sql = "";
            if (value.store_img != null)
            {
                sql = $@"UPDATE store SET store_name=@store_name,store_address=@store_address,store_email=@store_email,store_phone=@store_phone,store_time=@store_time,store_img=@store_img,update_id=@update_id,update_time=@update_time WHERE store_id = @store_id";
            }
            else
            {
                sql = $@"UPDATE store SET store_name=@store_name,store_address=@store_address,store_email=@store_email,store_phone=@store_phone,store_time=@store_time,update_id=@update_id,update_time=@update_time WHERE store_id = @store_id";
            }


            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sql, conn);
                try
                {
                    conn.Open();
                    command.Parameters.AddWithValue("@store_id", value.store_id);
                    command.Parameters.AddWithValue("@store_name", value.store_name);
                    command.Parameters.AddWithValue("@store_address", value.store_address);
                    command.Parameters.AddWithValue("@store_email", value.store_email);
                    command.Parameters.AddWithValue("@store_phone", value.store_phone);
                    command.Parameters.AddWithValue("@store_time", value.store_time);
                    if (value.store_img != null)
                    {
                        command.Parameters.AddWithValue("@store_img", filename);
                    }
                    command.Parameters.AddWithValue("@update_id", "admin");
                    command.Parameters.AddWithValue("@update_time", DateTime.Now);
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

        #region 軟刪除
        public string DeleteStore(Guid store_id)
        {
            string sql = $@"
            UPDATE store SET isdel=@isdel WHERE store_id = @store_id";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sql, conn);
                try
                {
                    conn.Open();
                    command.Parameters.AddWithValue("@isdel", '1');
                    command.Parameters.AddWithValue("@store_id", store_id);
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
