namespace PassionProject_AustinCaron_MVP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class animeImageRevert : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Animes", "AnimeHasPic");
            DropColumn("dbo.Animes", "PicExtension");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Animes", "PicExtension", c => c.String());
            AddColumn("dbo.Animes", "AnimeHasPic", c => c.Boolean(nullable: false));
        }
    }
}
