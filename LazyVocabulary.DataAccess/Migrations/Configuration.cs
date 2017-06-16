namespace LazyVocabulary.DataAccess.Migrations
{
    using LazyVocabulary.Common.Entities;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<LazyVocabulary.DataAccess.EF.ApplicationContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "LazyVocabulary.DataAccess.EF.ApplicationContext";
        }

        protected override void Seed(LazyVocabulary.DataAccess.EF.ApplicationContext context)
        {
            InitializeLanguages(context);
        }

        private void InitializeLanguages(EF.ApplicationContext db)
        {
            db.Languages.AddOrUpdate(new Language
            {
                Name = "Русский",
                Code = "ru",
                FlagImagePath = "ru.png",
            });

            db.Languages.AddOrUpdate(new Language
            {
                Name = "English",
                Code = "en",
                FlagImagePath = "en.png",
            });

            db.Languages.AddOrUpdate(new Language
            {
                Name = "Deutsch",
                Code = "de",
                FlagImagePath = "de.png",
            });

            db.Languages.AddOrUpdate(new Language
            {
                Name = "French",
                Code = "fr",
                FlagImagePath = "fr.png",
            });

            db.Languages.AddOrUpdate(new Language
            {
                Name = "Español",
                Code = "es",
                FlagImagePath = "es.png",
            });

            db.Languages.AddOrUpdate(new Language
            {
                Name = "Italian",
                Code = "it",
                FlagImagePath = "it.png",
            });

            db.SaveChanges();
        }
    }
}
