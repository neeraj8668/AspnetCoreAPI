using FluentValidation;

namespace AspNetCoreWebApi6.Validators
{
    public class UserValidator : BaseValidator<User>
    {
        public UserValidator() {

            RuleFor(p => p.Name).NotEmpty()
                   .MaximumLength(50).WithMessage("{PropertyName} should  have length {MaxLength}.");
            
            RuleFor(p => p.Email).EmailAddress();
        }

    }
}
