using Application.DTOs.ResponseDTOs;
using Domain.Enums;
using Microsoft.AspNetCore.Diagnostics;

namespace Presentation.MiddleWares
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            _logger.LogError(exception, "An unexpected server error occurred: {Message}", exception.Message);

            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            httpContext.Response.ContentType = "application/json";

            var response = ResponseDto<string>.Fail(ErrorCode.ServerError, "An unexpected error occurred. Please try again later.");

            await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);
            return true;
        }
    }
}
