using DI.Models;
using DI.ViewModels;
using System.Data.SqlClient;

namespace DI.Service
{
    public class StoryDBService
    {
        private readonly IConfiguration _config;
        private readonly string connectionString;
        private readonly IWebHostEnvironment _environment;
        public StoryDBService(IConfiguration configuration, IWebHostEnvironment environment)
        {
            _config = configuration;
            connectionString = _config.GetConnectionString("local");
            _environment = environment;
        }

        #region 品牌故事總覽
        public List<StoryAllViewModels> F_AllStory()
        {
            string Sql = "SELECT * FROM story where isdel='False'";
            var img = "";
            List<StoryAllViewModels> DataList = new List<StoryAllViewModels>();
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
                        img = Path.Combine(FilePeth, reader["story_img"].ToString());
                        StoryAllViewModels Data = new StoryAllViewModels();
                        Data.story_id = (Guid)reader["story_id"];
                        Data.story_title = reader["story_title"].ToString();
                        Data.story_content = reader["story_content"].ToString();
                        Data.story_img = img;
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


        #region 單筆品牌故事(id查詢)
        public List<StoryAllViewModels> IdAllStory(Guid story_id)
        {
            string Sql = string.Empty;
            if (story_id != Guid.Empty)
            {
                Sql = "SELECT * FROM story where story_id=@story_id and isdel='false'";
            }
            else
            {
                Sql = "SELECT * FROM story";
            }

            List<StoryAllViewModels> DataList = new List<StoryAllViewModels>();
            var img = "";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(Sql, conn);
                if (story_id != Guid.Empty)
                {
                    command.Parameters.AddWithValue("@story_id", story_id);
                }
                try
                {
                    conn.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var FilePeth = Path.Combine($"https://localhost:7094", "image");
                        img = Path.Combine(FilePeth, reader["story_img"].ToString());
                        StoryAllViewModels Data = new StoryAllViewModels();
                        Data.story_id = (Guid)reader["story_id"];
                        Data.story_title = reader["story_title"].ToString();
                        Data.story_content = reader["story_content"].ToString();
                        Data.story_img = img;
                        
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


        #region 新增品牌故事
        public string CreateStory(StoryCreateViewModels value) {

            //圖片存入資料夾
            string rootRoot = _environment.ContentRootPath + @"\wwwroot\image\";
            var filename = "";
            string date = DateTime.Now.ToString("yyyyMMddHHmmss");
            if (value.story_img.Length > 0)
            {
                filename = date + value.story_img.FileName;
                using (var stream = System.IO.File.Create(rootRoot + filename))
                {
                    value.story_img.CopyTo(stream);
                }
            }

            string sql = $@"INSERT INTO story(story_id,story_title,story_content,story_img,isdel,create_id,create_time) VALUES (@story_id,@story_title,@story_content,@story_img,@isdel,@create_id,@create_time)";
            Guid NewGuid = Guid.NewGuid();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sql, conn);
                try
                {
                    conn.Open();
                    command.Parameters.AddWithValue("@story_id", NewGuid);
                    command.Parameters.AddWithValue("@story_title", value.story_title);
                    command.Parameters.AddWithValue("@story_content", value.story_content);
                    command.Parameters.AddWithValue("@story_img", filename);
                    command.Parameters.AddWithValue("@isdel", "0");
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


        #region 修改品牌故事
        public string UpdStory(StoryUpdateViewModels value)
        {

            var filename = "";
            if (value.story_img != null)
            {
                //圖片存入資料夾
                string rootRoot = _environment.ContentRootPath + @"\wwwroot\image\";
                string date = DateTime.Now.ToString("yyyyMMddHHmmss");
                if (value.story_img.Length > 0)
                {
                    filename = date + value.story_img.FileName;
                    using (var stream = System.IO.File.Create(rootRoot + filename))
                    {
                        value.story_img.CopyTo(stream);
                    }
                }
            }

            string sql = "";
            if (value.story_img != null)
            {
                sql = $@"UPDATE story SET story_title=@story_title,story_content=@story_content,story_img=@story_img,update_id=@update_id,update_time=@update_time WHERE story_id = @story_id";
            }
            else
            {
                sql = $@"UPDATE story SET story_title=@story_title,story_content=@story_content,update_id=@update_id,update_time=@update_time WHERE story_id = @story_id";
            }


            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sql, conn);
                try
                {
                    conn.Open();
                    command.Parameters.AddWithValue("@story_id", value.story_id);
                    command.Parameters.AddWithValue("@story_title", value.story_title);
                    command.Parameters.AddWithValue("@story_content", value.story_content);
                    if (value.story_img != null)
                    {
                        command.Parameters.AddWithValue("@story_img", filename);
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
        public string DeleteStory(Guid story_id)
        {
            string sql = $@"
            UPDATE story SET isdel=@isdel WHERE story_id = @story_id";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sql, conn);
                try
                {
                    conn.Open();
                    command.Parameters.AddWithValue("@isdel", '1');
                    command.Parameters.AddWithValue("@story_id", story_id);
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
