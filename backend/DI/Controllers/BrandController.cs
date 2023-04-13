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


        //新增產品品牌
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

        //產品品牌總覽
        [HttpGet]
        public IActionResult AllBrand()
        {
            var result = _brandDBService.SearchBrand();
            if (result == null || result.Count <= 0)
            {
                return NotFound("找不到資源");
            }
            return Ok(result);
        }

        //修改產品品牌
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



        //軟刪除
        [HttpDelete]
        public IActionResult DeleteBrand([FromBody] Guid brand_id)
        {
            string result = _brandDBService.DeleteBrand(brand_id);
            if (result == null)
            {
                return NotFound("找不到資源");
            }
            return Ok(result);
        }

        //品牌總覽(id搜尋)
        [HttpGet]
        [Route("brand_id")]
        public IActionResult IdBrand([FromQuery] string brand_id = null)
        {
            var result = _brandDBService.IdBrand(brand_id);
            if (result == null || result.Count <= 0)
            {
                return NotFound("找不到資源");
            }
            return Ok(result);
        }
    }
}
