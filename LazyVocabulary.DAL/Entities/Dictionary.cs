using System;
using System.Collections.Generic;

namespace LazyVocabulary.DAL.Entities
{
    public class Dictionary
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime CreatedAt { get; set; }

        public int ViewsCount { get; set; }

        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        public int SourceLanguageId { get; set; }
        public virtual Language SourceLanguage { get; set; }

        public int TargetLanguageId { get; set; }
        public virtual Language TargetLanguage { get; set; }

        public virtual ICollection<SourcePhrase> SourcePhrases { get; set; }

        public Dictionary()
        {
            CreatedAt = DateTime.Now;
            SourcePhrases = new List<SourcePhrase>();
        }
    }
}
