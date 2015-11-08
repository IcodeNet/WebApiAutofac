using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Autofac.Integration.WebApi;
using NLog;

namespace WebApiAutofac.Filters
{
    public class LoggingActionFilter : IAutofacActionFilter
    {
        readonly ILogger _logger;

        public LoggingActionFilter(ILogger logger)
        {
            _logger = logger;
        }

        public void OnActionExecuting(HttpActionContext actionContext)
        {
            _logger.Debug(actionContext.ActionDescriptor.ActionName);
        }

        public void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            _logger.Debug(actionExecutedContext.ActionContext.ActionDescriptor.ActionName);
        }
    }
}
