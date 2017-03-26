using LazyVocabulary.DAL.Entities;
using System.Data.Entity;

namespace LazyVocabulary.DAL.EF
{
    internal class DbInitializer : DropCreateDatabaseIfModelChanges<ApplicationContext>
    {
        protected override void Seed(ApplicationContext db)
        {
            InitializeLanguages(db);

            base.Seed(db);
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
        }
    }
}
