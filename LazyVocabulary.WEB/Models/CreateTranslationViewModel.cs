using System.ComponentModel.DataAnnotations;

namespace LazyVocabulary.Web.Models
{
    public class CreateTranslationViewModel
    {
        [Required]
        public string Value { get; set; }

        [Required]
        public int DictionaryId { get; set; }
    }
}