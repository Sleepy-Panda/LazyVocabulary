using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace LazyVocabulary.Web.Models
{
    public class EditDictionaryViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Название")]
        [MaxLength(64)]
        [Remote("IsDictionaryNameAvailableForEdit", "Dictionary",
            ErrorMessage = "Словарь с таким названием уже существует.",
            AdditionalFields = "OldName")]
        public string Name { get; set; }

        [Required]
        public string OldName { get; set; }

        [Display(Name = "Описание")]
        [MaxLength(256)]
        public string Description { get; set; }

        [Required]
        public int SourceLanguageId { get; set; }

        [Required]
        public int TargetLanguageId { get; set; }
    }
}