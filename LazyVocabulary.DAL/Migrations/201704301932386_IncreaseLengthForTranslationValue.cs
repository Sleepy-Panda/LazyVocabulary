namespace LazyVocabulary.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IncreaseLengthForTranslationValue : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SourcePhrases", "Value", c => c.String(nullable: false, maxLength: 256));
            AlterColumn("dbo.TranslatedPhrases", "Value", c => c.String(nullable: false, maxLength: 256));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TranslatedPhrases", "Value", c => c.String(nullable: false, maxLength: 64));
            AlterColumn("dbo.SourcePhrases", "Value", c => c.String(nullable: false, maxLength: 64));
        }
    }
}
