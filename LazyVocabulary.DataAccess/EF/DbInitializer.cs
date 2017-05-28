using LazyVocabulary.Common.Entities;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace LazyVocabulary.DataAccess.EF
{
    //internal class DbInitializer : DropCreateDatabaseAlways<ApplicationContext>
    //{
    //    protected override void Seed(ApplicationContext db)
    //    {
    //        InitializeLanguages(db);
    //    }

    //    private void InitializeLanguages(ApplicationContext db)
    //    {
    //        db.Languages.AddOrUpdate(new Language {
    //            Name = "Русский",
    //            Code = "ru",
    //            FlagImagePath = "ru.png",
    //        });

    //        db.Languages.AddOrUpdate(new Language
    //        {
    //            Name = "English",
    //            Code = "en",
    //            FlagImagePath = "en.png",
    //        });

    //        db.Languages.AddOrUpdate(new Language
    //        {
    //            Name = "Deutsch",
    //            Code = "de",
    //            FlagImagePath = "de.png",
    //        });

    //        db.Languages.AddOrUpdate(new Language
    //        {
    //            Name = "French",
    //            Code = "fr",
    //            FlagImagePath = "fr.png",
    //        });

    //        db.Languages.AddOrUpdate(new Language
    //        {
    //            Name = "Español",
    //            Code = "es",
    //            FlagImagePath = "es.png",
    //        });

    //        db.Languages.AddOrUpdate(new Language
    //        {
    //            Name = "Italian",
    //            Code = "it",
    //            FlagImagePath = "it.png",
    //        });

    //        db.SaveChanges();            
    //    }
    //}
}
