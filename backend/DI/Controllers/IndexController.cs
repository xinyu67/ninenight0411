using DI.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IndexController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IndexDBService _indexDBService;
        private readonly string connectionString;
        public IndexController(IConfiguration Configuration, IndexDBService indexDBService)
        {
            _config = Configuration;
            _indexDBService = indexDBService;
            connectionString = _config.GetConnectionString("local");
        }

        #region 推薦商品
        [HttpGet]
        [Route("LikeProduct")]
        public IActionResult LikeProduct()
        {
            string cookieName = _config["Jwt:CookieName"].ToString();

            var cookie = Request.Cookies[cookieName];

            var result = _indexDBService.LikeProduct();
            if (result == null || result.Count <= 0)
            {
                return NotFound("找不到資源");
            }
            return Ok( cookie);
        }
        #endregion

        
        #region 最新消息
        [HttpGet]
        [Route("LatestNews")]
        public IActionResult LatestNews()
        {
            var result = _indexDBService.LatestNews();
            if (result == null || result.Count <= 0)
            {
                return NotFound("找不到資源");
            }
            return Ok(result);
        }
        #endregion
    }
}
