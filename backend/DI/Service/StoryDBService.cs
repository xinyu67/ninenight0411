using DI.Models;
using DI.ViewModels;
using System.Data.SqlClient;

namespace DI.Service
{
    public class StoryDBService
    {
        private readonly IConfiguration _config;
        private readonly string connectionString;
        public StoryDBService(IConfiguration configuration)
        {
            _config = configuration;
            connectionString = _config.GetConnectionString("local");
        }


        //品牌故事總覽
        public List<StoryModels> AllStory()
        {
            string Sql = "SELECT * FROM story";

            List<StoryModels> DataList = new List<StoryModels>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(Sql, conn);
                try
                {
                    conn.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        StoryModels Data = new StoryModels();
                        Data.story_id = (Guid)reader["story_id"];
                        Data.story_title = reader["story_title"].ToString();
                        Data.story_content = reader["story_content"].ToString();
                        Data.story_img = reader["story_img"].ToString();
                        Data.isdel = (bool)reader["isdel"];
                        Data.create_id = reader["create_id"].ToString();
                        Data.create_time = (DateTime)reader["create_time"];
                        if (reader["update_id"].Equals(DBNull.Value))
                        {
                            Data.update_id = null;
                        }
                        else
                        {
                            Data.update_id = reader["update_id"].ToString();
                        }
                        if (reader["update_time"].Equals(DBNull.Value))
                        {
                            Data.update_time = null;
                        }
                        else {
                            Data.update_time = (DateTime)reader["update_time"];
                        }
                        
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


        //新增品牌故事
        public StoryCreateViewModels CreateStory(StoryCreateViewModels value) {
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
                    command.Parameters.AddWithValue("@story_img", value.story_img);
                    command.Parameters.AddWithValue("@isdel", "0");
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


        //修改品牌故事
        public StoryUpdateViewModels UpdStory(StoryUpdateViewModels value)
        {
            string sql = $@"UPDATE story SET story_title=@story_title,story_content=@story_content,story_img=@story_img,update_id=@update_id,update_time=@update_time WHERE story_id = @story_id";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sql, conn);
                try
                {
                    conn.Open();
                    command.Parameters.AddWithValue("@story_id", value.story_id);
                    command.Parameters.AddWithValue("@story_title", value.story_title);
                    command.Parameters.AddWithValue("@story_content", value.story_content);
                    command.Parameters.AddWithValue("@story_img", value.story_img);
                    command.Parameters.AddWithValue("@update_id", "admin");
                    command.Parameters.AddWithValue("@update_time", DateTime.Now);
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


        //軟刪除



    }
}
