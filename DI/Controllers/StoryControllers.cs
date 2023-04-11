using DI.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoryControllers : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly StoryDBService _storyDBService;
        private readonly string connectionString;
        public StoryControllers(IConfiguration Configuration, StoryDBService storyDBService)
        {
            _config = Configuration;
            _storyDBService = storyDBService;
            connectionString = _config.GetConnectionString("local");
        }


        //品牌故事總覽
        [HttpGet]
        public IActionResult Allproduct()
        {
            var result = _storyDBService.GetProducts();
            if (result == null || result.Count <= 0)
            {
                return NotFound("找不到資源");
            }
            return Ok(result);
        }


        //新增品牌故事


    }
}
