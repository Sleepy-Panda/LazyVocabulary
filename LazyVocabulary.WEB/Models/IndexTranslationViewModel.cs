using LazyVocabulary.Common.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LazyVocabulary.WEB.Models
{
    public class IndexTranslationViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Value { get; set; }

        [Required]
        public int DictionaryId { get; set; }

        [Required]
        public ICollection<TranslatedPhrase> Translations { get; set; }

        public IndexTranslationViewModel()
        {
            Translations = new List<TranslatedPhrase>();
        }
    }
}