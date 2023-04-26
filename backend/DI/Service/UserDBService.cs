using DI.ViewModels;
using System.Data.SqlClient;

namespace DI.Service
{
    public class UserDBService
    {
        private readonly IConfiguration _config;
        private readonly string connectionString;
        public UserDBService(IConfiguration Configuration)
        {
            _config = Configuration;
            connectionString = _config.GetConnectionString("local");
        }

        #region 後－使用者總覽
        public List<User_B_AllViewModels> B_AllUser(string search)
        {
            string Sql = string.Empty;
            //如果搜尋都是空值
            if (string.IsNullOrWhiteSpace(search) == true)
            {
                Sql = @"SELECT * FROM ""user"" where isdel='false'";
            }
            else if (string.IsNullOrWhiteSpace(search) == false)
            {
                Sql = $@"SELECT * FROM ""user"" where (user_account LIKE '%{search}%' or user_name LIKE '%{search}%') and isdel='0'";
            }

            List<User_B_AllViewModels> DataList = new List<User_B_AllViewModels>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(Sql, conn);
                try
                {
                    conn.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        User_B_AllViewModels Data = new User_B_AllViewModels();
                        Data.user_id = (Guid)reader["user_id"];
                        Data.user_account = reader["user_account"].ToString();
                        Data.user_name = reader["user_name"].ToString();
                        Data.user_level = (bool)reader["user_level"];
                        Data.user_start = (int)reader["user_start"];
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


        #region 後 - 修改狀態
        public string B_PutUser(User_B_UpdateViewModel value)
        {
            string sql = $@"UPDATE ""user"" SET user_start=@user_start WHERE user_id = @user_id";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sql, conn);
                try
                {
                    conn.Open();
                    command.Parameters.AddWithValue("@user_id", value.user_id);
                    command.Parameters.AddWithValue("@user_start", value.user_start);
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

        #region 前 - 個人資料總覽
        public List<User_F_AllViewModels> F_AllUser(Guid user_id)
        {
            string Sql = $@"SELECT * FROM ""user"" where ""user_id""='{user_id}' ";


            List<User_F_AllViewModels> DataList = new List<User_F_AllViewModels>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(Sql, conn);
                try
                {
                    conn.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        User_F_AllViewModels Data = new User_F_AllViewModels();
                        Data.user_id = (Guid)reader["user_id"];
                        Data.user_account = reader["user_account"].ToString();
                        Data.user_name = reader["user_name"].ToString();
                        Data.user_gender = (int)reader["user_gender"];
                        Data.user_birthday = reader["user_birthday"].ToString();
                        Data.user_email = reader["user_email"].ToString();
                        Data.user_phone = reader["user_phone"].ToString();
                        Data.user_address = reader["user_address"].ToString();
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

        #region 前 - 修改狀態
        public string F_PutUser(User_F_UpdateViewModel value)
        {
            string sql = $@"UPDATE ""user"" SET user_name=@user_name,user_gender=@user_gender,user_address=@user_address WHERE user_id = @user_id";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sql, conn);
                try
                {
                    conn.Open();
                    command.Parameters.AddWithValue("@user_id", value.user_id);
                    command.Parameters.AddWithValue("@user_name", value.user_name);
                    command.Parameters.AddWithValue("@user_gender", value.user_gender);
                    command.Parameters.AddWithValue("@user_address", value.user_address);
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
    }
}
