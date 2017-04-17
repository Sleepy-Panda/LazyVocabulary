namespace LazyVocabulary.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserLocale : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserProfiles", "GuiLanguageId", "dbo.GuiLanguages");
            DropIndex("dbo.UserProfiles", new[] { "GuiLanguageId" });
            DropIndex("dbo.GuiLanguages", new[] { "Name" });
            DropIndex("dbo.GuiLanguages", new[] { "Code" });
            AddColumn("dbo.UserProfiles", "Locale", c => c.Int(nullable: false));
            DropColumn("dbo.UserProfiles", "GuiLanguageId");
            DropTable("dbo.GuiLanguages");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.GuiLanguages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 64),
                        Code = c.String(nullable: false, maxLength: 2, fixedLength: true),
                        FlagImagePath = c.String(maxLength: 256),
                        IsDefault = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.UserProfiles", "GuiLanguageId", c => c.Int());
            DropColumn("dbo.UserProfiles", "Locale");
            CreateIndex("dbo.GuiLanguages", "Code", unique: true);
            CreateIndex("dbo.GuiLanguages", "Name", unique: true);
            CreateIndex("dbo.UserProfiles", "GuiLanguageId");
            AddForeignKey("dbo.UserProfiles", "GuiLanguageId", "dbo.GuiLanguages", "Id");
        }
    }
}
