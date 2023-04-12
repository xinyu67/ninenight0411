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
    }
}
