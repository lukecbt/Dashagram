using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Dashagram.Api.Handlers
{
    /// <summary>
    /// Custom exception handler that implements the IExceptionHandler interface. This class is responsible for handling exceptions that occur during the processing of HTTP requests in the application.
    /// </summary>
    public class ExceptionHandler(ILogger<ExceptionHandler> logger) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            logger.LogError(exception,
                "An error occurred while processing the request. Message: {Message}. TraceId: {TraceId}",
                exception.Message,
                httpContext.TraceIdentifier);

            var statusCode = StatusCodes.Status500InternalServerError;
            var title = "Internal Server Error";
            var details = "An unexpected error occurred.";

            var problemDetails = new ProblemDetails
            {
                Status = statusCode,
                Title = title,
                Detail = details,
                Instance = httpContext.Request.Path,
                Type = $"https://httpstatuses.com/{statusCode}",
            };

            problemDetails.Extensions["traceId"] = httpContext.TraceIdentifier;

            // Override the default status code and details for specific exception types
            switch (exception)
            {
                // Thrown when a validation error occurs on a request
                case ValidationException validationException:
                    statusCode = StatusCodes.Status400BadRequest;
                    title = "Validation Failed";
                    details = "One or more validation errors occurred.";

                    var errors = validationException.Errors
                        .GroupBy(e => e.PropertyName)
                        .ToDictionary(
                            g => g.Key,
                            g => g.Select(e => e.ErrorMessage).ToArray()
                        );

                    problemDetails.Status = statusCode;
                    problemDetails.Title = title;
                    problemDetails.Detail = details;
                    problemDetails.Extensions["errors"] = errors;
                    break;
                case ArgumentNullException argumentNullException:
                    // TODO: boilerplate for other exception types, e.g., NotFoundException, UnauthorizedAccessException, etc.
                    statusCode = StatusCodes.Status400BadRequest;
                    title = "Bad Request";
                    details = argumentNullException.Message;
                    problemDetails.Status = statusCode;
                    problemDetails.Title = title;
                    problemDetails.Detail = details;
                    break;
            }

            // Convert to JSON and write as problem details response
            httpContext.Response.StatusCode = statusCode;
            httpContext.Response.ContentType = "application/problem+json";

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
    }
}
