using System;

namespace LazyVocabulary.DAL.Entities
{
    public class Comment
    {
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public string Text { get; set; }

        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        public string DictionaryId { get; set; }
        public virtual Dictionary Dictionary { get; set; }
    }
}
