namespace CSGO_match_result_prediction_курсовая_практика_.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MatchInfoNewKey : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TeamInfoes", "MatchInfo_MatchUrl", "dbo.MatchInfoes");
            DropForeignKey("dbo.MatchResults", "MatchInfoId", "dbo.MatchInfoes");
            DropForeignKey("dbo.TeamInfoes", "MatchInfo_MatchUrl1", "dbo.MatchInfoes");
            DropIndex("dbo.TeamInfoes", new[] { "MatchInfo_MatchUrl" });
            DropIndex("dbo.TeamInfoes", new[] { "MatchInfo_MatchUrl1" });
            DropIndex("dbo.MatchResults", new[] { "MatchInfoId" });
            RenameColumn(table: "dbo.TeamInfoes", name: "MatchInfo_MatchUrl1", newName: "MatchInfo_Id1");
            RenameColumn(table: "dbo.TeamInfoes", name: "MatchInfo_MatchUrl", newName: "MatchInfo_Id");
            DropPrimaryKey("dbo.MatchInfoes");
            DropPrimaryKey("dbo.MatchResults");
            AddColumn("dbo.MatchInfoes", "Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.MatchInfoes", "MatchUrl", c => c.String());
            AlterColumn("dbo.TeamInfoes", "MatchInfo_Id", c => c.Guid());
            AlterColumn("dbo.TeamInfoes", "MatchInfo_Id1", c => c.Guid());
            AlterColumn("dbo.MatchResults", "MatchInfoId", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.MatchInfoes", "Id");
            AddPrimaryKey("dbo.MatchResults", "MatchInfoId");
            CreateIndex("dbo.TeamInfoes", "MatchInfo_Id");
            CreateIndex("dbo.TeamInfoes", "MatchInfo_Id1");
            CreateIndex("dbo.MatchResults", "MatchInfoId");
            AddForeignKey("dbo.TeamInfoes", "MatchInfo_Id", "dbo.MatchInfoes", "Id");
            AddForeignKey("dbo.MatchResults", "MatchInfoId", "dbo.MatchInfoes", "Id");
            AddForeignKey("dbo.TeamInfoes", "MatchInfo_Id1", "dbo.MatchInfoes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TeamInfoes", "MatchInfo_Id1", "dbo.MatchInfoes");
            DropForeignKey("dbo.MatchResults", "MatchInfoId", "dbo.MatchInfoes");
            DropForeignKey("dbo.TeamInfoes", "MatchInfo_Id", "dbo.MatchInfoes");
            DropIndex("dbo.MatchResults", new[] { "MatchInfoId" });
            DropIndex("dbo.TeamInfoes", new[] { "MatchInfo_Id1" });
            DropIndex("dbo.TeamInfoes", new[] { "MatchInfo_Id" });
            DropPrimaryKey("dbo.MatchResults");
            DropPrimaryKey("dbo.MatchInfoes");
            AlterColumn("dbo.MatchResults", "MatchInfoId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.TeamInfoes", "MatchInfo_Id1", c => c.String(maxLength: 128));
            AlterColumn("dbo.TeamInfoes", "MatchInfo_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.MatchInfoes", "MatchUrl", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.MatchInfoes", "Id");
            AddPrimaryKey("dbo.MatchResults", "MatchInfoId");
            AddPrimaryKey("dbo.MatchInfoes", "MatchUrl");
            RenameColumn(table: "dbo.TeamInfoes", name: "MatchInfo_Id", newName: "MatchInfo_MatchUrl");
            RenameColumn(table: "dbo.TeamInfoes", name: "MatchInfo_Id1", newName: "MatchInfo_MatchUrl1");
            CreateIndex("dbo.MatchResults", "MatchInfoId");
            CreateIndex("dbo.TeamInfoes", "MatchInfo_MatchUrl1");
            CreateIndex("dbo.TeamInfoes", "MatchInfo_MatchUrl");
            AddForeignKey("dbo.TeamInfoes", "MatchInfo_MatchUrl1", "dbo.MatchInfoes", "MatchUrl");
            AddForeignKey("dbo.MatchResults", "MatchInfoId", "dbo.MatchInfoes", "MatchUrl");
            AddForeignKey("dbo.TeamInfoes", "MatchInfo_MatchUrl", "dbo.MatchInfoes", "MatchUrl");
        }
    }
}
