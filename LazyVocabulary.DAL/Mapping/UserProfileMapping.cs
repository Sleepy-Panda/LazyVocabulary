using LazyVocabulary.DAL.Entities;
using System.Data.Entity.ModelConfiguration;

namespace LazyVocabulary.DAL.Mapping
{
    internal class UserProfileMapping : EntityTypeConfiguration<UserProfile>
    {
        internal UserProfileMapping()
        {
            HasKey(u => u.Id);

            HasRequired(u => u.ApplicationUser)
                .WithOptional(u => u.UserProfile);

            Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(64);

            Property(u => u.Surname)
                .IsRequired()
                .HasMaxLength(64);

            Property(u => u.DateOfBirth)
                .IsRequired();

            Property(u => u.CreatedAt)
                .IsRequired();

            Property(u => u.UpdatedAt)
                .IsOptional();

            Property(u => u.PasswordUpdatedAt)
                .IsRequired();

            Property(u => u.AvatarImagePath)
                .IsRequired()
                .HasMaxLength(256);
        }
    }
}
