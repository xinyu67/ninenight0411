using DI.Service;
using DI.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly StoreDBService _storeDBService;
        private readonly string connectionString;
        public StoreController(IConfiguration Configuration, StoreDBService storeDBService)
        {
            _config = Configuration;
            _storeDBService = storeDBService;
            connectionString = _config.GetConnectionString("local");
        }

        
        #region 全部門市
        [HttpGet]
        public IActionResult AllStore()
        {
            var result = _storeDBService.AllStore();
            if (result == null || result.Count <= 0)
            {
                return NotFound("找不到資源");
            }
            return Ok(result);
        }
        #endregion

        #region 單一門市總覽資料(id)
        [HttpGet]
        [Route("store_id")]
        public IActionResult IdStore([FromQuery] Guid store_id)
        {
            var result = _storeDBService.IdStore(store_id);
            if (result == null || result.Count <= 0)
            {
                return NotFound("找不到資源");
            }
            return Ok(result);
        }
        #endregion

        #region 新增門市
        [HttpPost]
        public IActionResult CreateStory([FromBody] StoreCreateViewModels value)
        {
            var result = _storeDBService.CreateStore(value);
            if (result == null)
            {
                return NotFound("找不到資源");
            }
            return Ok(result);
        }
        #endregion

        #region 修改門市
        [HttpPut]
        public IActionResult UpdStore([FromBody] StoreUpdateViewModels value)
        {
            var result = _storeDBService.UpdStore(value);
            if (result == null)
            {
                return NotFound("找不到資源");
            }
            return Ok(result);
        }
        #endregion

        #region 刪除門市
        [HttpDelete]
        public IActionResult DeleteStore([FromBody] Guid store_id)
        {
            var result = _storeDBService.DeleteStore(store_id);
            if (result == null)
            {
                return NotFound("找不到資源");
            }
            return Ok(result);
        }
        #endregion

    }
}
