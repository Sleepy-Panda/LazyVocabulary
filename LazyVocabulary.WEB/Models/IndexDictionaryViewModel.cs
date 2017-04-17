using System;
using System.ComponentModel.DataAnnotations;

namespace LazyVocabulary.WEB.Models
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
        public DateTime CreatedAt { get; set; }
    }
}