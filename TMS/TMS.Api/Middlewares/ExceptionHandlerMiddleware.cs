using Newtonsoft.Json;
using System.Net;

namespace TMS.Api.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var error = new
            {
                StatusCode = context.Response.StatusCode,
                Message = ex.Message,
                Time = DateTime.UtcNow
            };
            return context.Response.WriteAsync(JsonConvert.SerializeObject(error));
        }
    }
}
