﻿using LazyVocabulary.Common.Entities;
using LazyVocabulary.DataAccess.Mapping;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace LazyVocabulary.DataAccess.EF
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Language> Languages { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Dictionary> Dictionaries { get; set; }
        public DbSet<SourcePhrase> SourcePhrases { get; set; }
        public DbSet<TranslatedPhrase> TranslatedPhrases { get; set; }

        static ApplicationContext()
        {

        }

        public ApplicationContext()
        {

        }

        public ApplicationContext(string conectionString) 
            : base(conectionString)
        {
            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new LanguageMapping());
            modelBuilder.Configurations.Add(new UserProfileMapping());
            modelBuilder.Configurations.Add(new DictionaryMapping());
            modelBuilder.Configurations.Add(new ApplicationUserMapping());
            modelBuilder.Configurations.Add(new SourcePhraseMapping());
            modelBuilder.Configurations.Add(new TranslatedPhraseMapping());
        }
    }
}
