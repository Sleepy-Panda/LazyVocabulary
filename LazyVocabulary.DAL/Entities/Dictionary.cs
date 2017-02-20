﻿using System;

namespace LazyVocabulary.DAL.Entities
{
    public class Dictionary
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public Guid ApplicationUserId { get; set; }
        public int SourceLanguageId { get; set; }
        public int TargetLanguageId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual Language SourceLanguage { get; set; }
        public virtual Language TargetLanguage { get; set; }
    }
}
