using DemoKanban.Models;
using DemoKanban.Services;
using System.Globalization;

namespace DemoKanban.Middlewares
{
    public static class MiddlewareHelpers {
        public static IApplicationBuilder UseGlobalization(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<GlobalizationMiddleware>();
            return builder;
        }

        public static IEndpointRouteBuilder MapIssueEnpints(this IEndpointRouteBuilder builder)
        {
            builder.Map("/api/minApiIssue", (IEmailService emailService) =>
            {
                return KanbanContext.Data.Issues;
            });



            return builder;
        }
    }

    public class GlobalizationMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            //https://localhost:7541/api/issue?Title=Test

            var cultureValue = context.Request.Query["culture"];
            
            if(!string.IsNullOrEmpty(cultureValue))
            {
                var culture = new CultureInfo(cultureValue);

                CultureInfo.CurrentCulture = culture;
                CultureInfo.CurrentUICulture = culture;
            }

            await _next(context);


        }
    }
}
