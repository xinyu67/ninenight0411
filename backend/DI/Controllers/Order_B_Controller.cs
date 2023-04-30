using DI.Service;
using DI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace DI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Order_B_Controller : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly Order_B_DBService _orderBDBService;
        private readonly string connectionString;
        public Order_B_Controller(IConfiguration Configuration, Order_B_DBService B_orderDBService)
        {
            _config = Configuration;
            _orderBDBService = B_orderDBService;
            connectionString = _config.GetConnectionString("local");
        }

        #region 訂單總覽
        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public IActionResult AllB_Order(string search=null)
        {
            var result = _orderBDBService.AllB_Order(search);
            if (result == null)
            {
                return NotFound("找不到資源");
            }
            return Ok(result);
        }
        #endregion
        
        #region 單一訂單總覽(id)
        [HttpGet]
        [Route("order_id")]
        //[Authorize(Roles = "Admin")]
        public IActionResult All_ID_B_Order([FromQuery] Guid order_id)
        {
            var result = _orderBDBService.All_ID_B_Order(order_id);
            if (result == null)
            {
                return NotFound("找不到資源");
            }
            return Ok(result);
        }
        #endregion

        #region 修改訂單狀態
        [HttpPut]
        //[Authorize(Roles = "Admin")]
        public IActionResult PutOrder_B([FromForm] OrderUpdateViewModel value)
        {
            var result = _orderBDBService.PutOrder_B(value);
            if (result == null)
            {
                return NotFound("找不到資源");
            }
            return Ok(result);
        }
        #endregion

        #region 軟刪除商品
        [HttpDelete]
        //[Authorize(Roles = "Admin")]
        public IActionResult Deleteorder([FromQuery] Guid order_id)
        {
            string result = _orderBDBService.Deleteorder(order_id);
            if (result == null)
            {
                return NotFound("找不到資源");
            }
            return Ok(result);
        }
        #endregion
    }
}
