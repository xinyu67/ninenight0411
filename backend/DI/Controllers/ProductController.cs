using DI.Models;
using DI.Service;
using DI.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data.SqlClient;

namespace DI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly ProductDBService _productDBService;
        private readonly string connectionString;
        private readonly IWebHostEnvironment _environment;
        public ProductController(IConfiguration Configuration , ProductDBService productDBService, IWebHostEnvironment environment)
        {
            _config = Configuration;
            _productDBService = productDBService;
            connectionString = _config.GetConnectionString("local");
            _environment = environment;
        }


        #region 全部商品 & 名稱搜尋
        [HttpGet]
        public IActionResult Allproduct(string search_brand = null, string search_place = null, string search_ml = null, string money = null, string search_product= null) {
            var result=_productDBService.SearchProduct(search_brand, search_place, search_ml, money,search_product);
            if (result==null || result.Count<=0) {
                return NotFound("找不到資源");
            }
            return Ok(result);
        }
        #endregion

        #region 單一商品總覽資料(id)
        [HttpGet]
        [Route("product_id")]
        public IActionResult Allproduct_id([FromQuery] Guid product_id)
        {
            var result = _productDBService.Allproduct_id(product_id);
            if (result == null || result.Count <= 0)
            {
                return NotFound("找不到資源");
            }
            return Ok(result);
        }
        #endregion

        #region 新增商品
        [HttpPost]
        public IActionResult Createproduct([FromForm] ProductCreateViewModels value) {
            var create = _productDBService.CreateproductAsync(value);
            if (create == null)
            {
                return NotFound("找不到資源");
            }
            return Ok(create);
        }
        #endregion

        #region 修改商品
        [HttpPut]
        public IActionResult Putproduct([FromForm] ProductUpdateViewModel value)
        {
            var result = _productDBService.PutProduct(value);
            if (result == null)
            {
                return NotFound("找不到資源");
            }
            return Ok(result);
        }
        #endregion

        #region 軟刪除商品
        [HttpDelete]
        public IActionResult Deleteproduct([FromBody] Guid product_id)
        {
            string result = _productDBService.Deleteproduct(product_id);
            if (result == null)
            {
                return NotFound("找不到資源");
            }
            return Ok(result);
        }
        #endregion
    }
}
