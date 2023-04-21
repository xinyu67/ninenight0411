using DI.Service;
using DI.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace DI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly BrandDBService _brandDBService;
        private readonly string connectionString;
        public BrandController(IConfiguration Configuration, BrandDBService brandDBService)
        {
            _config = Configuration;
            _brandDBService = brandDBService;
            connectionString = _config.GetConnectionString("local");
        }


        #region 新增產品品牌
        [HttpPost]
        public IActionResult CreateBrand([FromBody] BrandCreateViewModels value)
        {
            var result = _brandDBService.CreateBrand(value);
            if (result == null)
            {
                return NotFound("找不到資源");
            }
            return Ok(result);
        }
        #endregion

        #region 後 - 產品品牌總覽
        [HttpGet]
        public IActionResult B_AllBrand()
        {
            var result = _brandDBService.B_AllBrand();
            if (result == null || result.Count <= 0)
            {
                return NotFound("找不到資源");
            }
            return Ok(result);
        }
        #endregion

        #region 單筆品牌資料(id查詢)
        [HttpGet]
        [Route("brand_id")]
        public IActionResult IdBrand([FromQuery] Guid brand_id)
        {
            var result = _brandDBService.IdBrand(brand_id);
            if (result == null || result.Count <= 0)
            {
                return NotFound("找不到資源");
            }
            return Ok(result);
        }
        #endregion

        #region 修改產品品牌
        [HttpPut]
        public IActionResult PutBrand([FromBody] BrandUpdateViewModel value)
        {
            var result = _brandDBService.PutBrand(value);
            if (result == null)
            {
                return NotFound("找不到資源");
            }
            return Ok(result);
        }
        #endregion

        #region 軟刪除
        [HttpDelete]
        public IActionResult DeleteBrand([FromQuery] Guid brand_id)
        {
            string result = _brandDBService.DeleteBrand(brand_id);
            if (result == null)
            {
                return NotFound("找不到資源");
            }
            return Ok(result);
        }
        #endregion


    }
}
