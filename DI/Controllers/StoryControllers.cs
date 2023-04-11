using DI.Service;
using DI.ViewModels;
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
        public IActionResult AllStory()
        {
            var result = _storyDBService.AllStory();
            if (result == null || result.Count <= 0)
            {
                return NotFound("找不到資源");
            }
            return Ok(result);
        }


        //新增品牌故事
        [HttpPost]
        public IActionResult CreateStory([FromBody] StoryCreateViewModels value) {
            var result = _storyDBService.CreateStory(value);
            if (result == null)
            {
                return NotFound("找不到資源");
            }
            return Ok(result);
        }


        //修改品牌故事
        [HttpPut]
        public IActionResult UpdStory([FromBody] StoryUpdateViewModels value)
        {
            var result = _storyDBService.UpdStory(value);
            if (result == null)
            {
                return NotFound("找不到資源");
            }
            return Ok(result);
        }


    }
}
