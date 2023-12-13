using FluentValidation;

namespace AspNetCoreWebApi6.Validators
{
    public class ProductValidator : BaseValidator<Product>
    {
        public ProductValidator() {

            RuleFor(p => p.Name).NotEmpty()
                   .MaximumLength(200).WithMessage("{PropertyName} should  have length {MaxLength}.");
        }

    }
}
