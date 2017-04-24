using LazyVocabulary.Common.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LazyVocabulary.WEB.Models
{
    public class IndexSourcePhrasesViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Value { get; set; }

        [Required]
        public int DictionaryId { get; set; }

        [Required]
        public ICollection<TranslatedPhrase> Translations { get; set; }

        public IndexSourcePhrasesViewModel()
        {
            Translations = new List<TranslatedPhrase>();
        }
    }
}