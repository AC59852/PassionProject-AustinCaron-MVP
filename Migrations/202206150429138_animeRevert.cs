namespace PassionProject_AustinCaron_MVP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class animeRevert : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Animes", "AnimeLogo", c => c.String());
            DropColumn("dbo.Animes", "AnimeHasLogo");
            DropColumn("dbo.Animes", "LogoExtension");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Animes", "LogoExtension", c => c.String());
            AddColumn("dbo.Animes", "AnimeHasLogo", c => c.Boolean(nullable: false));
            DropColumn("dbo.Animes", "AnimeLogo");
        }
    }
}
