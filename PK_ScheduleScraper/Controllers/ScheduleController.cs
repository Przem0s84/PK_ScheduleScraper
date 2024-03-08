using Microsoft.AspNetCore.Mvc;

namespace PK_ScheduleScraper.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ScheduleController : ControllerBase
    {
        [HttpGet("getall")]
        public async Task<IActionResult<List<>>> GetAll([FromBody] )
        {


            return Ok();
        }

    }
}
