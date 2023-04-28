using DI.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForgetPwdController : ControllerBase
    {
        private readonly ForgetPwdDBService _forgetpwdService;
        private readonly IConfiguration _config;
        private readonly string connectionString;

        public ForgetPwdController(ForgetPwdDBService forgetpwdService, IConfiguration config)
        {
            _forgetpwdService = forgetpwdService;
            _config = config;
            connectionString = _config.GetConnectionString("Local");

        }

        #region 發送email
        [HttpGet]
        [Route("send_email")]
        public IActionResult forget_pwd(string user_email)
        {
            var result = _forgetpwdService.forget_pwd(user_email);
            if (result == null)
            {
                return NotFound("找不到資源");
            }
            else if (result == 0)
            {
                return Ok("資料庫未有此信箱");
            }
            else if (result == 1)
            {
                return Ok("已發送驗證碼");
            }
            return Ok(result);
        }
        #endregion


        #region 驗證驗證碼
        [HttpGet]
        [Route("verify_cord")]
        public IActionResult forget_pwd_code(string user_email,string code)
        {
            var result = _forgetpwdService.forget_pwd_code(user_email, code);
            if (result == null)
            {
                return NotFound("找不到資源");
            }
            return Ok(result);
        }
        #endregion

        #region 修改密碼
        [HttpPut]
        [Route("upd_pwd")]
        public IActionResult upd_pwd(string email,string pwd, string pwd_two)
        {
            var result = _forgetpwdService.upd_pwd(email,pwd, pwd_two);
            if (result == null)
            {
                return NotFound("找不到資源");
            }
            return Ok(result);
        }
        #endregion

    }
}
