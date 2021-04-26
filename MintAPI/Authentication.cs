using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace MintAPI
{
    [AttributeUsage(validOn: AttributeTargets.Class)]
    public class ApiKeyAttribute : Attribute, IAsyncActionFilter
    {
        private const string apiKeyValue = "x-api-key";

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue(apiKeyValue, out var extractedApiKey))
            {
                context.Result = new ContentResult() { StatusCode = 401, Content = "Missing API Key" };

                return;
            }

            var appSettings = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
            var apiKey = appSettings.GetValue<string>(apiKeyValue);

            if (!apiKey.Equals(extractedApiKey))
            {
                context.Result = new ContentResult() { StatusCode = 401, Content = "Invalid API Key" };
                return;
            }

            await next();
        }
                
    }
}
