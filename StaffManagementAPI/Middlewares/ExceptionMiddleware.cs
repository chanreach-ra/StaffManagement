using System.Net;
using StaffManagementAPI.Exceptions;
using StaffManagementAPI.Responses;

namespace StaffManagementAPI.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (CustomException customEx)
            {
                _logger.LogError(customEx, "Custom Exception caught");
                await HandleCustomExceptionAsync(httpContext, customEx);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled Exception caught");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static async Task HandleCustomExceptionAsync(HttpContext context, CustomException customEx)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.NotAcceptable;

            await context.Response.WriteAsJsonAsync(new Response<Task>()
            {
                Code = customEx.StatusCode,
                Message = customEx.Message
            });
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            var message = "The request was declined";
            await context.Response.WriteAsJsonAsync(new Response<Task>()
            {
                Code = HttpStatusCode.BadRequest,
                Message = message
            });
        }
    }
}
