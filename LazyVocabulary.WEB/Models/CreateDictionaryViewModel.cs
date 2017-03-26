using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace LazyVocabulary.WEB.Models
{
    public class CreateDictionaryViewModel
    {
        [Required]
        [Display(Name = "Название")]
        [MaxLength(64)]
        [Remote("IsDictionaryNameAvailable", "Dictionary", 
            ErrorMessage = "Словарь с таким именем уже существует.")]
        public string Name { get; set; }

        [Display(Name = "Описание")]
        [MaxLength(256)]
        public string Description { get; set; }

        //public int SourceLanguageId { get; set; }

        //public int TargetLanguageId { get; set; }
    }
}