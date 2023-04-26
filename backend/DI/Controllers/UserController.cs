using DI.Service;
using DI.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly UserDBService _userDBService;
        private readonly string connectionString;
        public UserController(IConfiguration Configuration, UserDBService userDBService)
        {
            _config = Configuration;
            _userDBService = userDBService;
            connectionString = _config.GetConnectionString("local");
        }

        #region 後 - 使用者總覽
        [HttpGet]
        [Route("B_All")]
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
    }
}
