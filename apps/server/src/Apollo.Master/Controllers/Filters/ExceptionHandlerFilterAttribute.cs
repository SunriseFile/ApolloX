using Apollo.Master.Controllers.Models;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Apollo.Master.Controllers.Filters
{
    public class ExceptionHandlerFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var ex = context.Exception;
            var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<ExceptionHandlerFilterAttribute>>();
            var env = context.HttpContext.RequestServices.GetRequiredService<IHostingEnvironment>();

            logger.LogError(ex, ex.Message);

            var response = new ErrorResponseModel("An unhandled server error has occurred");

            if (env.IsDevelopment())
            {
                response = new ErrorResponseModel(ex);
            }

            context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Result = new ObjectResult(response);
        }
    }
}
