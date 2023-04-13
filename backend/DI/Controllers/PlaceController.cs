using DI.Service;
using DI.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace DI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaceController : ControllerBase
    {

        private readonly IConfiguration _config;
        private readonly PlaceDBService _placeDBService;
        private readonly string connectionString;
        public PlaceController(IConfiguration Configuration, PlaceDBService placeDBService)
        {
            _config = Configuration;
            _placeDBService = placeDBService;
            connectionString = _config.GetConnectionString("local");
        }


        //新增產地
        [HttpPost]
        public IActionResult CreatePlace([FromBody] PlaceCreateViewModels value)
        {
            var result = _placeDBService.CreatePlace(value);
            if (result == null)
            {
                return NotFound("找不到資源");
            }
            return Ok(result);
        }


        //產地總覽
        [HttpGet]
        public IActionResult AllPlace()
        {
            var result = _placeDBService.AllPlace();
            if (result == null || result.Count <= 0)
            {
                return NotFound("找不到資源");
            }
            return Ok(result);
        }

        //修改產地
        [HttpPut]
        public IActionResult PutPlace([FromBody] PlaceUpdateViewModel value)
        {
            var result = _placeDBService.PutPlace(value);
            if (result == null)
            {
                return NotFound("找不到資源");
            }
            return Ok(result);
        }


        //軟刪除
        [HttpDelete]
        public IActionResult DeletePlace([FromBody] Guid place_id)
        {
            string result = _placeDBService.DeletePlace(place_id);
            if (result == null)
            {
                return NotFound("找不到資源");
            }
            return Ok(result);
        }

        //產地總覽(id搜尋)
        [HttpGet]
        [Route("place_id")]
        public IActionResult IdPlace([FromQuery] string place_id=null)
        {
            var result = _placeDBService.IdPlace(place_id);
            if (result == null || result.Count <= 0)
            {
                return NotFound("找不到資源");
            }
            return Ok(result);
        }
    }
}
