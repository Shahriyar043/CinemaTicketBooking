using CinemaTicketBookingApi.Exceptions;
using CinemaTicketBookingApi.Models;
using Newtonsoft.Json;
using System.Net;

namespace CinemaTicketBookingApi.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ErrorHandlingMiddleware> logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An unhandled exception occurred.");
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var statusCode = HttpStatusCode.InternalServerError;

            if (exception is NotFoundException)
            {
                statusCode = HttpStatusCode.NotFound;
            }
            else if (exception is BadRequestException)
            {
                statusCode = HttpStatusCode.BadRequest;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var response = new ErrorResponse
            {
                StatusCode = context.Response.StatusCode,
                Message = exception.Message
            };

            await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
        }
    }
}
