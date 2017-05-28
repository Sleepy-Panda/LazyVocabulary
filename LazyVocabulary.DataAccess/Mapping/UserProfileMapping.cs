using LazyVocabulary.Common.Entities;
using System.Data.Entity.ModelConfiguration;

namespace LazyVocabulary.DataAccess.Mapping
{
    internal class UserProfileMapping : EntityTypeConfiguration<UserProfile>
    {
        internal UserProfileMapping()
        {
            HasKey(u => u.Id);

            Property(u => u.Name)
                .IsOptional()
                .HasMaxLength(64);

            Property(u => u.Surname)
                .IsOptional()
                .HasMaxLength(64);

            Property(u => u.DateOfBirth)
                .IsOptional();

            Property(u => u.CreatedAt)
                .IsRequired();

            Property(u => u.UpdatedAt)
                .IsRequired();

            Property(u => u.PasswordUpdatedAt)
                .IsRequired();

            Property(u => u.Locale)
                .IsRequired();
        }
    }
}
