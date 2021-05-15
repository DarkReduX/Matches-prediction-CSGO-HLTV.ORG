using AngleSharp;
using AngleSharp.Dom;
using CSGO_match_result_prediction_курсовая_практика_.Models;
using Quartz;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
//sing Microsoft.;

namespace CSGO_match_result_prediction_курсовая_практика_.Jobs
{
    public class Parser : IJob
    {
        public IConfiguration config { get; set; }
        public string adress { get; set; }
        public IBrowsingContext context { get; set; }
        public IDocument document { get; set; }
        ApplicationDbContext db = new ApplicationDbContext();
        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                List<MatchInfo> matches = ParseMatchesToday().Result;
                List<MatchInfo> needPredict = db.MatchesInfo.Where(m => m.Prediction == null)
                    .Include(c => c.TeamsInfo)
                    .Include(s => s.TeamsInfo.Select(sel => sel.Stats)).ToList();
                foreach (var match in needPredict)
                {
                    match.Prediction = PredictMatch(match).Result;
                    db.Entry(match).State = EntityState.Modified;
                }
                db.SaveChanges();
                GetMatchesResultToDb();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
        }

        public async Task<List<MatchInfo>> ParseMatchesToday()
        {
            config = Configuration.Default.WithDefaultLoader();
            adress = @"https://www.hltv.org/matches";
            context = BrowsingContext.New(config);
            document = await context.OpenAsync(adress);
            const int count = 10;
            List<MatchInfo> matchInfos = new List<MatchInfo>();
            List<TeamInfo> teamsInfo = new List<TeamInfo>();
            var upcomingMatchesList = document.All.Where(m => m.LocalName == "div" && m.ClassName == "upcomingMatch ");
            Console.WriteLine(upcomingMatchesList.Count());
            int counter = 0;
            var dbMatchesInfo = db.MatchesInfo.ToList();
            var dbMatchesNotLoaded = db.MatchesNotLoaded.ToList();
            foreach (var item in upcomingMatchesList)
            {
                System.Diagnostics.Debug.WriteLine(upcomingMatchesList.Count());
                if (counter > count)
                    break;
                var itemTeams = item.GetElementsByClassName("matchTeamName text-ellipsis");
                string EventName = "Empty";
                if (itemTeams.Count() == 2 && EventName != null)
                {
                    counter++;
                    string TeamName_1, TeamName_2;
                    //TeamInfo Team1, Team2;
                    var itemLogos = item.GetElementsByTagName("img");
                    var itemMatchUrl = item.GetElementsByTagName("a")[0].GetAttribute("href");
                    TeamName_1 = itemTeams[0].TextContent;
                    TeamName_2 = itemTeams[1].TextContent;
                    var stats = ParseMatchStats(@"https://www.hltv.org" + itemMatchUrl).Result;
                    string team1_id = item.GetAttribute("team1"),
                              team2_id = item.GetAttribute("team2");
                    //var Team1 = db.TeamsInfo
                    //    .Where(t => t.Id == team1_id)
                    //    .FirstOrDefault();
                    //var Team2 = db.TeamsInfo
                    //    .Where(t => t.Id == team2_id)
                    //    .FirstOrDefault();
                    //if (Team1 == null)
                    TeamInfo Team1 = new TeamInfo
                    {
                        Id = team1_id,
                        Name = TeamName_1,
                        LogoUrl = itemLogos[0].GetAttribute("src"),
                        Stats = stats[0]
                    };
                    //if (Team2 == null)
                    TeamInfo Team2 = new TeamInfo
                    {
                        Id = team2_id,
                        Name = TeamName_2,
                        LogoUrl = itemLogos[1].GetAttribute("src"),
                        Stats = stats[1]
                    };
                    var team1contains = teamsInfo
                        .Where(t => t.Id == Team1.Id).FirstOrDefault();
                    var team1containsDb = db.TeamsInfo.ToList()
                        .FirstOrDefault(t => t.Id == Team1.Id);
                    var team2contains = teamsInfo
                        .Where(t => t.Id == Team2.Id).FirstOrDefault();
                    var team2containsDb = db.TeamsInfo.ToList()
                        .FirstOrDefault(t => t.Id == Team2.Id);
                    if (team1contains == null && team1containsDb == null)
                    {
                        teamsInfo.Add(Team1);
                        db.TeamsInfo.Add(Team1);
                    }
                    if (team2contains == null && team2containsDb == null)
                    {
                        teamsInfo.Add(Team2);
                        db.TeamsInfo.Add(Team2);
                    }

                }
            }
            counter = 0;
            //db.TeamsInfo.AddRange(teamsInfo);
            db.SaveChanges();
            foreach (var item in upcomingMatchesList)
            {

                if (counter > count)
                    break;
                counter++;
                var itemEvent = item.GetElementsByClassName("matchEventName ")
                                                .FirstOrDefault();
                var itemTeams = item.GetElementsByClassName("matchTeamName text-ellipsis");
                string EventName = "Empty";
                var itemMatchUrl = item.GetElementsByTagName("a")[0].GetAttribute("href");
                if (dbMatchesInfo
                        .FirstOrDefault(x => x.MatchUrl == itemMatchUrl) != null
                     || dbMatchesNotLoaded
                        .FirstOrDefault(x => x.MatchUrl == itemMatchUrl) != null)
                    continue;
                if (itemEvent != null && itemTeams.Count() == 2)
                {
                    string TeamName_1, TeamName_2;
                    //TeamInfo Team1, Team2;
                    var itemLogos = item.GetElementsByTagName("img");
                    var matchFormat = item.GetElementsByClassName("matchMeta");
                    EventName = itemEvent.TextContent;
                    TeamName_1 = itemTeams[0].TextContent;
                    TeamName_2 = itemTeams[1].TextContent;
                    var stats = ParseMatchStats("https://www.hltv.org" + itemMatchUrl).Result;
                    string team1_id = item.GetAttribute("team1"),
                           team2_id = item.GetAttribute("team2");
                    var Team1 = db.TeamsInfo.ToList()
                        .Where(t => t.Id == team1_id)
                        .FirstOrDefault();
                    var Team2 = db.TeamsInfo.ToList()
                        .Where(t => t.Id == team2_id)
                        .FirstOrDefault();
                    if (Team1 == null)
                        Team1 = new TeamInfo
                        {
                            Id = team1_id,
                            Name = TeamName_1,
                            LogoUrl = itemLogos[0].GetAttribute("src"),
                            Stats = stats[0]
                        };
                    else
                    {
                        Team1.Stats = stats[0];
                        db.Entry(Team1).State = EntityState.Modified;
                    }
                    if (Team2 == null)
                        Team2 = new TeamInfo
                        {
                            Id = team2_id,
                            Name = TeamName_2,
                            LogoUrl = itemLogos[1].GetAttribute("src"),
                            Stats = stats[1]
                        };
                    else
                    {
                        Team2.Stats = stats[0];
                        db.Entry(Team2).State = EntityState.Modified;
                    }
                    MatchInfo match = new MatchInfo
                    {
                        Id = Guid.NewGuid(),
                        EventName = EventName.Trim(),
                        MatchUrl = itemMatchUrl,
                        LogoUrl = itemLogos[2].GetAttribute("src"),
                        TeamsInfo = new List<TeamInfo>() { Team1, Team2 },
                        StartTime = UnixTimeStampToDateTime(int.Parse(item.GetAttribute("data-zonedgrouping-entry-unix").Substring(0, 10))),
                        MatchFormat = matchFormat[0].TextContent
                    };

                    matchInfos.Add(match);
                    db.MatchesInfo.Add(match);
                }
                else
                {
                    MatchNotLoaded notLoaded = new MatchNotLoaded()
                    {
                        MatchUrl = itemMatchUrl,
                        StartTime
                        = UnixTimeStampToDateTime(int.Parse(item.GetAttribute("data-zonedgrouping-entry-unix").Substring(0, 10)))
                    };
                    db.MatchesNotLoaded.Add(notLoaded);
                }

            }
            db.SaveChanges();
            return matchInfos;
        }

        public async Task<List<List<TeamMapStats>>> ParseMatchStats(string urlAdress)
        {
            List<TeamMapStats> team1 = new List<TeamMapStats>(), team2 = new List<TeamMapStats>();
            document = await context.OpenAsync(urlAdress);
            var stats = document.All.Where(m => m.ClassName == "map-stats-infobox-maps ");
            foreach (var item in stats)
            {
                string mapName = item.GetElementsByClassName("mapname")[0].TextContent;
                string[] winPercentage = new string[2] {
                    item.GetElementsByClassName("map-stats-infobox-winpercentage ")[0].TextContent.Replace("%","").Replace("-","0"),
                    item.GetElementsByClassName("map-stats-infobox-winpercentage ")[1].TextContent.Replace("%","").Replace("-","0")
                };
                string[] mapsPlayed = new string[2]
                {
                    item.GetElementsByClassName("map-stats-infobox-maps-played")[0].TextContent.Replace(" maps","").Replace(" map", ""),
                    item.GetElementsByClassName("map-stats-infobox-maps-played")[1].TextContent.Replace(" maps","").Replace(" map", "")
                };

                team1.Add(new TeamMapStats
                {
                    MapName = mapName,
                    MapPlayed = int.Parse(mapsPlayed[0]),
                    WinPercentage = int.Parse(winPercentage[0])
                });
                team2.Add(new TeamMapStats
                {
                    MapName = mapName,
                    MapPlayed = int.Parse(mapsPlayed[1]),
                    WinPercentage = int.Parse(winPercentage[1])
                });

            }
            var result = new List<List<TeamMapStats>>() { team1, team2 };
            return result;
        }
        public async Task UpdateNotLoadedMatches()
        {

        }
        public async Task GetMatchesResultToDb()
        {
            var matches = db.MatchesInfo
                .Where(m => m.Result == null && DateTime.Now > m.StartTime)
                .Include(t => t.TeamsInfo).ToListAsync().Result;
            foreach (var item in matches)
                db.Entry(item).Collection(m => m.TeamsInfo);
            //.ThenInclude();
            foreach (var match in matches)
            {
                if (match.TeamsInfo.Count() != 2)
                    continue;
                adress = "https://www.hltv.org" + match.MatchUrl;
                document = await context.OpenAsync(adress);
                if (document.All.
                    Where(m => m.ClassName == "won" && m.LocalName == "div")
                    .Count() > 0)
                {
                    var team1_score = document.GetElementsByClassName("team1-gradient")[0];
                    var team2_score = document.GetElementsByClassName("team2-gradient")[0];
                    MatchResult matchResult = new MatchResult();
                    if (team1_score.InnerHtml.Contains("won"))
                    {
                        matchResult.Winner = match.TeamsInfo.ToList()[0];
                        matchResult.Loser = match.TeamsInfo.ToList()[1];
                    }
                    else
                    {
                        matchResult.MatchInfo = match;
                        matchResult.Winner = match.TeamsInfo.ToList()[1];
                        matchResult.Loser = match.TeamsInfo.ToList()[0];
                    }
                    matchResult.MapScore = team1_score.LastElementChild.TextContent + "-"
                        + team2_score.LastElementChild.TextContent;
                    matchResult.MatchInfo = match;
                    db.MatchResults.Add(matchResult);
                    //match.Result = matchResult;
                    //db.Entry(match).State = EntityState.Modified;
                }
                try
                {
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e);
                }
            }
        }
        public async Task<TeamInfo> PredictMatch(MatchInfo match)
        {
            if (match.TeamsInfo.Count() == 2)
            {
                TeamInfo team1, team2;
                double team1_chance = 0, team2_chance = 0;

                team1 = match.TeamsInfo.ToList()[0];
                team2 = match.TeamsInfo.ToList()[1];
                var team1_stats = team1.Stats.ToList();
                var team2_stats = team2.Stats.ToList();
                var length = team1_stats.Count() >= team2_stats.Count() ?
                    team2_stats.Count() : team1_stats.Count();
                for (int i = 0; i < length; i++)
                {
                    if (team1_stats[i].WinPercentage > 70 && team1_stats[i].MapPlayed > 10)
                        team1_chance += 1.5;
                    else if (team1_stats[i].WinPercentage >= 50)
                        team1_chance += 1 * 0.3;
                    if (team2_stats[i].WinPercentage > 70 && team2_stats[i].MapPlayed > 10)
                        team2_chance += 1.5;
                    else if (team1_stats[i].WinPercentage >= 50)
                        team2_chance += 1 * 0.3;
                    /////////////////////////
                    if (team1_stats[i].WinPercentage > team2_stats[i].WinPercentage
                    && team1_stats[i].MapPlayed > team2_stats[i].MapPlayed)
                        team1_chance += 0.2;
                    else
                        team2_chance += 0.2;
                    ;
                }
                return team1_chance >= team2_chance ? team1 : team2;
            }
            return null;
        }
        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }
    }
}