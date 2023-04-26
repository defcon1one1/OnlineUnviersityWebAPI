using OnlineUniversityWebAPI.Application.Exceptions;

namespace OnlineUniversityWebAPI.Presentation.Middleware
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger)
        {
            _logger = logger;
        }
        public Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                next.Invoke(context);
            }
            catch(BadRequestException badRequestException)
            {
                context.Response.StatusCode = 400;
                context.Response.WriteAsync(badRequestException.Message);
            }
            catch (NotFoundException notFoundException)
            {
                context.Response.StatusCode = 404;
                context.Response.WriteAsync(notFoundException.Message);
            }
            catch(Exception e)
            {
                _logger.LogError(e, e.Message);
                context.Response.StatusCode = 500;
                context.Response.WriteAsync("Something went wrong");
            }
            return Task.CompletedTask;
        }
    }
}
