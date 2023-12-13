using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using AspNetCoreWebApi6.Response;
using System;

namespace AspNetCoreWebApi6
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class ValidateModelAttribute : Attribute, IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var modelType = context.ActionDescriptor.Parameters
                .FirstOrDefault()?.ParameterType;

            if (modelType != null)
            {
                var model = context.ActionArguments.Values.FirstOrDefault(arg => modelType.IsInstanceOfType(arg));

                if (model != null)
                {
                    var validator = (IValidator)context.HttpContext.RequestServices.GetService(typeof(IValidator<>).MakeGenericType(modelType));

                    if (validator != null)
                    {
                        var validationResult = validator.Validate(new ValidationContext<object>(model));

                        if (!validationResult.IsValid)
                        {
                            var erroInfo=new Dictionary<string, string>();
                            foreach (var error in validationResult.Errors)
                            {
                                context.ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                                erroInfo.Add(error.PropertyName, error.ErrorMessage);
                            }
                            var details = ServiceResult.Failed(erroInfo, ServiceError.Validation);
                            context.Result = new BadRequestObjectResult(details);
                        }
                    }
                }
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Do nothing here
        }

    }
}
