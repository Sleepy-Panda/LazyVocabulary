using LazyVocabulary.DAL.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;

namespace LazyVocabulary.DAL.EF
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<GuiLanguage> GuiLanguages { get; set; }
        public DbSet<Dictionary> Dictionaries { get; set; }

        public ApplicationContext(string conectionString) 
            : base(conectionString)
        { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            GetLanguageMapping(modelBuilder);
            GetGuiLanguageMapping(modelBuilder);
            GetUserProfileMapping(modelBuilder);
            GetDictionaryMapping(modelBuilder);
        }

        protected void GetLanguageMapping(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Language>()
                .HasKey(l => l.Id);

            modelBuilder.Entity<Language>().Property(l => l.Name)
                .IsRequired()
                .HasMaxLength(64)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute("IX_Name") { IsUnique = true })
                );

            modelBuilder.Entity<Language>().Property(l => l.Code)
                .IsRequired()
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute("IX_Code") { IsUnique = true })
                );

            modelBuilder.Entity<Language>().Property(l => l.FlagImagePath)
                .IsOptional()
                .HasMaxLength(256);
        }

        protected void GetGuiLanguageMapping(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GuiLanguage>()
                .HasKey(l => l.Id);

            modelBuilder.Entity<GuiLanguage>().Property(l => l.Name)
                .IsRequired()
                .HasMaxLength(64)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute("IX_Name") { IsUnique = true })
                );

            modelBuilder.Entity<GuiLanguage>().Property(l => l.Code)
                .IsRequired()
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute("IX_Code") { IsUnique = true })
                );

            modelBuilder.Entity<GuiLanguage>().Property(l => l.FlagImagePath)
                .IsOptional()
                .HasMaxLength(256);
        }

        protected void GetUserProfileMapping(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserProfile>()
                .HasKey(u => u.Id);

            modelBuilder.Entity<UserProfile>().Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(64);

            modelBuilder.Entity<UserProfile>().Property(u => u.Surname)
                .IsRequired()
                .HasMaxLength(64);

            modelBuilder.Entity<UserProfile>().Property(u => u.DateOfBirth)
                .IsRequired();

            modelBuilder.Entity<UserProfile>().Property(u => u.AvatarImagePath)
                .IsOptional()
                .HasMaxLength(256);

            modelBuilder.Entity<UserProfile>().HasRequired(u => u.ApplicationUser).WithOptional(au => au.UserProfile);
        }

        protected void GetDictionaryMapping(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dictionary>()
                .HasKey(d => d.Id);

            modelBuilder.Entity<Dictionary>()
                .HasRequired(d => d.SourceLanguage)
                .WithMany(d => d.SourceDictionaries)
                .HasForeignKey(d => d.SourceLanguageId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Dictionary>()
                .HasRequired(d => d.TargetLanguage)
                .WithMany(d => d.TargetDictionaries)
                .HasForeignKey(d => d.TargetLanguageId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Dictionary>().Property(d => d.ApplicationUserId)
                .IsRequired()
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute("IX_ApplicationUser_Name", 1) { IsUnique = true })
                );

            modelBuilder.Entity<Dictionary>().Property(d => d.Name)
                .IsRequired()
                .HasMaxLength(64)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute("IX_ApplicationUser_Name", 2) { IsUnique = true })
                );

            modelBuilder.Entity<Dictionary>().Property(d => d.Description)
                .IsRequired()
                .HasMaxLength(256);

            modelBuilder.Entity<Dictionary>().Property(d => d.CreatedAt)
                .IsRequired();
        }
    }
}
