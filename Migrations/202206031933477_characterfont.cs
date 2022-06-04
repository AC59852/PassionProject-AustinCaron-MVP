namespace PassionProject_AustinCaron_MVP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class characterfont : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Characters", "CharacterFontRatio", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Characters", "CharacterFontRatio");
        }
    }
}
