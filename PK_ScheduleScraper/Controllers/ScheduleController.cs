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

        [HttpGet]
        public async Task<ActionResult<IList<EventDto>>> GetAllMine([FromQuery]GetEvent eventProp)
        {
            var scrappedList = await _scraperService.ScrapEventList(eventProp.TeamName);

            var myEvents = scrappedList.Where(e => e.EventType == eventProp.LabGropName|| e.EventType == eventProp.ProjGroupName || e.EventType == eventProp.CompLabGroupName||e.EventType=="W"||e.EventType=="Ć"||e.EventType=="S" ||e.EventType.Contains("(K") ||e.EventType.Contains("M")||e.EventType.Contains("undefined")).Where(e=>e.WeekType==eventProp.WeekType).OrderBy(e=>e.DayNr).ThenBy(e=>e.EventLP).ToList();


            return Ok(myEvents);
        }

    }
}
