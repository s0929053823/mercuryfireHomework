
using System.Net;
using System.Text.Json;

namespace Homework.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
           
                await HandleExceptionAsync(httpContext, ex);  
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

 
            var response = new { message = "An unexpected error occurred." };
            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
