using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Text;
using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;
using PK_ScheduleScraper.Models;

namespace PK_ScheduleScraper.Services
{
    public class ScrapService : IScrapService
    {
        
         
         
        private readonly IConfiguration _config;
        public ScrapService(IConfiguration config)
        {
            _config= config;
        }

        public async Task<List<EventDto>> ScrapEventList(string teamName)
        {
          
            string urlCongif = $"ScheduleLinks:{teamName}";
            string url = _config[urlCongif];
            
            var DividePattern = @"(?<name>[A-Z][\w\sąćęłńóśźżŻŹ]*)(?<eventType>P0\d|W|Ć|S|L0\d|K0\d).*\-\((?<weekType>[NnPp]).*(?<location>[A-Z]\d{1,3}.*)|(?<name>J angielski).*\-\((?<weekType>[PpNn]).*(?<location>[A-Z]\d{1,3}.*)|(?<name>[A-Z][\w\sąćęłńóśźżŻŹ]*)(?<eventType>P0\d|W|Ć|Ś|L0\d|K0\d).*\-\((?<weekType>[NnPp])\)?.*\s[\w]\-?\w*|(?<name>WF).*(?<eventType>\((?:K|M|K\+M)\)).*(?<location>[A-Z][\w]*\d?)";

            var web = new HtmlWeb();
            web.OverrideEncoding = Encoding.UTF8;
            var doc = web.Load(url);
            var lablerows = doc.QuerySelectorAll("div table.tabela tr");
            var eventList = new List<EventDto> { };
            string[] NameOfDay = new string[] { "Poniedziałek", "Wtorek", "Środa", "Czwartek", "Piątek" };
            EventDto newEvent;
            string timeRange = null;
            int EventNrOnDay = 0;

            for (int i = 0; i < lablerows.Count; i++)
            {
                var tds = lablerows[i].QuerySelectorAll("td");
                var tdscount = tds.Count();
                for (int j = 0; j < tds.Count; j++)
                {
                    var cell = tds[j].InnerText;

                    if (j == 0)
                    {
                        EventNrOnDay = int.Parse(tds[0].InnerText);
                        continue;
                    }
                    if (j == 1)
                    {
                        timeRange = tds[1].InnerText;
                        continue;
                    }

                    cell = (cell.Contains("WF")) ? cell + "-p" : cell;

                    var divided = Regex.Split(cell, @"-[pn]");


                    for (int k = 0; k < divided.Length; k++)
                    {

                        var str = divided[k];
                        if (str.Length == 0 || str.Contains("&nb")) continue;


                        Match match = Regex.Match(str, DividePattern);
                        newEvent = new EventDto()
                        {
                            TimeRange = timeRange,
                            NameOfDay = NameOfDay[j - 2],
                            DayNr = j - 1,
                            EventLP = EventNrOnDay,
                            Name = (match.Groups["name"].Value != "" ? (match.Groups["name"].Value) : "no-data"),
                            EventType = (match.Groups["eventType"].Value != "" ? (match.Groups["eventType"].Value) : "undefined"),
                            Location = (match.Groups["location"].Value != "" ? (match.Groups["location"].Value) : "undefined"),
                            WeekType = (match.Groups["weekType"].Value != "" ? (match.Groups["weekType"].Value.ToUpper()) : "undefined or both")
                        };

                        eventList.Add(newEvent);

                    }
                }
            }

            

            return eventList;

        }
    }
}

