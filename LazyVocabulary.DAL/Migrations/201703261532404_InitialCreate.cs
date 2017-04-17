namespace LazyVocabulary.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Dictionaries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 64),
                        Description = c.String(maxLength: 256),
                        CreatedAt = c.DateTime(nullable: false),
                        ViewsCount = c.Int(nullable: false),
                        ApplicationUserId = c.String(nullable: false, maxLength: 128),
                        SourceLanguageId = c.Int(nullable: false),
                        TargetLanguageId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApplicationUsers", t => t.ApplicationUserId)
                .ForeignKey("dbo.Languages", t => t.SourceLanguageId)
                .ForeignKey("dbo.Languages", t => t.TargetLanguageId)
                .Index(t => new { t.ApplicationUserId, t.Name }, unique: true, name: "IX_ApplicationUser_Name")
                .Index(t => t.SourceLanguageId)
                .Index(t => t.TargetLanguageId);
            
            CreateTable(
                "dbo.ApplicationUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApplicationUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.ApplicationUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.ApplicationUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.Subscriptions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatedAt = c.DateTime(nullable: false),
                        TargetId = c.String(maxLength: 128),
                        SubscriberId = c.String(maxLength: 128),
                        ApplicationUser_Id = c.String(maxLength: 128),
                        ApplicationUser_Id1 = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApplicationUsers", t => t.SubscriberId)
                .ForeignKey("dbo.ApplicationUsers", t => t.TargetId)
                .ForeignKey("dbo.ApplicationUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.ApplicationUsers", t => t.ApplicationUser_Id1)
                .Index(t => t.TargetId)
                .Index(t => t.SubscriberId)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id1);
            
            CreateTable(
                "dbo.UserProfiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 64),
                        Surname = c.String(maxLength: 64),
                        DateOfBirth = c.DateTime(),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        PasswordUpdatedAt = c.DateTime(nullable: false),
                        AvatarImagePath = c.String(nullable: false, maxLength: 256),
                        GuiLanguageId = c.Int(),
                        ApplicationUserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GuiLanguages", t => t.GuiLanguageId)
                .ForeignKey("dbo.ApplicationUsers", t => t.ApplicationUserId)
                .Index(t => t.GuiLanguageId)
                .Index(t => t.ApplicationUserId);
            
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
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true)
                .Index(t => t.Code, unique: true);
            
            CreateTable(
                "dbo.Languages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 64),
                        Code = c.String(nullable: false, maxLength: 2, fixedLength: true),
                        FlagImagePath = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true)
                .Index(t => t.Code, unique: true);
            
            CreateTable(
                "dbo.SourcePhrases",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.String(),
                        DictionaryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Dictionaries", t => t.DictionaryId, cascadeDelete: true)
                .Index(t => t.DictionaryId);
            
            CreateTable(
                "dbo.TranslatedPhrases",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.String(),
                        SourcePhraseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SourcePhrases", t => t.SourcePhraseId, cascadeDelete: true)
                .Index(t => t.SourcePhraseId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Dictionaries", "TargetLanguageId", "dbo.Languages");
            DropForeignKey("dbo.TranslatedPhrases", "SourcePhraseId", "dbo.SourcePhrases");
            DropForeignKey("dbo.SourcePhrases", "DictionaryId", "dbo.Dictionaries");
            DropForeignKey("dbo.Dictionaries", "SourceLanguageId", "dbo.Languages");
            DropForeignKey("dbo.Dictionaries", "ApplicationUserId", "dbo.ApplicationUsers");
            DropForeignKey("dbo.UserProfiles", "ApplicationUserId", "dbo.ApplicationUsers");
            DropForeignKey("dbo.UserProfiles", "GuiLanguageId", "dbo.GuiLanguages");
            DropForeignKey("dbo.Subscriptions", "ApplicationUser_Id1", "dbo.ApplicationUsers");
            DropForeignKey("dbo.Subscriptions", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.Subscriptions", "TargetId", "dbo.ApplicationUsers");
            DropForeignKey("dbo.Subscriptions", "SubscriberId", "dbo.ApplicationUsers");
            DropForeignKey("dbo.AspNetUserRoles", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.AspNetUserLogins", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.AspNetUserClaims", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.TranslatedPhrases", new[] { "SourcePhraseId" });
            DropIndex("dbo.SourcePhrases", new[] { "DictionaryId" });
            DropIndex("dbo.Languages", new[] { "Code" });
            DropIndex("dbo.Languages", new[] { "Name" });
            DropIndex("dbo.GuiLanguages", new[] { "Code" });
            DropIndex("dbo.GuiLanguages", new[] { "Name" });
            DropIndex("dbo.UserProfiles", new[] { "ApplicationUserId" });
            DropIndex("dbo.UserProfiles", new[] { "GuiLanguageId" });
            DropIndex("dbo.Subscriptions", new[] { "ApplicationUser_Id1" });
            DropIndex("dbo.Subscriptions", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Subscriptions", new[] { "SubscriberId" });
            DropIndex("dbo.Subscriptions", new[] { "TargetId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.AspNetUserClaims", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Dictionaries", new[] { "TargetLanguageId" });
            DropIndex("dbo.Dictionaries", new[] { "SourceLanguageId" });
            DropIndex("dbo.Dictionaries", "IX_ApplicationUser_Name");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.TranslatedPhrases");
            DropTable("dbo.SourcePhrases");
            DropTable("dbo.Languages");
            DropTable("dbo.GuiLanguages");
            DropTable("dbo.UserProfiles");
            DropTable("dbo.Subscriptions");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.ApplicationUsers");
            DropTable("dbo.Dictionaries");
        }
    }
}
