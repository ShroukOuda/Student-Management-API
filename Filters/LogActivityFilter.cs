using System.Diagnostics.Eventing.Reader;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Student_Management_API.Filters;

public class LogActivityFilter : IAsyncActionFilter
{
    private readonly ILogger<LogActivityFilter> _logger;
    
    public LogActivityFilter(ILogger<LogActivityFilter> logger)
    {
        _logger = logger;
    }
  

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        _logger.LogInformation($"(Async) Executing Action {context.ActionDescriptor.DisplayName} OnController {context.Controller} with arguments {JsonSerializer.Serialize(context.ActionArguments)}");
    
        await next();
        
        _logger.LogInformation($"(Async) Action {context.ActionDescriptor.DisplayName} executed on controller {context.Controller}");
    }
}