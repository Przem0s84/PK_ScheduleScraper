using Microsoft.AspNetCore.Mvc;
using PK_ScheduleScraper.Models;
using PK_ScheduleScraper.Services;

namespace PK_ScheduleScraper.Controllers
{
    [ApiController]
    [Route("api/schedule")]
    public class ScheduleController : ControllerBase
    {
        private readonly IScrapService _scraperService;
        public ScheduleController(IScrapService scrapService)
        {
            _scraperService = scrapService;
        }

        [HttpGet]
        [Route("{teamName}")]
        public async Task<ActionResult<IList<EventDto>>> GetAll([FromRoute] string teamName)
        {
           
            var scrappedList = await _scraperService.ScrapEventList(teamName);

            return Ok(scrappedList);
        }

    }
}
