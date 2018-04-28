using DotNetGigs.ViewModels.Validations;
using FluentValidation.Attributes;

namespace DotNetGigs.ViewModels
{
    [Validator(typeof(CredentialsViewModelValidator))]
    public class CredentialsViewModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}