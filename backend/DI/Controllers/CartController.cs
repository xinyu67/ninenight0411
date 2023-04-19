using DI.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly CartDBService _cartDBService;
        private readonly string connectionString;
        public CartController(IConfiguration Configuration, CartDBService cartDBService)
        {
            _config = Configuration;
            _cartDBService = cartDBService;
            connectionString = _config.GetConnectionString("local");
        }


        #region 購物車總覽
        [HttpGet]
        public IActionResult Allcart()
        {
            var result = _cartDBService.Allcart();
            if (result == null || result.Count <= 0)
            {
                return NotFound("找不到資源");
            }
            return Ok(result);
        }
        #endregion
    }
}
