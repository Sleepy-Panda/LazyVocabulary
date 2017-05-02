using System;
using System.ComponentModel.DataAnnotations;

namespace LazyVocabulary.Web.Models
{
    public class IndexDictionaryViewModel
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
        public int PhrasesCount { get; set; }

        [Required]
        public string CreatedAt { get; set; }

        [Required]
        public string UpdatedAt { get; set; }
    }
}