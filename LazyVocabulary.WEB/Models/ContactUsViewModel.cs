using LazyVocabulary.Resources.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace LazyVocabulary.Web.Models
{
    public class ContactUsViewModel
    {
        [Required(ErrorMessageResourceName = "FieldIsRequired",
            ErrorMessageResourceType = typeof(CommonValidationResource))]

        [StringLength(256, MinimumLength = 6,
            ErrorMessageResourceName = "LengthFromTo",
            ErrorMessageResourceType = typeof(CommonValidationResource))]

        [EmailAddress(ErrorMessageResourceName = "EmailIsInvalid",
            ErrorMessageResourceType = typeof(CommonValidationResource))]

        public string Email { get; set; }

        [Required(ErrorMessageResourceName = "FieldIsRequired",
            ErrorMessageResourceType = typeof(CommonValidationResource))]

        [StringLength(256, MinimumLength = 6,
            ErrorMessageResourceName = "LengthFromTo",
            ErrorMessageResourceType = typeof(CommonValidationResource))]

        public string Subject { get; set; }

        [Required(ErrorMessageResourceName = "FieldIsRequired",
            ErrorMessageResourceType = typeof(CommonValidationResource))]

        [StringLength(1000, MinimumLength = 6,
            ErrorMessageResourceName = "LengthFromTo",
            ErrorMessageResourceType = typeof(CommonValidationResource))]

        public string Text { get; set; }
    }
}