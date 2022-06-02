namespace PassionProject_AustinCaron_MVP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class animecharacterupdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Animes", "AnimeIcon", c => c.String());
            AddColumn("dbo.Animes", "AnimeBck", c => c.String());
            AddColumn("dbo.Characters", "CharacterAge", c => c.Int(nullable: false));
            AddColumn("dbo.Characters", "CharacterIcon", c => c.String());
            AddColumn("dbo.Characters", "CharacterImage", c => c.String());
            AddColumn("dbo.Characters", "CharacterBck", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Characters", "CharacterBck");
            DropColumn("dbo.Characters", "CharacterImage");
            DropColumn("dbo.Characters", "CharacterIcon");
            DropColumn("dbo.Characters", "CharacterAge");
            DropColumn("dbo.Animes", "AnimeBck");
            DropColumn("dbo.Animes", "AnimeIcon");
        }
    }
}
