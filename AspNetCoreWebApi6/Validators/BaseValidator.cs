using FluentValidation;

namespace AspNetCoreWebApi6.Validators
{
    public abstract class BaseValidator<T> : AbstractValidator<T> where T : class
    {
        public BaseValidator()
        {

        }
        protected bool IsValidName(string name)
        {
            return name.All(Char.IsLetter);
        }
        protected bool IsValidNumeric(string name)
        {
            return name.All(Char.IsNumber);
        }

    }
}
