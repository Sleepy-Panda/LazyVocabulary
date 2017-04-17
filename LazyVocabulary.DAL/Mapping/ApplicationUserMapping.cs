﻿using LazyVocabulary.Common.Entities;
using System.Data.Entity.ModelConfiguration;

namespace LazyVocabulary.DAL.Mapping
{
    internal class ApplicationUserMapping : EntityTypeConfiguration<ApplicationUser>
    {
        internal ApplicationUserMapping()
        {
            ToTable("ApplicationUsers");

            HasOptional(u => u.UserProfile)
                .WithRequired(u => u.ApplicationUser)
                .Map(u => u.MapKey("ApplicationUserId"));
        }
    }
}
