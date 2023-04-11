using DI.Models;
using DI.Service;
using DI.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
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
        public ProductController(IConfiguration Configuration , ProductDBService productDBService)
        {
            _config = Configuration;
            _productDBService = productDBService;
            connectionString = _config.GetConnectionString("local");
        }

/*
        //商品總覽
        [HttpGet]
        public IActionResult Allproduct() {
            var result=_productDBService.GetProducts();
            if (result==null || result.Count<=0) {
                return NotFound("找不到資源");
            }
            return Ok(result);
        }*/

        
        //搜尋商品
        [HttpGet]
        public IActionResult Allproduct(string name=null) {
            var result=_productDBService.SearchProduct(name);
            if (result==null || result.Count<=0) {
                return NotFound("找不到資源");
            }
            return Ok(result);
        }

        //新增商品
        [HttpPost]
        public IActionResult Createproduct([FromBody] ProductCreateViewModels value, IFormFile file1) {
            var create = _productDBService.CreateproductAsync(value, file1);
            if (create == null)
            {
                return NotFound("找不到資源");
            }
            return Ok(create);
        }

        //修改商品
        [HttpPut]
        public IActionResult Putproduct([FromBody] ProductUpdateViewModel value)
        {
            var result = _productDBService.PutProduct(value);
            if (result == null)
            {
                return NotFound("找不到資源");
            }
            return Ok(result);
        }


        //軟刪除商品
        [HttpPut("{product_id}")]
        public IActionResult Deleteproduct([FromRoute] Guid product_id)
        {
            Guid result = _productDBService.Deleteproduct(product_id);
            /*if (result == null)
            {
                return NotFound("找不到資源");
            }
            else if (result == 1)
            {
                return Ok($"成功刪除商品資料");
            }*/
            return Ok(result);
        }
    }
}
