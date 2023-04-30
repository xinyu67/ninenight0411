using DI.Service;
using JWT;
using JWT.Algorithms;
using JWT.Exceptions;
using JWT.Serializers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

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

            var result = _indexDBService.LikeProduct();
            if (result == null || result.Count <= 0)
            {
                return NotFound("找不到資源");
            }
            return Ok(result);
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
