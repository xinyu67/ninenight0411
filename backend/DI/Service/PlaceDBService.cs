using DI.ViewModels;
using System;
using System.Data.SqlClient;
using System.Xml.Linq;

namespace DI.Service
{
    public class PlaceDBService
    {
        private readonly IConfiguration _config;
        private readonly string connectionString;
        public PlaceDBService(IConfiguration Configuration)
        {
            _config = Configuration;
            connectionString = _config.GetConnectionString("local");
        }


        #region 新增產地
        public string CreatePlace(PlaceCreateViewModels value)
        {
            string Sql_repeat = "SELECT place_name FROM place where isdel='false'";


            string sql = $@"INSERT INTO place
                        (place_id,place_name,place_eng,isdel,create_id,create_time,update_id,update_time) 
                        VALUES (@place_id,@place_name,@place_eng,@isdel,@create_id,@create_time,@update_id,@update_time)";
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
                    while(reader.Read()){
                        if ( value.place_name == reader["place_name"].ToString())
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
                        command.Parameters.AddWithValue("@place_id", NewGuid);
                        command.Parameters.AddWithValue("@place_name", value.place_name);
                        command.Parameters.AddWithValue("@place_eng", value.place_eng);
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
                    else
                    {
                        return "已新增過此地點";
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

        #region 總覽產地
        public List<PlaceAllViewModels> B_AllPlace()
        {
            string Sql = "SELECT * FROM place where isdel='false'";


            List<PlaceAllViewModels> DataList = new List<PlaceAllViewModels>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(Sql, conn);
                try
                {
                    conn.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        PlaceAllViewModels Data = new PlaceAllViewModels();
                        Data.place_id = (Guid)reader["place_id"];
                        Data.place_name = reader["place_name"].ToString();
                        Data.place_eng = reader["place_eng"].ToString();
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

        #region 總覽產地(id搜尋)
        public List<PlaceOneViewModels> IdPlace(Guid place_id)
        {
            string Sql = string.Empty;
            if (place_id != Guid.Empty)
            {
                Sql = "SELECT * FROM place where place_id=@place_id and isdel='false'";
            }
            else
            {
                Sql = "SELECT * FROM place where isdel='false'";
            }

            List<PlaceOneViewModels> DataList = new List<PlaceOneViewModels>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(Sql, conn);

                if (place_id != Guid.Empty)
                {
                    command.Parameters.AddWithValue("@place_id", place_id);
                }

                try
                {
                    conn.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        PlaceOneViewModels Data = new PlaceOneViewModels();
                        Data.place_id = (Guid)reader["place_id"];
                        Data.place_name = reader["place_name"].ToString();
                        Data.place_eng = reader["place_eng"].ToString();

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

        #region 修改產地
        public string PutPlace(PlaceUpdateViewModel value)
        {
            string Sql_repeat = "SELECT place_name FROM place where isdel='false'";

            string sql = "";
            if (value.place_name == "null")
            {
                sql = $@"
            UPDATE place SET place_eng=@place_eng,update_id=@update_id,update_time=@update_time WHERE place_id = @place_id";
            }
            else if (value.place_eng == "null")
            {
                sql = $@"
            UPDATE place SET place_name=@place_name,update_id=@update_id,update_time=@update_time WHERE place_id = @place_id";
            }
            else
            {
                sql = $@"
            UPDATE place SET place_name=@place_name,place_eng=@place_eng,update_id=@update_id,update_time=@update_time WHERE place_id = @place_id";
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
                        if (value.place_name == reader["place_name"].ToString())
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
                        command.Parameters.AddWithValue("@place_id", value.place_id);
                        if (value.place_name != null)
                            command.Parameters.AddWithValue("@place_name", value.place_name);
                        if (value.place_eng != null)
                            command.Parameters.AddWithValue("@place_eng", value.place_eng);
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
                        return "已有此產地名稱";
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
        public string DeletePlace(Guid place_id)
        {
            string sql = $@"
            UPDATE place SET isdel=@isdel WHERE place_id = @place_id";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sql, conn);
                try
                {
                    conn.Open();
                    command.Parameters.AddWithValue("@isdel", '1');
                    command.Parameters.AddWithValue("@place_id", place_id);
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
