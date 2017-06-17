using LazyVocabulary.Resources.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace LazyVocabulary.Web.Models
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessageResourceName = "FieldIsRequired",
            ErrorMessageResourceType = typeof(CommonValidationResource))]

        [StringLength(256, MinimumLength = 6,
            ErrorMessageResourceName = "LengthFromTo",
            ErrorMessageResourceType = typeof(CommonValidationResource))]

        [DataType(DataType.Password)]

        public string Password { get; set; }

        [Required(ErrorMessageResourceName = "FieldIsRequired",
            ErrorMessageResourceType = typeof(CommonValidationResource))]

        [StringLength(256, MinimumLength = 6,
            ErrorMessageResourceName = "LengthFromTo",
            ErrorMessageResourceType = typeof(CommonValidationResource))]

        [DataType(DataType.Password)]

        public string NewPassword { get; set; }

        [Required(ErrorMessageResourceName = "FieldIsRequired",
            ErrorMessageResourceType = typeof(CommonValidationResource))]

        [DataType(DataType.Password)]

        [Compare("NewPassword",
            ErrorMessageResourceName = "PasswordsShouldBeEqual",
            ErrorMessageResourceType = typeof(RegisterViewModelResource))]

        public string ConfirmNewPassword { get; set; }
    }
}