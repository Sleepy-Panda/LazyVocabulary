using System.ComponentModel.DataAnnotations;

namespace LazyVocabulary.WEB.Models
{
    public class IndexDictionaryViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Название")]
        [MaxLength(64)]
        public string Name { get; set; }

        [Display(Name = "Описание")]
        [MaxLength(256)]
        public string Description { get; set; }

        [Required]
        public string SourceLanguageImagePath { get; set; }

        [Required]
        public string TargetLanguageImagePath { get; set; }

        [Required]
        public int PhrasesCount { get; set; }
    }
}