namespace LazyVocabulary.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedAtForDictionaryEtc : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Dictionaries", "UpdatedAt", c => c.DateTime(nullable: false));
            AlterColumn("dbo.SourcePhrases", "Value", c => c.String(nullable: false, maxLength: 64));
            AlterColumn("dbo.TranslatedPhrases", "Value", c => c.String(nullable: false, maxLength: 64));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TranslatedPhrases", "Value", c => c.String());
            AlterColumn("dbo.SourcePhrases", "Value", c => c.String());
            DropColumn("dbo.Dictionaries", "UpdatedAt");
        }
    }
}
