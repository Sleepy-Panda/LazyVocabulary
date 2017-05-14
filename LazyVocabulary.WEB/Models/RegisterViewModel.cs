using LazyVocabulary.Resources.ViewModels;
using System.ComponentModel.DataAnnotations;
using Remote = System.Web.Mvc.RemoteAttribute;

namespace LazyVocabulary.Web.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessageResourceName = "FieldIsRequired",
            ErrorMessageResourceType = typeof(CommonValidationResource))]

        [Remote("IsUserNameAvailable", "Account", 
            ErrorMessageResourceName = "UserNameIsUnavailable",
            ErrorMessageResourceType = typeof(RegisterViewModelResource))]

        public string UserName { get; set; }

        [Required(ErrorMessageResourceName = "FieldIsRequired",
            ErrorMessageResourceType = typeof(CommonValidationResource))]

        [EmailAddress(ErrorMessageResourceName = "EmailIsInvalid",
            ErrorMessageResourceType = typeof(CommonValidationResource))]

        [Remote("IsEmailAvailable", "Account",
            ErrorMessageResourceName = "EmailIsUnavailable",
            ErrorMessageResourceType = typeof(RegisterViewModelResource))]

        public string Email { get; set; }

        [Required(ErrorMessageResourceName = "FieldIsRequired",
            ErrorMessageResourceType = typeof(CommonValidationResource))]

        [DataType(DataType.Password)]

        public string Password { get; set; }

        [Required(ErrorMessageResourceName = "FieldIsRequired",
            ErrorMessageResourceType = typeof(CommonValidationResource))]

        [DataType(DataType.Password)]

        [Compare("Password",
            ErrorMessageResourceName = "PasswordsShouldBeEqual",
            ErrorMessageResourceType = typeof(RegisterViewModelResource))]

        public string ConfirmPassword { get; set; }
    }
}