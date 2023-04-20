using DI.ViewModels;
using System;
using System.Data.SqlClient;

namespace DI.Service
{
    public class NewDBService
    {
        private readonly IConfiguration _config;
        private readonly string connectionString;
        private readonly IWebHostEnvironment _environment;
        public NewDBService(IConfiguration Configuration, IWebHostEnvironment environment)
        {
            _config = Configuration;
            _environment = environment;
            connectionString = _config.GetConnectionString("local");
        }

        #region 新增最新消息
        public string CreateNew(NewCreateViewModels value)
        {
            //圖片存入資料夾
            string rootRoot = _environment.ContentRootPath + @"\wwwroot\image\";
            var filename = "";
            string date = DateTime.Now.ToString("yyyyMMddHHmmss");
            if (value.new_img.Length > 0)
            {
                filename = date + value.new_img.FileName;
                using (var stream = System.IO.File.Create(rootRoot + filename))
                {
                    value.new_img.CopyTo(stream);
                }
            }

            string sql = $@"INSERT INTO new
                        (new_id,new_title,new_startdate,new_enddate,new_content,new_img,isdel,create_id,create_time) 
                        VALUES (@new_id,@new_title,@new_startdate,@new_enddate,@new_content,@new_img,@isdel,@create_id,@create_time)";
            Guid NewGuid = Guid.NewGuid();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sql, conn);
                try
                {
                    conn.Open();
                    command.Parameters.AddWithValue("@new_id", NewGuid);
                    command.Parameters.AddWithValue("@new_title", value.new_title);
                    command.Parameters.AddWithValue("@new_startdate", value.new_startdate);
                    command.Parameters.AddWithValue("@new_enddate", value.new_enddate);
                    command.Parameters.AddWithValue("@new_content", value.new_content);
                    command.Parameters.AddWithValue("@new_img", filename);
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



        #region 總覽最新消息
        public List<NewAllViewModels> AllNew()
        {
            string Sql = "SELECT * FROM new where isdel='false'";

            List<NewAllViewModels> DataList = new List<NewAllViewModels>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(Sql, conn);
                try
                {
                    conn.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        NewAllViewModels Data = new NewAllViewModels();
                        Data.new_id = (Guid)reader["new_id"];
                        Data.new_title = reader["new_title"].ToString();
                        Data.new_startdate = reader["new_startdate"].ToString();
                        Data.new_enddate = reader["new_enddate"].ToString();
                        Data.new_content = reader["new_content"].ToString();
                        Data.new_img = reader["new_img"].ToString();

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
                return DataList.OrderBy(item => item.new_startdate).ToList();
            }
        }
        #endregion


        #region 單一商品總覽資料
        public List<NewAllViewModels> Allnew_id(Guid new_id)
        {
            string Sql = string.Empty;
            if (new_id != Guid.Empty)
            {
                Sql = "SELECT * FROM new where new_id=@new_id and isdel='false'";
            }
            else
            {
                Sql = "SELECT * FROM new where isdel='false'";
            }
            List<NewAllViewModels> DataList = new List<NewAllViewModels>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(Sql, conn);

                if (new_id != Guid.Empty)
                {
                    command.Parameters.AddWithValue("@new_id", new_id);
                }
                
                try
                {
                    conn.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        NewAllViewModels Data = new NewAllViewModels();
                        Data.new_id = (Guid)reader["new_id"];
                        Data.new_title = reader["new_title"].ToString();
                        Data.new_startdate = reader["new_startdate"].ToString();
                        Data.new_enddate = reader["new_enddate"].ToString();
                        Data.new_content = reader["new_content"].ToString();
                        Data.new_img = reader["new_img"].ToString();


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


        #region 修改品牌
        public string PutNew(NewUpdateViewModel value)
        {

            //圖片存入資料夾
            string rootRoot = _environment.ContentRootPath + @"\wwwroot\image\";
            var filename = "";
            string date = DateTime.Now.ToString("yyyyMMddHHmmss");
            if (value.new_img.Length > 0)
            {
                filename = date + value.new_img.FileName;
                using (var stream = System.IO.File.Create(rootRoot + filename))
                {
                    value.new_img.CopyTo(stream);
                }
            }

            string sql = $@"
            UPDATE new SET new_title=@new_title,new_startdate=@new_startdate,new_enddate=@new_enddate,new_content=@new_content,new_img=@new_img,update_id=@update_id,update_time=@update_time WHERE new_id = @new_id";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sql, conn);
                try
                {
                    conn.Open();
                    command.Parameters.AddWithValue("@new_id", value.new_id);
                    command.Parameters.AddWithValue("@new_title", value.new_title);
                    command.Parameters.AddWithValue("@new_startdate", value.new_startdate);
                    command.Parameters.AddWithValue("@new_enddate", value.new_enddate);
                    command.Parameters.AddWithValue("@new_content", value.new_content);
                    command.Parameters.AddWithValue("@new_img", filename);
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
        public string DeleteNew(Guid new_id)
        {
            string sql = $@"
            UPDATE new SET isdel=@isdel WHERE new_id = @new_id";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sql, conn);
                try
                {
                    conn.Open();
                    command.Parameters.AddWithValue("@isdel", '1');
                    command.Parameters.AddWithValue("@new_id", new_id);
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
