using LazyVocabulary.DAL.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace LazyVocabulary.DAL.Mapping
{
    internal class GuiLanguageMapping : EntityTypeConfiguration<GuiLanguage>
    {
        internal GuiLanguageMapping()
        {
            HasKey(l => l.Id);

            Property(l => l.Name)
                .IsRequired()
                .HasMaxLength(64)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute("IX_Name") { IsUnique = true })
                );

            Property(l => l.Code)
                .IsRequired()
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute("IX_Code") { IsUnique = true })
                );

            Property(l => l.FlagImagePath)
                .IsOptional()
                .HasMaxLength(256);

            Property(l => l.IsDefault)
                .IsRequired()
                .HasColumnType("bit");
        }
    }
}
