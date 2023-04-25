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

        #region 新增品牌
        public string CreateBrand(BrandCreateViewModels value)
        {
            string Sql_repeat = "SELECT brand_name FROM brand where isdel='false'";

            string sql = $@"INSERT INTO brand
                        (brand_id,brand_name,brand_eng,isdel,create_id,create_time) 
                        VALUES (@brand_id,@brand_name,@brand_eng,@isdel,@create_id,@create_time)";
            Guid NewGuid = Guid.NewGuid();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sql, conn);
                SqlCommand command_repeat = new SqlCommand(Sql_repeat, conn);

                try
                {
                    conn.Open();

                    SqlDataReader reader = command_repeat.ExecuteReader();
                    int no_create = 0;
                    while (reader.Read())
                    {
                        if (value.brand_name == reader["brand_name"].ToString())
                        {
                            no_create = 1;
                            break;
                        }
                        else
                        {
                            no_create = 0;
                        }
                    }

                    if (no_create == 0)
                    {
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
                    else
                    {
                        return "已新增過此品牌";
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

        #region 總覽品牌
        public List<BrandAllViewModels> B_AllBrand()
        {
            string Sql = "SELECT * FROM brand where isdel='false'";

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

        #region 單一品牌資料(id查詢)
        public List<BrandOneViewModels> IdBrand(Guid brand_id)
        {
            string Sql = string.Empty;
            if (brand_id != Guid.Empty)
            {
                Sql = "SELECT * FROM brand where brand_id=@brand_id and isdel='false'";
            }
            else
            {
                Sql = "SELECT * FROM brand";
            }

            List<BrandOneViewModels> DataList = new List<BrandOneViewModels>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(Sql, conn);

                if (brand_id != Guid.Empty)
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
        public string PutBrand(BrandUpdateViewModel value)
        {
            string Sql_repeat = "SELECT brand_name FROM brand where isdel='false'";

            string sql = "";
            if (value.brand_name == null)
            {
                sql = $@"
            UPDATE brand SET brand_eng=@brand_eng,update_id=@update_id,update_time=@update_time WHERE brand_id = @brand_id";
            }
            else if (value.brand_eng == null)
            {
                sql = $@"
            UPDATE brand SET brand_name=@brand_name,update_id=@update_id,update_time=@update_time WHERE brand_id = @brand_id";
            }
            else {
                sql = $@"
            UPDATE brand SET brand_name=@brand_name,brand_eng=@brand_eng,update_id=@update_id,update_time=@update_time WHERE brand_id = @brand_id";
            }
            
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sql, conn);
                SqlCommand command_repeat = new SqlCommand(Sql_repeat, conn);
                try
                {
                    conn.Open();

                    SqlDataReader reader = command_repeat.ExecuteReader();
                    int no_create = 0;
                    while (reader.Read())
                    {
                        if (value.brand_name == reader["brand_name"].ToString())
                        {
                            no_create = 1;
                            break;
                        }
                        else
                        {
                            no_create = 0;
                        }
                    }

                    if (no_create == 0)
                    {
                        command.Parameters.AddWithValue("@brand_id", value.brand_id);
                        if (value.brand_name != null)
                            command.Parameters.AddWithValue("@brand_name", value.brand_name);
                        if (value.brand_eng != null)
                            command.Parameters.AddWithValue("@brand_eng", value.brand_eng);
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
                    else
                    {
                        return "已有此品牌名稱";
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
        #endregion

        
    }
}
