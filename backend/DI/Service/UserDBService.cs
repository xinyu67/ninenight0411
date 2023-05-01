using DI.Models;
using DI.ViewModels;
using System.Data.SqlClient;
using System.Text;
using System.Security.Cryptography;

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
                //return DataList;
                return DataList.OrderBy(item => item.user_start).ThenBy(e => e.user_name).ToList();
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

        #region 前 - 修改資料
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





        //----------------------------------------33----------------------------------------------



        #region 註冊
        public void Register(UserRegisterViewModel newMember, string AuthCode)
        {
            SqlConnection conn = new SqlConnection(connectionString);

            newMember.user_pwd = HashPassword(newMember.user_pwd);
            string sql = string.Empty;


            sql = @"INSERT INTO ""user"" ( user_id, user_account,  user_pwd,user_name, user_gender, user_birthday, user_email,  user_authcode,user_phone, user_address,user_level,user_start,isdel,create_id,create_time,update_id,update_time) VALUES (@user_id, @user_account,  @user_pwd,@user_name, @user_gender, @user_birthday, @user_email,  @user_authcode,@user_phone, @user_address,@user_level,@user_start,@isdel,@create_id,@create_time,@update_id,@update_time )";

            try
            {
                conn.Open();

                SqlCommand command = new SqlCommand(sql, conn);
                command.Parameters.AddWithValue("@user_id", Guid.NewGuid());
                command.Parameters.AddWithValue("@user_account", newMember.user_account);
                command.Parameters.AddWithValue("@user_pwd", newMember.user_pwd);
                command.Parameters.AddWithValue("@user_name", newMember.user_name);
                command.Parameters.AddWithValue("@user_gender", newMember.user_gender);
                command.Parameters.AddWithValue("@user_birthday", newMember.user_birthday.ToString("yyyyMMdd"));
                command.Parameters.AddWithValue("@user_email", newMember.user_email);
                command.Parameters.AddWithValue("@user_authcode", AuthCode);
                command.Parameters.AddWithValue("@user_phone", newMember.user_phone);
                command.Parameters.AddWithValue("@user_address", newMember.user_address);
                command.Parameters.AddWithValue("@user_level", 0);
                command.Parameters.AddWithValue("@user_start", 0);
                command.Parameters.AddWithValue("@isdel", 0);
                command.Parameters.AddWithValue("@create_id", newMember.user_account);
                command.Parameters.AddWithValue("@create_time", DateTime.Now);
                command.Parameters.AddWithValue("@update_id", newMember.user_account);
                command.Parameters.AddWithValue("@update_time", DateTime.Now);
                command.ExecuteNonQuery();
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
        #endregion
        //Hash密碼
        //用的方法
        public string HashPassword(string user_pwd)
        {

            //宣告Hash時所添加的無意義亂數值
            string satltkey = "1q2w3e4r5t6y7u8ui9o0po7tyy";
            string saltAndPassword = String.Concat(user_pwd, satltkey);
            SHA256CryptoServiceProvider sha256Hasher = new SHA256CryptoServiceProvider();
            byte[] PasswordData = Encoding.Default.GetBytes(saltAndPassword);
            byte[] HashDate = sha256Hasher.ComputeHash(PasswordData);
            string Hashresult = Convert.ToBase64String(HashDate);
            return Hashresult;

        }

        public string EmailCheck(string Email)
        {

            User Data = GetDataByEmail(Email);

            if (Data == null)
            {
                return "";
            }
            else
            {
                return "信箱已被註冊";
            }
        }

        #region 帳號註冊重複確認
        // 確認要註冊帳號是否有被註冊過的方法
        public string AccountCheck(string Account)
        {

            User Data = GetDataByAccount(Account);

            if (Data == null)
            {
                return "";
            }
            else
            {
                return "已被註冊";
            }
        }
        #endregion


        #region 查詢一筆資料
        // 藉由帳號取得單筆資料的方法

        public User GetDataByAccount(string Account)
        {
            SqlConnection conn = new SqlConnection(connectionString);

            User Data = new User();

            //Sql 語法
            string sql = $@" select * from ""user"" where user_account =@user_account ";
            // 確保程式不會因執行錯誤而整個中斷
            try
            {
                // 開啟資料庫連線
                conn.Open();

                // 執行 Sql 指令
                SqlCommand command = new SqlCommand(sql, conn);
                command.Parameters.AddWithValue("@user_account", Account);
                command.ExecuteNonQuery();
                // 取得 Sql 資料
                SqlDataReader dr = command.ExecuteReader();
                dr.Read();
                Data.user_id = (Guid)dr["user_id"];
                Data.user_account = dr["user_account"].ToString();
                Data.user_pwd = dr["user_pwd"].ToString();
                Data.user_name = dr["user_name"].ToString();
                Data.user_gender = Convert.ToInt32(dr["user_gender"]);
                Data.user_birthday = Convert.ToDateTime(dr["user_birthday"]);
                Data.user_email = dr["user_email"].ToString();
                Data.user_authcode = dr["user_authcode"].ToString();
                Data.user_phone = dr["user_phone"].ToString();
                Data.user_address = dr["user_address"].ToString();
                Data.user_level = Convert.ToBoolean(dr["user_level"]);
                Data.isdel = Convert.ToBoolean(dr["isdel"]);



            }
            catch (Exception e)
            {
                // 查無資料
                Data = null;
            }
            finally
            {
                // 關閉資料庫連線
                conn.Close();
            }
            // 回傳根據編號所取得的資料
            return Data;
        }
        #endregion
        public User GetDataByEmail(string Email)
        {
            SqlConnection conn = new SqlConnection(connectionString);

            User Data = new User();

            //Sql 語法
            string sql = $@" select * from ""user"" where user_email =@user_email ";
            // 確保程式不會因執行錯誤而整個中斷
            try
            {
                // 開啟資料庫連線
                conn.Open();

                // 執行 Sql 指令
                SqlCommand command = new SqlCommand(sql, conn);
                command.Parameters.AddWithValue("@user_email", Email);
                command.ExecuteNonQuery();
                // 取得 Sql 資料
                SqlDataReader dr = command.ExecuteReader();
                dr.Read();
                Data.user_id = (Guid)dr["user_id"];
                Data.user_account = dr["user_account"].ToString();
                Data.user_pwd = dr["user_pwd"].ToString();
                Data.user_name = dr["user_name"].ToString();
                Data.user_gender = Convert.ToInt32(dr["user_gender"]);
                Data.user_birthday = Convert.ToDateTime(dr["user_birthday"]);
                Data.user_email = dr["user_email"].ToString();
                Data.user_authcode = dr["user_authcode"].ToString();
                Data.user_phone = dr["user_phone"].ToString();
                Data.user_address = dr["user_address"].ToString();
                Data.user_level = Convert.ToBoolean(dr["user_level"]);
                Data.isdel = Convert.ToBoolean(dr["isdel"]);



            }
            catch (Exception e)
            {
                // 查無資料
                Data = null;
            }
            finally
            {
                // 關閉資料庫連線
                conn.Close();
            }
            // 回傳根據編號所取得的資料
            return Data;
        }


        #region 信箱驗證
        // 信箱驗證碼驗證方法
        public string EmailValidate(string user_account, string AuthCode)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            // 取得傳入帳號的會員資料
            User ValidateMember = GetDataByAccount(user_account);
            // 宣告驗證後訊息字串
            string ValidateStr = string.Empty;
            if (ValidateMember != null)
            {
                // 判斷傳入驗證碼與資料庫中是否相同
                if (ValidateMember.user_authcode == AuthCode)
                {

                    // 將資料庫中的驗證碼設為空
                    //sql 更新語法
                    string sql = $@" update  ""user""  set  user_authcode = '{string.Empty}'  ,   user_start='1' where user_account = '{user_account}' ";

                    try
                    {
                        // 開啟資料庫連線
                        conn.Open();
                        // 執行 Sql 指令
                        SqlCommand cmd = new SqlCommand(sql, conn);
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        // 丟出錯誤
                        throw new Exception(e.Message.ToString());
                    }
                    finally
                    {
                        // 關閉資料庫連線
                        conn.Close();
                    }
                    ValidateStr = " 帳號信箱驗證成功，現在可以登入了 ";
                }
                else
                {
                    ValidateStr = " 驗證碼錯誤，請重新確認或再註冊 ";
                }
            }
            else
            {
                ValidateStr = " 傳送資料錯誤，請重新確認或再註冊 ";
            }
            // 回傳驗證訊息
            return ValidateStr;
        }
        #endregion



        #region 登入確認
        // 登入帳密確認方法，並回傳驗證後訊息
        public string LoginCheck(string user_account, string user_pwd)
        {
            // 取得傳入帳號的會員資料
            User LoginUser = GetDataByAccount(user_account);

            // 判斷是否有此會員
            if (LoginUser != null)
            {
                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();
                string sql_user_start = $@"SELECT user_start FROM ""user"" where user_account ='{user_account}' ";
                SqlCommand cmd_user_start = new SqlCommand(sql_user_start, conn);
                int user_start_num = (int)cmd_user_start.ExecuteScalar();

                // 判斷是否有經過信箱驗證，有經驗證驗證碼欄位會被清空
                if (user_start_num == 1)
                {
                    // 進行帳號密碼確認
                    if (PasswordCheck(LoginUser, user_pwd))
                    {
                        return "";
                    }
                    else
                    {
                        return " 密碼輸入錯誤 ";
                    }
                }
                else if(user_start_num == 0)
                {
                    return " 此帳號尚未經過 Email 驗證，請去收信 ";
                }else {
                    return " 此帳號停用中"+user_start_num;
                }
                conn.Close();
            }
            else
            {
                return " 無此會員帳號，請去註冊 ";
            }

            
        }
        #endregion




        #region 密碼確認
        // 進行密碼確認方法
        public bool PasswordCheck(User CheckMember, string user_pwd)
        {
            // 判斷資料庫裡的密碼資料與傳入密碼資料 Hash 後是否一樣
            bool result = CheckMember.user_pwd.Equals(HashPassword(user_pwd));
            // 回傳結果
            return result;
        }
        #endregion

        #region 變更密碼
        // 變更會員密碼方法，並回傳最後訊息
        public string ChangePassword(string user_account, string user_pwd, string NewPassword)

        {
            SqlConnection conn = new SqlConnection(connectionString);
            // 取得傳入帳號的會員資料
            User LoginUser = GetDataByAccount(user_account);
            // 確認舊密碼正確性
            if (PasswordCheck(LoginUser, user_pwd))
            {
                // 將新密碼 Hash 後寫入資料庫中
                LoginUser.user_pwd = HashPassword(NewPassword);
                //sql 修改語法
                string sql = $@" update  ""user""  set  user_pwd = '{LoginUser.user_pwd}' where user_account = '{user_account}' ";


                try
                {
                    // 開啟資料庫連線
                    conn.Open();
                    // 執行 Sql 指令
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    // 丟出錯誤
                    throw new Exception(e.Message.ToString());
                }
                finally
                {
                    // 關閉資料庫連線
                    conn.Close();
                }
                return " 密碼修改成功 ";
            }
            else
            {
                return " 舊密碼輸入錯誤 ";
            }
        }
        #endregion

        #region 取得角色
        // 取得會員的權限角色資料
        public string GetRole(string user_account)
        {
            // 宣告初始角色字串
            string Role = "User";
            // 取得傳入帳號的會員資料
            User LoginMember = GetDataByAccount(user_account);
            // 判斷資料庫欄位，用以確認是否為 Admon
            if (LoginMember.user_level)
            {
                Role += ",Admin"; // 添加 Admin
            }
            // 回傳最後結果
            return Role;
        }
        #endregion




    }
}
