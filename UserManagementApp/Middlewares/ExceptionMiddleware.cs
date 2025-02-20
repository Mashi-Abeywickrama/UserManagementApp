using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace UserManagementApp.Middlewares
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

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context); // Continue the request pipeline
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred: {ex}"); // Log error
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var statusCode = exception switch
            {
                UnauthorizedAccessException => (int)HttpStatusCode.Unauthorized,   // 401
                KeyNotFoundException => (int)HttpStatusCode.NotFound,             // 404
                ArgumentException => (int)HttpStatusCode.BadRequest,              // 400
                _ => (int)HttpStatusCode.InternalServerError                      // Default - 500
            };

            // default messages for known exceptions
            var defaultMessage = exception switch
            {
                KeyNotFoundException => "The requested resource was not found.",
                ArgumentException => "Invalid argument provided.",
                UnauthorizedAccessException => "You are not authorized to perform this action.",
                _ => "An error occurred while processing your request."
            };

            var errorMessage = exception.Message ?? defaultMessage;

            var errorResponse = new
            {
                StatusCode = statusCode,
                Message = errorMessage,
                ErrorType = statusCode switch
                {
                    400 => "Bad Request",
                    401 => "Unauthorized",
                    404 => "Not Found",
                    500 => "Internal Server Error",
                    _ => "Error"
                }
            };

            var response = JsonSerializer.Serialize(errorResponse);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            return context.Response.WriteAsync(response);
        }
    }
}