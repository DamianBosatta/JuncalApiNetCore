using Newtonsoft.Json;

namespace JuncalApi.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception)
            {
                _logger.LogError("Fin De La Excepcion Capturada Desde El Middleware");

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json"; // Establecer el tipo de contenido como JSON

                var errorResponse = new
                {
                    Error = new
                    {
                       
                        SERVER = "Ocurrió un error al procesar la solicitud. Por favor, contacta al equipo de soporte para más ayuda.",
                       
                    }
                };

                var errorMessage = JsonConvert.SerializeObject(errorResponse);
                await context.Response.WriteAsync(errorMessage);
            }
        }
    }

    public static class ExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}
