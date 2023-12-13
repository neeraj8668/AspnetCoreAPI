using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AspNetCoreWebApi6.Exceptions
{
    public class ForbiddenAccessException : Exception
    {
        public ForbiddenAccessException() : base() { }
    }
    
    
   
}
