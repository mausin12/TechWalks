using System.Net;

namespace TechWalks.API.Middlewares
{
    public class AppExceptionHandlerMiddleware
    {
        private readonly ILogger<AppExceptionHandlerMiddleware> _logger;
        private readonly RequestDelegate _next;

        public AppExceptionHandlerMiddleware(ILogger<AppExceptionHandlerMiddleware> logger, RequestDelegate next)
        {
            this._logger = logger;
            this._next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid();
                //Log Exception
                _logger.LogError(ex, $"{errorId} : {ex.Message}");

                //Return a custom error Response to the Client
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                httpContext.Response.ContentType = "application/json";

                var error = new
                {
                    Id = errorId,
                    ErrorMessage = "Something went wrong..."
                };
                await httpContext.Response.WriteAsJsonAsync(error);
            }
        }
    }
}
