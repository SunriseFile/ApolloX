using System;
using System.Linq;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Apollo.Master.Controllers.Models
{
    public class ErrorResponseModel
    {
        public string Message { get; set; }
        public string Stack { get; set; }

        public ErrorResponseModel(string message)
        {
            Message = message;
        }

        public ErrorResponseModel(Exception exception)
            : this(exception.Message)
        {
            Stack = exception.StackTrace;
        }

        public ErrorResponseModel(IdentityResult result)
            : this(result.Errors.FirstOrDefault()?.Description)
        {
        }

        public ErrorResponseModel(ModelStateDictionary modelState)
            : this(modelState.Values.FirstOrDefault()?.Errors.FirstOrDefault()?.ErrorMessage)
        {
        }
    }
}
