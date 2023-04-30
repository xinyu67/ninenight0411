using DI.Models;
using DI.Security;
using DI.Service;
using DI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Data.SqlClient;
using System.Net;

namespace DI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly UserDBService _userDBService;
        private readonly string connectionString;
        private readonly MailDBService _mailService;
        private readonly JwtService _jwtService;
        
        public UserController(IConfiguration Configuration, UserDBService userDBService, MailDBService mailService, JwtService jwtService)
        {
            _config = Configuration;
            _userDBService = userDBService;
            connectionString = _config.GetConnectionString("local");
            _mailService = mailService;
            _jwtService = jwtService;
        }

        #region 後 - 使用者總覽
        [HttpGet]
        [Route("B_All")]
        //[Authorize(Roles = "Admin")]
        public IActionResult B_AllUser(string search = null)
        {
            var result = _userDBService.B_AllUser(search);
            if (result == null )
            {
                return NotFound("找不到資源");
            }
            return Ok(result);
        }
        #endregion

        #region 後 - 修改狀態
        [HttpPut]
        [Route("B_Put")]
        //[Authorize(Roles = "Admin")]
        public IActionResult B_PutUser([FromBody] User_B_UpdateViewModel value)
        {
            var result = _userDBService.B_PutUser(value);
            if (result == null)
            {
                return NotFound("找不到資源");
            }
            return Ok(result);
        }
        #endregion

        #region 前 - 個人資料總覽
        [HttpGet]
        [Route("F_All")]
        public IActionResult F_AllUser([FromQuery] Guid user_id)
        {
            var result = _userDBService.F_AllUser(user_id);
            if (result == null)
            {
                return NotFound("找不到資源");
            }
            return Ok(result);
        }
        #endregion

        #region 前 - 修改資料
        [HttpPut]
        [Route("F_Put")]
        public IActionResult F_PutUser([FromBody] User_F_UpdateViewModel value)
        {
            var result = _userDBService.F_PutUser(value);
            if (result == null)
            {
                return NotFound("找不到資源");
            }
            return Ok(result);
        }
        #endregion




        //------------------------------------33------------------------------------------------

        [HttpPost("Register")]
        public IActionResult Register([FromBody] UserRegisterViewModel RegisterMember)
        {
            if (ModelState.IsValid)
            {
                string emailExists = _userDBService.EmailCheck(RegisterMember.user_email);
                string accountExists = _userDBService.AccountCheck(RegisterMember.user_account);

                if (!string.IsNullOrWhiteSpace(emailExists) || !string.IsNullOrWhiteSpace(accountExists))
                {
                    return BadRequest("帳號已註冊或信箱已註冊");
                }



                // 取得信箱驗證碼
                string authCode = _mailService.GetValidateCode();

                // 将信箱驗證碼填入
                // RegisterMember.user_authcode = authCode;

                // 呼叫 Service 註冊新會員
                _userDBService.Register(RegisterMember, authCode);

                // 取得寫好的驗證信範本內容
                string filePath = @"View/RegisterEmail.html";
                string tempMail = System.IO.File.ReadAllText(filePath);

                // 藉由 Service 將使用者資料填入驗證信範本中
                string mailBody = _mailService.GetRegisterMailBody(tempMail, RegisterMember.user_name, authCode);

                // 呼叫 Service 寄出驗證信
                _mailService.SendRegisterMail(mailBody, RegisterMember.user_email);

                return Ok(new { message = "註冊成功，請去收信來驗證Email" });
            }

            // 未經驗證清空密碼相關欄位
            RegisterMember.user_pwd = null;

            // 將資料回填至 View 中
            return Ok(RegisterMember);
        }



        [HttpPost]
        [Route("email/validate")]
        public IActionResult EmailValidate([FromQuery] string Account, string AuthCode)
        {


            var result = _userDBService.EmailValidate(Account, AuthCode);


            // 建立一個匿名物件，包含驗證結果訊息


            // 將匿名物件轉換為JSON格式的回應
            return Ok(result);
        }



        [HttpGet]
        [Route("GetDataByAccount")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetDataByAccount(string user_account)
        {
            var result = _userDBService.GetDataByAccount(user_account);
            if (result == null)
            {
                return NotFound("找不到資源");
            }
            return Ok(result);
        }


        #region 修改密碼
        // 修改密碼傳入資料 Action
        // 設定此 Action 須登入

        [HttpPut] // 設定此 Action 接受頁面 POST 資料傳入
        [Route("ChangePassword")]
        [Authorize(Roles ="User")]
        public IActionResult ChangePassword([FromBody] ChangePassword ChangeData, string user_account)
        {

            // 判斷頁面資料是否都經過驗證
            if (ModelState.IsValid)
            {


                var result = _userDBService.ChangePassword(user_account, ChangeData.user_pwd, ChangeData.NewPassword);
                var response = new { result = result };

                // 將匿名物件轉換為JSON格式的回應
                return Ok(response);


            }
            return Ok();
        }
        #endregion




        // 登入一開始載入畫面

        // 傳入登入資料的 Action

        [HttpPost]
        [Route("Login")]// 設定此 Action 只接受頁面 POST 資料傳入
        public ActionResult Login([FromBody] LoginUser LoginMember)
        {

            // 使用 Service 裡的方法來驗證登入的帳號密碼
            string ValidateStr = _userDBService.LoginCheck(LoginMember.user_account, LoginMember.user_pwd);


            // 回傳根據編號所取得的資料




            // 判斷驗證後結果是否有錯誤訊息
            if (String.IsNullOrEmpty(ValidateStr))
            {

                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();
                // 無錯誤訊息，則登入
                // 重新導向頁面

                string ID_sql = $@"SELECT ""user_id"" FROM ""user"" where user_account='{LoginMember.user_account}' ";
                SqlCommand conn_User_id = new SqlCommand(ID_sql, conn);
                string user_ID = conn_User_id.ExecuteScalar().ToString();


                HttpContext.Response.Cookies.Append("UserAccount", LoginMember.user_account);

                string Role = _userDBService.GetRole(LoginMember.user_account);
                string jwt = _jwtService.f(user_ID, LoginMember.user_account, Role);

                string cookieName = _config["Jwt:CookieName"].ToString();

                var cookie = new CookieOptions();
                string encodedJwt = WebUtility.UrlEncode(jwt);

                // Set the cookie value


                // Set the cookie expiration
                Response.Cookies.Append(cookieName, encodedJwt, new CookieOptions
                {
                    Expires = DateTime.Now.AddMinutes(Convert.ToInt32(_config["Jwt:ExpireMinutes"]))
                });




                User Data = new User();

                conn.Close();

                //Sql 語法
                string sql = $@" select * from ""user"" where user_account =@user_account ";
                // 確保程式不會因執行錯誤而整個中斷
                try
                {
                    // 開啟資料庫連線
                    conn.Open();

                    // 執行 Sql 指令
                    SqlCommand command = new SqlCommand(sql, conn);
                    command.Parameters.AddWithValue("@user_account", LoginMember.user_account);
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


                var response = new
                {
                    User = Data,
                    EncodedJwt = encodedJwt
                };

                return Ok(response);

            }
            else
            {
                // 有驗證錯誤訊息，加入頁面模型中
                ModelState.AddModelError("", ValidateStr);

                return BadRequest(ValidateStr);
            }

        }

        #region 登出
        // 登出 Action

        [HttpPost]
        [Route("Logout")]// 設定此 Action 只接受頁面 POST 資料傳入
        [Authorize(Roles ="Admin,User")]
        public IActionResult Logout()
        {
            //Cookie 名稱
            string cookieName = _config["Jwt:CookieName"].ToString();

            var cookie = Request.Cookies[cookieName];
            //var jwt = _jwtService.GenerateToken(user_account, Role);
            // 使用者登出

            Response.Cookies.Delete(cookieName);


            return Ok();
        }
        #endregion




    }
}
