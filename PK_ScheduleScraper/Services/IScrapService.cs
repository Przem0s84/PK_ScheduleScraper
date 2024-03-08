using PK_ScheduleScraper.Models;

namespace PK_ScheduleScraper.Services
{
    public interface IScrapService
    {
        
        Task<List<EventDto>> ScrapEventList(string teamName);
    }
}