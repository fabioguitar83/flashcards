using Flashcards.Domain.Notifications;
using Newtonsoft.Json;
using System.Net;

namespace Flashcards.API.Middlewares
{
    public class ErrorMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
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
        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;
            var exceptionMessage = exception.Message;

            if (exception is ValidationException) code = HttpStatusCode.BadRequest;  
            else if (exception is UnauthorizedException) code = HttpStatusCode.Unauthorized;            
            else
            {
#if !DEBUG
                exceptionMessage = "Ocorreu um erro inexperado. Tente mais tarde.";
#endif
            }

            var result = JsonConvert.SerializeObject(new { error = exceptionMessage });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }
}
