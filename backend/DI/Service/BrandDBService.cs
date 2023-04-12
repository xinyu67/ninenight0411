using DI.ViewModels;
using System.Data.SqlClient;

namespace DI.Service
{
    public class BrandDBService
    {
        private readonly IConfiguration _config;
        private readonly string connectionString;
        public BrandDBService(IConfiguration Configuration)
        {
            _config = Configuration;
            connectionString = _config.GetConnectionString("local");
        }

        //新增產品品牌
        public BrandCreateViewModels CreateBrand(BrandCreateViewModels value)
        {
            string sql = $@"INSERT INTO brand(brand_id,brand_name,brand_eng,isdel,create_id,create_time) VALUES (@brand_id,@brand_name,@brand_eng,@isdel,@create_id,@create_time)";
            Guid NewGuid = Guid.NewGuid();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sql, conn);
                try
                {
                    conn.Open();
                    command.Parameters.AddWithValue("@brand_id", NewGuid);
                    command.Parameters.AddWithValue("@brand_name", value.brand_name);
                    command.Parameters.AddWithValue("@brand_eng", value.brand_eng);
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
    }
}
