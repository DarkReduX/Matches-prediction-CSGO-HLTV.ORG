using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CSGO_match_result_prediction_курсовая_практика_.Models
{
    public class MatchInfo
    {
        [Key]
        public string MatchUrl { get; set; }
        public string EventName { get; set; }
        public string LogoUrl { get; set; }
        public DateTime DateTime { get; set; }
        public List<TeamInfo> TeamsInfo { get; set; } = new List<TeamInfo>();
    }
    public class TeamInfo
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public string LogoUrl { get; set; }
        public MatchInfo MatchInfo { get; set; }
        public ICollection<TeamMapStats> Stats { get; set; }

    }

    public class TeamMapStats
    {
        [Key]
        public int Id { get; set; }
        public string MapName { get; set; }
        public string TeamInfoId { get; set; }
        public TeamInfo TeamInfo { get; set; }
        public int MapPlayed { get; set; }
        public int WinPercentage { get; set; }
    }
    public class MatchNotLoaded
    {
        [Key]
        public string MatchUrl { get; set; }
        public DateTime StartTime { get; set; }
    }

}