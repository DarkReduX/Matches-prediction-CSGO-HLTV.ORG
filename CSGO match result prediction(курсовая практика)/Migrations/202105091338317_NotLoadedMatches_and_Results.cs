namespace CSGO_match_result_prediction_курсовая_практика_.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NotLoadedMatches_and_Results : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TeamInfoes", "MatchInfo_MatchUrl", "dbo.MatchInfoes");
            CreateTable(
                "dbo.MatchResults",
                c => new
                    {
                        MatchInfoId = c.String(nullable: false, maxLength: 128),
                        MapScore = c.String(),
                        Loser_Id = c.String(maxLength: 128),
                        Winner_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.MatchInfoId)
                .ForeignKey("dbo.TeamInfoes", t => t.Loser_Id)
                .ForeignKey("dbo.MatchInfoes", t => t.MatchInfoId)
                .ForeignKey("dbo.TeamInfoes", t => t.Winner_Id)
                .Index(t => t.MatchInfoId)
                .Index(t => t.Loser_Id)
                .Index(t => t.Winner_Id);
            
            AddColumn("dbo.MatchInfoes", "StartTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.MatchInfoes", "MatchFormat", c => c.String());
            AddColumn("dbo.MatchInfoes", "Prediction_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.TeamInfoes", "MatchInfo_MatchUrl1", c => c.String(maxLength: 128));
            CreateIndex("dbo.MatchInfoes", "Prediction_Id");
            CreateIndex("dbo.TeamInfoes", "MatchInfo_MatchUrl1");
            AddForeignKey("dbo.MatchInfoes", "Prediction_Id", "dbo.TeamInfoes", "Id");
            AddForeignKey("dbo.TeamInfoes", "MatchInfo_MatchUrl1", "dbo.MatchInfoes", "MatchUrl");
            DropColumn("dbo.MatchInfoes", "DateTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MatchInfoes", "DateTime", c => c.DateTime(nullable: false));
            DropForeignKey("dbo.TeamInfoes", "MatchInfo_MatchUrl1", "dbo.MatchInfoes");
            DropForeignKey("dbo.MatchResults", "Winner_Id", "dbo.TeamInfoes");
            DropForeignKey("dbo.MatchResults", "MatchInfoId", "dbo.MatchInfoes");
            DropForeignKey("dbo.MatchResults", "Loser_Id", "dbo.TeamInfoes");
            DropForeignKey("dbo.MatchInfoes", "Prediction_Id", "dbo.TeamInfoes");
            DropIndex("dbo.MatchResults", new[] { "Winner_Id" });
            DropIndex("dbo.MatchResults", new[] { "Loser_Id" });
            DropIndex("dbo.MatchResults", new[] { "MatchInfoId" });
            DropIndex("dbo.TeamInfoes", new[] { "MatchInfo_MatchUrl1" });
            DropIndex("dbo.MatchInfoes", new[] { "Prediction_Id" });
            DropColumn("dbo.TeamInfoes", "MatchInfo_MatchUrl1");
            DropColumn("dbo.MatchInfoes", "Prediction_Id");
            DropColumn("dbo.MatchInfoes", "MatchFormat");
            DropColumn("dbo.MatchInfoes", "StartTime");
            DropTable("dbo.MatchResults");
            AddForeignKey("dbo.TeamInfoes", "MatchInfo_MatchUrl", "dbo.MatchInfoes", "MatchUrl");
        }
    }
}
