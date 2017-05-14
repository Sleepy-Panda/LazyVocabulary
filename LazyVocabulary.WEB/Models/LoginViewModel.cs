using System.ComponentModel.DataAnnotations;
using LazyVocabulary.Resources.ViewModels;

namespace LazyVocabulary.Web.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessageResourceName = "FieldIsRequired",
            ErrorMessageResourceType = typeof(CommonValidationResource))]
        public string Login { get; set; }

        [Required(ErrorMessageResourceName = "FieldIsRequired",
            ErrorMessageResourceType = typeof(CommonValidationResource))]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}