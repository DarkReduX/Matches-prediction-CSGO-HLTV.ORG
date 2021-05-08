namespace CSGO_match_result_prediction_курсовая_практика_.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNotLoadedMatches : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MatchNotLoadeds",
                c => new
                    {
                        MatchUrl = c.String(nullable: false, maxLength: 128),
                        StartTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.MatchUrl);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MatchNotLoadeds");
        }
    }
}
