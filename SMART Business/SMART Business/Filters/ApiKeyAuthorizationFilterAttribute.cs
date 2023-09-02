using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Reflection.Metadata.Ecma335;

namespace SMART_Business.Filters
{
    public class ApiKeyAuthorizationFilterAttribute : ActionFilterAttribute
    {
        private readonly IConfiguration _configuration;
        public ApiKeyAuthorizationFilterAttribute(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            
            var headers = context.HttpContext.Request.Headers;
            StringValues key;
            if (headers.TryGetValue("Authorization", out key))
            {
                string? apiKey = key.FirstOrDefault();
                
                if (!IsValidApiKey(key))
                {
                    context.ModelState.AddModelError("ApiKey", "The key is invalid!");
                    var problemDetails = new ValidationProblemDetails(context.ModelState)
                    {
                        Status = StatusCodes.Status400BadRequest
                    };
                    context.Result = new BadRequestObjectResult(problemDetails);
                }
            }
            else
            {
                context.ModelState.AddModelError("ApiKey", "In order to get access you should to enter an api key");
                var problemDetails = new ValidationProblemDetails(context.ModelState)
                {
                    Status = StatusCodes.Status400BadRequest
                };
                context.Result = new BadRequestObjectResult(problemDetails);
            }
        }

        public bool IsValidApiKey (string apiKey)
        {
            string pass = _configuration["ApiKey"];
            bool response = apiKey.Equals(pass)==false?false:true;
            return response;
        }
    }

    public class RequestLoggerActionFilter : ActionFilterAttribute
    {
        private readonly IConfiguration _configuration;
        public RequestLoggerActionFilter(
            IConfiguration configuration
           )
        {
            
            _configuration = configuration;
            
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {

            base.OnActionExecuting(context);
        }
        public override void OnResultExecuting(ResultExecutingContext context)
        {
           
            var connString = _configuration["ApiKey"];
        }
    }
}
