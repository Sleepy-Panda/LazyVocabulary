using LazyVocabulary.Common.Entities;
using System.Data.Entity;

namespace LazyVocabulary.DAL.EF
{
    internal class DbInitializer : DropCreateDatabaseAlways<ApplicationContext>
    {
        protected override void Seed(ApplicationContext db)
        {
            InitializeLanguages(db);
        }

        private void InitializeLanguages(ApplicationContext db)
        {
            db.Languages.Add(new Language {
                Name = "Русский",
                Code = "ru",
                FlagImagePath = "ru.png",
            });

            db.Languages.Add(new Language
            {
                Name = "English",
                Code = "en",
                FlagImagePath = "en.png",
            });

            db.Languages.Add(new Language
            {
                Name = "Deutsch",
                Code = "de",
                FlagImagePath = "de.png",
            });

            db.Languages.Add(new Language
            {
                Name = "French",
                Code = "fr",
                FlagImagePath = "fr.png",
            });

            db.Languages.Add(new Language
            {
                Name = "Español",
                Code = "es",
                FlagImagePath = "es.png",
            });

            db.Languages.Add(new Language
            {
                Name = "Italian",
                Code = "it",
                FlagImagePath = "it.png",
            });

            db.SaveChanges();            
        }
    }
}
