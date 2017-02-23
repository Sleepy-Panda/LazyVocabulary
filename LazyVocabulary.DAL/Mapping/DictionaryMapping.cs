using LazyVocabulary.DAL.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace LazyVocabulary.DAL.Mapping
{
    internal class DictionaryMapping : EntityTypeConfiguration<Dictionary>
    {
        internal DictionaryMapping()
        {
            HasKey(d => d.Id);

            HasRequired(d => d.SourceLanguage)
                .WithMany(d => d.SourceDictionaries)
                .HasForeignKey(d => d.SourceLanguageId)
                .WillCascadeOnDelete(false);

            HasRequired(d => d.TargetLanguage)
                .WithMany(d => d.TargetDictionaries)
                .HasForeignKey(d => d.TargetLanguageId)
                .WillCascadeOnDelete(false);

            Property(d => d.ApplicationUserId)
                .IsRequired()
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute("IX_ApplicationUser_Name", 1) { IsUnique = true })
                );

            Property(d => d.Name)
                .IsRequired()
                .HasMaxLength(64)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute("IX_ApplicationUser_Name", 2) { IsUnique = true })
                );

            Property(d => d.Description)
                .IsRequired()
                .HasMaxLength(256);

            Property(d => d.CreatedAt)
                .IsRequired();
        }
    }
}
