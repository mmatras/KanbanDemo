using DemoKanban.Models;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DemoKanban.Filters
{
    public class AuditLogFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var ctx = context.HttpContext.RequestServices.GetService<KanbanContext>();

            var constroller = context.RouteData.Values["controller"];
            var action = context.RouteData.Values["action"];

            var req = context.HttpContext.Request;
            var url = $"{req.Host}${req.Path}";

            ctx.AuditLog.Add(new AuditLog(url, $"c:{constroller} a:{action}", "<anonymous>"));
        }
    }
}
