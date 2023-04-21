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
        
        #region 品牌故事總覽
        [HttpGet]
        public IActionResult F_AllStory()
        {
            var result = _storyDBService.F_AllStory();
            if (result == null || result.Count <= 0)
            {
                return NotFound("找不到資源");
            }
            return Ok(result);
        }
        #endregion

        #region 單筆品牌故事(id查詢)
        [HttpGet]
        [Route("story_id")]
        public IActionResult IdAllStory([FromQuery] Guid story_id)
        {
            var result = _storyDBService.IdAllStory(story_id);
            if (result == null || result.Count <= 0)
            {
                return NotFound("找不到資源");
            }
            return Ok(result);
        }
        #endregion

        #region 新增品牌故事
        [HttpPost]
        public IActionResult CreateStory([FromForm] StoryCreateViewModels value) {
            var result = _storyDBService.CreateStory(value);
            if (result == null)
            {
                return NotFound("找不到資源");
            }
            return Ok(result);
        }
        #endregion

        #region 修改品牌故事
        [HttpPut]
        public IActionResult UpdStory([FromForm] StoryUpdateViewModels value)
        {
            var result = _storyDBService.UpdStory(value);
            if (result == null)
            {
                return NotFound("找不到資源");
            }
            return Ok(result);
        }
        #endregion
        
        #region 刪除品牌故事
        [HttpDelete]
        public IActionResult DeleteStory([FromQuery] Guid story_id)
        {
            var result = _storyDBService.DeleteStory(story_id);
            if (result == null)
            {
                return NotFound("找不到資源");
            }
            return Ok(result);
        }
        #endregion

    }
}
