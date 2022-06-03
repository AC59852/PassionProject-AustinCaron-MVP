namespace PassionProject_AustinCaron_MVP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class animelogo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Animes", "AnimeLogo", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Animes", "AnimeLogo");
        }
    }
}
