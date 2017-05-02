using LazyVocabulary.Common.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace LazyVocabulary.DataAccess.Mapping
{
    internal class SubscriptionMapping : EntityTypeConfiguration<Subscription>
    {
        internal SubscriptionMapping()
        {
            HasKey(s => s.Id);

            HasRequired(s => s.Target)
                .WithMany(u => u.TargetSubscriptions);

            HasRequired(s => s.Subscriber)
                .WithMany(u => u.SubscriberSubscriptions);

            Property(s => s.TargetId)
                .IsRequired()
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute("IX_Target_Subscriber", 1) { IsUnique = true })
                );

            Property(s => s.SubscriberId)
                .IsRequired()
                .HasMaxLength(64)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute("IX_Target_Subscriber", 2) { IsUnique = true })
                );

            Property(s => s.CreatedAt)
                .IsRequired();
        }
    }
}
