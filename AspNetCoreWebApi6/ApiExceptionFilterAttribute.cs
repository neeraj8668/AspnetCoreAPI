using AspNetCoreWebApi6.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AspNetCoreWebApi6
{
    public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;
        private readonly ILogger<ApiExceptionFilterAttribute> _logger;

        public ApiExceptionFilterAttribute(ILogger<ApiExceptionFilterAttribute> logger)
        {
            _logger = logger;

            _exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
        {
            { typeof(ApiValidateModelException), HandleValidationException },
            { typeof(NotFoundException), HandleNotFoundException },
            { typeof(UnauthorizedAccessException), HandleUnauthorizedAccessException },
            { typeof(ForbiddenAccessException), HandleForbiddenAccessException },
        };
        }

        public override void OnException(ExceptionContext context)
        {
            HandleException(context);

            base.OnException(context);
        }

        private void HandleValidationException(ExceptionContext context)
        {
            if (context.Exception is ApiValidateModelException exception)
            {
                var details = ServiceResult.Failed(exception.Errors, ServiceError.Validation);

                context.Result = new BadRequestObjectResult(details);
            }

            context.ExceptionHandled = true;
        }

        private void HandleException(ExceptionContext context)
        {
            Type type = context.Exception.GetType();
           
            if (_exceptionHandlers.ContainsKey(type))
            {
                _exceptionHandlers[type].Invoke(context);
                return;
            }

            if (!context.ModelState.IsValid)
            {
                HandleInvalidModelStateException(context);
                return;
            }

            HandleUnknownException(context);
        }

        private static void HandleUnknownException(ExceptionContext context)
        {
            var details = ServiceResult.Failed(new ServiceError(context.Exception.Message + context.Exception?.InnerException?.Message, 500));

            context.Result = new ObjectResult(details)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };

            context.ExceptionHandled = true;
        }

        private static void HandleInvalidModelStateException(ExceptionContext context)
        {
            var exception = new ApiValidateModelException(context.ModelState);

            context.Result = new BadRequestObjectResult(ServiceResult.Failed(exception.Errors, ServiceError.ValidationFormat));

            context.ExceptionHandled = true;
        }

        private void HandleNotFoundException(ExceptionContext context)
        {
            var details = ServiceResult.Failed(ServiceError.CustomMessage(context.Exception is NotFoundException exception ? exception.Message : ServiceError.NotFound.ToString()));

            context.Result = new NotFoundObjectResult(details);

            context.ExceptionHandled = true;
        }

        private void HandleForbiddenAccessException(ExceptionContext context)
        {
            var details = ServiceResult.Failed(ServiceError.ForbiddenError);

            context.Result = new ObjectResult(details)
            {
                StatusCode = StatusCodes.Status403Forbidden
            };

            context.ExceptionHandled = true;
        }

        private void HandleUnauthorizedAccessException(ExceptionContext context)
        {
            var details = ServiceResult.Failed(ServiceError.ForbiddenError);

            context.Result = new UnauthorizedObjectResult(details);

            context.ExceptionHandled = true;
        }
    }
}
