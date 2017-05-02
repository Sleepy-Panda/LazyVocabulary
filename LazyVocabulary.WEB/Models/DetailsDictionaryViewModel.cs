using LazyVocabulary.Common.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LazyVocabulary.Web.Models
{
    public class DetailsDictionaryViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string SourceLanguageImagePath { get; set; }

        [Required]
        public string TargetLanguageImagePath { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public ICollection<SourcePhrase> SourcePhrases { get; set; }

        public DetailsDictionaryViewModel()
        {
            SourcePhrases = new List<SourcePhrase>();
        }
    }
}