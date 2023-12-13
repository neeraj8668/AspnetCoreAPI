using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AspNetCoreWebApi6.Exceptions
{
    public class ApiValidateModelException :   Exception
    {
        public List<ValidationFailure> ValidationErrors { get; }

        public ApiValidateModelException()
        {
            Errors = new Dictionary<string, List<string>>();
        }
        public ApiValidateModelException(List<ValidationFailure> validationErrors)
        {
            ValidationErrors = validationErrors;
            Errors = new Dictionary<string, List<string>>();
            foreach (var key in ValidationErrors)
            {
                Errors.Add(key.PropertyName, new List<string>() { key.ErrorMessage});
            }
        }

        public ApiValidateModelException(ModelStateDictionary modelState)
            : this()
        {
            foreach (string key in modelState.Keys)
            {
                var property = modelState.GetValueOrDefault(key);

                List<string> errors = property.Errors.Select(error => error.ErrorMessage).ToList();

                Errors.Add(key, errors);
            }
        }

        public IDictionary<string, List<string>> Errors { get; }
    } 
}
