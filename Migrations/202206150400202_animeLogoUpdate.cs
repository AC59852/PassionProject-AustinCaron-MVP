namespace PassionProject_AustinCaron_MVP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class animeLogoUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Animes", "AnimeHasLogo", c => c.Boolean(nullable: false));
            AddColumn("dbo.Animes", "LogoExtension", c => c.String());
            DropColumn("dbo.Animes", "AnimeLogo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Animes", "AnimeLogo", c => c.String());
            DropColumn("dbo.Animes", "LogoExtension");
            DropColumn("dbo.Animes", "AnimeHasLogo");
        }
    }
}
