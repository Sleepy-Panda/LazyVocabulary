﻿using System;
using System.Collections.Generic;

namespace LazyVocabulary.Common.Entities
{
    public class Dictionary
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

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
            CreatedAt = UpdatedAt = DateTime.Now;
            SourcePhrases = new List<SourcePhrase>();
        }
    }
}
