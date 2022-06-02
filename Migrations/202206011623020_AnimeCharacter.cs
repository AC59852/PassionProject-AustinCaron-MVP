namespace PassionProject_AustinCaron_MVP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AnimeCharacter : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Animes",
                c => new
                    {
                        AnimeId = c.Int(nullable: false, identity: true),
                        AnimeName = c.String(),
                        AnimeAbbr = c.String(),
                        AnimeDescription = c.String(),
                    })
                .PrimaryKey(t => t.AnimeId);
            
            CreateTable(
                "dbo.Characters",
                c => new
                    {
                        CharacterId = c.Int(nullable: false, identity: true),
                        CharacterName = c.String(),
                        CharacterBio = c.String(),
                        CharacterWeight = c.Int(nullable: false),
                        CharacterHeight = c.String(),
                        CharacterAffiliation = c.String(),
                        AnimeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CharacterId)
                .ForeignKey("dbo.Animes", t => t.AnimeId, cascadeDelete: true)
                .Index(t => t.AnimeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Characters", "AnimeId", "dbo.Animes");
            DropIndex("dbo.Characters", new[] { "AnimeId" });
            DropTable("dbo.Characters");
            DropTable("dbo.Animes");
        }
    }
}
