using LazyVocabulary.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace LazyVocabulary.Web.Models
{
    public class EditProfileViewModel
    {
        public int Id { get; set; }

        [StringLength(64)]
        public string Name { get; set; }

        [StringLength(64)]
        public string Surname { get; set; }

        public string DateOfBirth { get; set; }

        [Required]
        public LocaleLanguage LocaleLanguage { get; set; }
    }
}