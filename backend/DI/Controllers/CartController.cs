using DI.Service;
using DI.ViewModels;
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

        #region 購物車新增
        [HttpPost]
        public IActionResult CreateCart([FromQuery] Guid product_id)
        {
            var create = _cartDBService.CreateCart(product_id);
            if (create == null)
            {
                return NotFound("找不到資源");
            }
            return Ok(create);
        }
        #endregion

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

        #region 刪除購物車商品
        [HttpDelete]
        public IActionResult DeleteCart_P([FromQuery] Guid product_id)
        {
            string result = _cartDBService.DeleteCart_P(product_id);
            if (result == null)
            {
                return NotFound("找不到資源");
            }
            return Ok(result);
        }
        #endregion
    }
}
