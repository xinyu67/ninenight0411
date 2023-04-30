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
            /*
            //製作金鑰
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetValue<string>("Jwt:SecretKey")));
            var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjEyMyIsIm5hbWVpZCI6IjNhNDVkZDk2LWFkY2ItNDc2OC1hMTkxLTU0ODU3N2I5YWRkYSIsInJvbGUiOlsiVXNlciIsIkFkbWluIl0sIm5iZiI6MTY4Mjg2NzY2NSwiZXhwIjoxNjgzNDcyNDY1LCJpYXQiOjE2ODI4Njc2NjV9.yXJvkV1QbGS9lbR5O2cIG-BJ6FCpQyyMhQ3DsO9RQ1g";
            try {
                IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
                IJsonSerializer serializer = new JsonNetSerializer();
                IDateTimeProvider provider = new UtcDateTimeProvider();
                IJwtValidator validator = new JwtValidator(serializer, provider);
                IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
                IJwtDecoder decoder = new JwtDecoder(serializer,validator, urlEncoder,algorithm);
                var json = decoder.Decode(token, key, verify: true);
                return json;
            }
            catch (TokenExpiredException) {
                Console.WriteLine("過期");
            }
            catch (SignatureVerificationException) {
                Console.WriteLine("校驗失敗");
            }
            */


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
