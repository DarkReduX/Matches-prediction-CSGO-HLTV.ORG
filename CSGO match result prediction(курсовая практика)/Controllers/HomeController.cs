using CSGO_match_result_prediction_курсовая_практика_.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using AngleSharp;

namespace CSGO_match_result_prediction_курсовая_практика_.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

    
        //public async Task Parse()
        //{
        //    var config = Configuration.Default.WithDefaultLoader();
        //    var adress = @"https://www.hltv.org/matches";
        //    var context = BrowsingContext.New(config);
        //    var document = await context.OpenAsync(adress);
        //    List<MatchInfo> matchesInfo = new List<MatchInfo>();
        //    var upcomingMatchesList = document.All.Where(m => m.LocalName == "div" && m.ClassName == "upcomingMatch ");
        //    Console.WriteLine(upcomingMatchesList.Count());
        //    int counter = 10;
        //    foreach (var item in upcomingMatchesList)
        //    {
        //        if (counter == 0)
        //            break;
        //        counter--;
        //        var itemEvent = item.GetElementsByClassName("matchEventName ")
        //                                        .FirstOrDefault();
        //        var itemTeams = item.GetElementsByClassName("matchTeamName text-ellipsis");
        //        string EventName = "Empty";
        //        string TeamName_1, TeamName_2;
        //        TeamInfo Team1, Team2;
        //        if (itemEvent != null && itemTeams.Count() == 2)
        //        {
        //            var itemLogos = item.GetElementsByTagName("img");
        //            var itemMatchUrl = item.GetElementsByTagName("a")[0].GetAttribute("href");
        //            EventName = itemEvent.TextContent;
        //            TeamName_1 = itemTeams[0].TextContent;
        //            TeamName_2 = itemTeams[1].TextContent;
        //            List<TeamMapStats> team1 = new List<TeamMapStats>();
        //            List<TeamMapStats> team2 = new List<TeamMapStats>();
        //            document = await context.OpenAsync("https://www.hltv.org" + itemMatchUrl);
        //            var stats = document.All.Where(m => m.ClassName == "map-stats-infobox-maps ");
        //            foreach (var itemStats in stats)
        //            {
        //                string mapName = itemStats.GetElementsByClassName("mapname")[0].TextContent;
        //                string[] winPercentage = new string[2] {
        //                    itemStats.GetElementsByClassName("map-stats-infobox-winpercentage ")[0].TextContent.Replace("%","").Replace("-","0"),
        //                    itemStats.GetElementsByClassName("map-stats-infobox-winpercentage ")[1].TextContent.Replace("%","").Replace("-","0")
        //                };
        //                string[] mapsPlayed = new string[2]
        //                {
        //                    itemStats.GetElementsByClassName("map-stats-infobox-maps-played")[0].TextContent.Replace(" maps","").Replace(" map", ""),
        //                    itemStats.GetElementsByClassName("map-stats-infobox-maps-played")[1].TextContent.Replace(" maps","").Replace(" map", "")
        //                };

        //                team1.Add(new TeamMapStats
        //                {
        //                    MapName = mapName,
        //                    MapPlayed = int.Parse(mapsPlayed[0]),
        //                    WinPercentage = int.Parse(winPercentage[0])
        //                });
        //                team2.Add(new TeamMapStats
        //                {
        //                    MapName = mapName,
        //                    MapPlayed = int.Parse(mapsPlayed[1]),
        //                    WinPercentage = int.Parse(winPercentage[1])
        //                });

        //            }
        //            Team1 = new TeamInfo
        //            {
        //                Id = item.GetAttribute("team1"),
        //                Name = TeamName_1,
        //                LogoUrl = itemLogos[0].GetAttribute("src"),
        //                Stats = team1
        //            };
        //            Team2 = new TeamInfo
        //            {
        //                Id = item.GetAttribute("team2"),
        //                Name = TeamName_2,
        //                LogoUrl = itemLogos[1].GetAttribute("src"),
        //                Stats = team2
        //            };
        //            matchesInfo.Add(new MatchInfo
        //            {
        //                EventName = EventName.Trim(),
        //                MatchUrl = itemMatchUrl,
        //                LogoUrl = itemLogos[2].GetAttribute("src"),
        //                TeamsInfo = new List<TeamInfo> { Team1, Team2 },
        //                DateTime = UnixTimeStampToDateTime(int.Parse(item.GetAttribute("data-zonedgrouping-entry-unix").Substring(0, 10)))
        //            });


        //        }
        //        foreach (var matchInfo in matchesInfo)
        //            db.MatchesInfo.Add(matchInfo);
        //    }
        //    return null;
        //}
        //public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        //{
        //    // Unix timestamp is seconds past epoch
        //    System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
        //    dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
        //    return dtDateTime;
        //}
    }
}