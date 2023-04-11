using DI.Models;
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
        public List<StoryModels> GetProducts()
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
                        Data.story_title = reader["story_title"].ToString();
                        Data.story_content = reader["story_content"].ToString();
                        Data.story_img = reader["story_img"].ToString();
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

    }
}
