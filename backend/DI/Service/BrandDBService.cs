using DI.Models;
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

        //新增品牌
        public string CreateBrand(BrandCreateViewModels value)
        {
            string sql = $@"INSERT INTO brand
                        (brand_id,brand_name,brand_eng,isdel,create_id,create_time) 
                        VALUES (@brand_id,@brand_name,@brand_eng,@isdel,@create_id,@create_time)";
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
                    int num=command.ExecuteNonQuery();
                    if (num > 0)
                    {
                        return "新增成功！";
                    }
                    else {
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


        //總覽品牌
        public List<BrandAllViewModels> SearchBrand()
        {
            string Sql = "SELECT * FROM brand";

            List<BrandAllViewModels> DataList = new List<BrandAllViewModels>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(Sql, conn);
                try
                {
                    conn.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        BrandAllViewModels Data = new BrandAllViewModels();
                        Data.brand_id = (Guid)reader["brand_id"];
                        Data.brand_name = reader["brand_name"].ToString();
                        Data.brand_eng = reader["brand_eng"].ToString();
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
                return DataList;
            }
        }

        //修改品牌
        public string PutBrand(BrandUpdateViewModel value)
        {
            string sql = $@"
            UPDATE brand SET brand_name=@brand_name,brand_eng=@brand_eng,update_id=@update_id,update_time=@update_time WHERE brand_id = @brand_id";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sql, conn);
                try
                {
                    conn.Open();
                    command.Parameters.AddWithValue("@brand_id", value.brand_id);
                    command.Parameters.AddWithValue("@brand_name", value.brand_name);
                    command.Parameters.AddWithValue("@brand_eng", value.brand_eng);
                    command.Parameters.AddWithValue("@update_id", "admin");
                    command.Parameters.AddWithValue("@update_time", DateTime.Now);
                    int row=command.ExecuteNonQuery();
                    if(row > 0)
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


        //軟刪除
        public string DeleteBrand(Guid brand_id)
        {
            string sql = $@"
            UPDATE brand SET isdel=@isdel WHERE brand_id = @brand_id";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sql, conn);
                try
                {
                    conn.Open();
                    command.Parameters.AddWithValue("@isdel", '1');
                    command.Parameters.AddWithValue("@brand_id", brand_id);
                    int num=command.ExecuteNonQuery();
                    if(num > 0)
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


        //總覽品牌(id搜尋)
        public List<BrandOneViewModels> IdBrand(string brand_id)
        {
            string Sql = string.Empty;
            if (string.IsNullOrWhiteSpace(brand_id) == false)
            {
                Sql = "SELECT * FROM brand where brand_id LIKE '%' + @brand_id + '%'";
            }
            else
            {
                Sql = "SELECT * FROM brand";
            }

            List<BrandOneViewModels> DataList = new List<BrandOneViewModels>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(Sql, conn);

                if (string.IsNullOrWhiteSpace(brand_id) == false)
                {
                    command.Parameters.AddWithValue("@brand_id", brand_id);
                }

                try
                {
                    conn.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        BrandOneViewModels Data = new BrandOneViewModels();
                        Data.brand_id = (Guid)reader["brand_id"];
                        Data.brand_name = reader["brand_name"].ToString();
                        Data.brand_eng = reader["brand_eng"].ToString();
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
                return DataList;
            }
        }
    }
}
