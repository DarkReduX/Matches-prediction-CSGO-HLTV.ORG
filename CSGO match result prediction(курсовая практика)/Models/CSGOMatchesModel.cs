using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CSGO_match_result_prediction_курсовая_практика_.Models
{
    public class MatchInfo
    {
        [Key]
        public Guid Id { get; set; }
        public string MatchUrl { get; set; }
        public string EventName { get; set; }
        public string LogoUrl { get; set; }
        public DateTime StartTime { get; set; }
        public ICollection<TeamInfo> TeamsInfo { get; set; }
        public string MatchFormat { get; set; }
        
        public TeamInfo Prediction { get; set; }
        public MatchResult Result { get; set; }
    }
    public class MatchResult
    {
        [Key]
        [ForeignKey("MatchInfo")]
        public Guid MatchInfoId { get; set; }
        public MatchInfo MatchInfo { get; set; }
        public TeamInfo Winner { get; set; }
        public TeamInfo Loser { get; set; }
        public string MapScore { get; set; }
    }
    public class TeamInfo
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public string LogoUrl { get; set; }
        public ICollection<MatchInfo> MatchInfo { get; set; }
        public ICollection<TeamMapStats> Stats { get; set; }

    }

    public class TeamMapStats
    {
        [Key]
        public int Id { get; set; }
        public string MapName { get; set; }
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
    public class AlgorithmStatsViewModel
    {
        public List<MatchInfo> MatchInfos { get; set; }
        public double WinRate { get; set; }
        public int LostCount { get; set; }
        public int WinCount { get; set; }
    }
}