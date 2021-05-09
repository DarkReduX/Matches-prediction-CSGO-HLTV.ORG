namespace CSGO_match_result_prediction_курсовая_практика_.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FK_TeamMapStats : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.TeamMapStats", name: "TeamInfoId", newName: "TeamInfo_Id");
            RenameIndex(table: "dbo.TeamMapStats", name: "IX_TeamInfoId", newName: "IX_TeamInfo_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.TeamMapStats", name: "IX_TeamInfo_Id", newName: "IX_TeamInfoId");
            RenameColumn(table: "dbo.TeamMapStats", name: "TeamInfo_Id", newName: "TeamInfoId");
        }
    }
}
