using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;
using task_management.Application.Service;
using task_management.Domain.Exceptions;
using System.Text.Json;
using task_management.Application.Dtos.Response;

namespace task_management.WebApi.Middleware
{
    public class ValidationExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public ValidationExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var originalBodyStream = context.Response.Body;
            using var memStream = new MemoryStream();
            context.Response.Body = memStream;

            // Crear opciones para serialización con camelCase
            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            try
            {
                await _next(context);
                if (context.Response.StatusCode == 400 &&
                    context.Response.ContentType?.Contains("application/problem+json") == true)
                {
                    memStream.Seek(0, SeekOrigin.Begin);
                    var responseText = await new StreamReader(memStream).ReadToEndAsync();

                    var validationProblem = JsonSerializer.Deserialize<ValidationProblemDetails>(responseText);
                    var errorDescription = string.Join("; ", validationProblem.Errors
                        .SelectMany(e => e.Value.Select(msg => $"{e.Key}: {msg}")));

                    var response = ResponseApiBuilderService.ErrorResponse<object>(
                        400, "VALIDACION", errorDescription);

                    memStream.SetLength(0);
                    // Usar opciones para serializar con camelCase
                    await JsonSerializer.SerializeAsync(memStream, response, jsonOptions);
                    context.Response.ContentType = "application/json";
                }
            }
            catch (BadRequestException ex)
            {
                context.Response.StatusCode = 400;
                memStream.SetLength(0);
                // Usar opciones para serializar con camelCase
                await JsonSerializer.SerializeAsync(memStream,
                    ResponseApiBuilderService.ErrorResponse<object>(400, "VALIDATION_ERROR", ex.Message),
                    jsonOptions);
                context.Response.ContentType = "application/json";
            }
            finally
            {
                memStream.Seek(0, SeekOrigin.Begin);
                await memStream.CopyToAsync(originalBodyStream);
                context.Response.Body = originalBodyStream;
            }
        }

    }
    public static class ValidationExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomErrorHandling(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ValidationExceptionMiddleware>();
        }
    }
}
