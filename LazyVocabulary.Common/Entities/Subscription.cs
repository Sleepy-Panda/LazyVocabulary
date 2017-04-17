using System;

namespace LazyVocabulary.Common.Entities
{
    public class Subscription
    {
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public string TargetId { get; set; }
        public virtual ApplicationUser Target { get; set; }

        public string SubscriberId { get; set; }
        public virtual ApplicationUser Subscriber { get; set; }

        public Subscription()
        {
            CreatedAt = DateTime.Now;
        }
    }
}
