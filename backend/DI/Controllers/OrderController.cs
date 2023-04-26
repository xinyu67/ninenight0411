using DI.Service;
using DI.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly OrderDBService _orderDBService;
        private readonly string connectionString;
        public OrderController(IConfiguration Configuration, OrderDBService orderDBService)
        {
            _config = Configuration;
            _orderDBService = orderDBService;
            connectionString = _config.GetConnectionString("local");
        }

        #region 建立訂單(購物車送出後)
        [HttpPost]
        public IActionResult CreateOrder([FromForm] Order_F_CreateViewModels value)
        {
            var create = _orderDBService.CreateOrder(value);
            if (create == null)
            {
                return NotFound("找不到資源");
            }
            return Ok(create);
        }
        #endregion
        
        #region 訂單總覽
        [HttpGet]
        public IActionResult AllOrder()
        {
            var result = _orderDBService.AllOrder();
            if (result == null )
            {
                return NotFound("找不到資源");
            }
            return Ok(result);
        }
        #endregion

        #region 單一訂單總覽資料(id)
        [HttpGet]
        [Route("order_id")]
        public IActionResult Allorder_id([FromQuery] Guid order_id)
        {
            var result = _orderDBService.Allorder_id(order_id);
            if (result == null)
            {
                return NotFound("找不到資源");
            }
            return Ok(result);
        }
        #endregion
    }
}
