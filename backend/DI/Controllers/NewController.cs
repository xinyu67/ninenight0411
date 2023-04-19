using DI.Service;
using DI.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly NewDBService _newDBService;
        private readonly string connectionString;
        public NewController(IConfiguration Configuration, NewDBService newDBService)
        {
            _config = Configuration;
            _newDBService = newDBService;
            connectionString = _config.GetConnectionString("local");
        }

        #region 新增最新消息
        [HttpPost]
        public IActionResult CreateNew([FromForm] NewCreateViewModels value)
        {
            var create = _newDBService.CreateNew(value);
            if (create == null)
            {
                return NotFound("找不到資源");
            }
            return Ok(create);
        }
        #endregion


        #region 全部最新消息
        [HttpGet]
        public IActionResult AllNew()
        {
            var result = _newDBService.AllNew();
            if (result == null || result.Count <= 0)
            {
                return NotFound("找不到資源");
            }
            return Ok(result);
        }
        #endregion

        #region 單一消息總覽資料(id)
        [HttpGet]
        [Route("new_id")]
        public IActionResult Allnew_id([FromQuery] Guid new_id)
        {
            var result = _newDBService.Allnew_id(new_id);
            if (result == null || result.Count <= 0)
            {
                return NotFound("找不到資源");
            }
            return Ok(result);
        }
        #endregion


        #region 修改最新消息
        [HttpPut]
        public IActionResult PutNew([FromBody] NewUpdateViewModel value)
        {
            var result = _newDBService.PutNew(value);
            if (result == null)
            {
                return NotFound("找不到資源");
            }
            return Ok(result);
        }
        #endregion

        #region 軟刪除最新消息
        [HttpDelete]
        public IActionResult DeleteNew([FromBody] Guid new_id)
        {
            string result = _newDBService.DeleteNew(new_id);
            if (result == null)
            {
                return NotFound("找不到資源");
            }
            return Ok(result);
        }
        #endregion
    }
}
