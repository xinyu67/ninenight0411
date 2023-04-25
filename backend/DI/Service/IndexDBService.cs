using DI.ViewModels;
using System.Data.SqlClient;

namespace DI.Service
{
    public class IndexDBService
    {
        private readonly IConfiguration _config;
        private readonly string connectionString;
        public IndexDBService(IConfiguration Configuration)
        {
            _config = Configuration;
            connectionString = _config.GetConnectionString("local");
        }

        #region 總覽品牌
        public List<IndexProductViewModels> LikeProduct()
        {
            string Sql = "SELECT TOP 4 * FROM product ORDER BY NEWID()";
            var img = "";
            List<IndexProductViewModels> DataList = new List<IndexProductViewModels>();
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
                        img = Path.Combine(FilePeth, reader["product_img"].ToString());
                        IndexProductViewModels Data = new IndexProductViewModels();
                        Data.product_id = (Guid)reader["product_id"];
                        Data.product_name = reader["product_name"].ToString();
                        Data.product_img = img;

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


        #region 最新消息
        public List<IndexNewViewModels> LatestNews()
        {
            string Sql = "SELECT TOP 4 * FROM new order by create_time DESC";
            List<IndexNewViewModels> DataList = new List<IndexNewViewModels>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(Sql, conn);
                try
                {
                    conn.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        IndexNewViewModels Data = new IndexNewViewModels();
                        Data.new_id = (Guid)reader["new_id"];
                        Data.new_title = reader["new_title"].ToString();
                        Data.new_startdate = reader["new_startdate"].ToString();
                        Data.new_enddate = reader["new_enddate"].ToString();

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
