namespace CSGO_match_result_prediction_курсовая_практика_.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TeamMapStats", "TeamInfo_Id", "dbo.TeamInfoes");
            DropForeignKey("dbo.TeamInfoes", "MatchInfo_MatchUrl", "dbo.MatchInfoes");
            DropForeignKey("dbo.TeamInfoes", "MatchInfo_MatchUrl1", "dbo.MatchInfoes");
            DropForeignKey("dbo.MatchInfoes", "Prediction_Id", "dbo.TeamInfoes");
            DropForeignKey("dbo.MatchResults", "Loser_Id", "dbo.TeamInfoes");
            DropForeignKey("dbo.MatchResults", "MatchInfoId", "dbo.MatchInfoes");
            DropForeignKey("dbo.MatchResults", "Winner_Id", "dbo.TeamInfoes");
            DropIndex("dbo.MatchInfoes", new[] { "Prediction_Id" });
            DropIndex("dbo.TeamInfoes", new[] { "MatchInfo_MatchUrl" });
            DropIndex("dbo.TeamInfoes", new[] { "MatchInfo_MatchUrl1" });
            DropIndex("dbo.TeamMapStats", new[] { "TeamInfo_Id" });
            DropIndex("dbo.MatchResults", new[] { "MatchInfoId" });
            DropIndex("dbo.MatchResults", new[] { "Loser_Id" });
            DropIndex("dbo.MatchResults", new[] { "Winner_Id" });
            DropTable("dbo.TeamMapStats");
            DropTable("dbo.TeamInfoes");
            DropTable("dbo.MatchResults");
            DropTable("dbo.MatchNotLoadeds");
            DropTable("dbo.MatchInfoes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.MatchNotLoadeds",
                c => new
                    {
                        MatchUrl = c.String(nullable: false, maxLength: 128),
                        StartTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.MatchUrl);
            
            CreateTable(
                "dbo.MatchResults",
                c => new
                    {
                        MatchInfoId = c.String(nullable: false, maxLength: 128),
                        MapScore = c.String(),
                        Loser_Id = c.String(maxLength: 128),
                        Winner_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.MatchInfoId);
            
            CreateTable(
                "dbo.TeamMapStats",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MapName = c.String(),
                        MapPlayed = c.Int(nullable: false),
                        WinPercentage = c.Int(nullable: false),
                        TeamInfo_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TeamInfoes",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        LogoUrl = c.String(),
                        MatchInfo_MatchUrl = c.String(maxLength: 128),
                        MatchInfo_MatchUrl1 = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MatchInfoes",
                c => new
                    {
                        MatchUrl = c.String(nullable: false, maxLength: 128),
                        EventName = c.String(),
                        LogoUrl = c.String(),
                        StartTime = c.DateTime(nullable: false),
                        MatchFormat = c.String(),
                        Prediction_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.MatchUrl);
            
            CreateIndex("dbo.MatchResults", "Winner_Id");
            CreateIndex("dbo.MatchResults", "Loser_Id");
            CreateIndex("dbo.MatchResults", "MatchInfoId");
            CreateIndex("dbo.TeamMapStats", "TeamInfo_Id");
            CreateIndex("dbo.TeamInfoes", "MatchInfo_MatchUrl1");
            CreateIndex("dbo.TeamInfoes", "MatchInfo_MatchUrl");
            CreateIndex("dbo.MatchInfoes", "Prediction_Id");
            AddForeignKey("dbo.TeamInfoes", "MatchInfo_MatchUrl1", "dbo.MatchInfoes", "MatchUrl");
            AddForeignKey("dbo.MatchResults", "Winner_Id", "dbo.TeamInfoes", "Id");
            AddForeignKey("dbo.MatchResults", "MatchInfoId", "dbo.MatchInfoes", "MatchUrl");
            AddForeignKey("dbo.MatchResults", "Loser_Id", "dbo.TeamInfoes", "Id");
            AddForeignKey("dbo.MatchInfoes", "Prediction_Id", "dbo.TeamInfoes", "Id");
            AddForeignKey("dbo.TeamMapStats", "TeamInfo_Id", "dbo.TeamInfoes", "Id");
            AddForeignKey("dbo.TeamInfoes", "MatchInfo_MatchUrl", "dbo.MatchInfoes", "MatchUrl");
        }
    }
}
