using System.Net;
using System.Text.Json;
using CampusHire.API.DTOs.Common;
using CampusHire.API.Exceptions;

namespace CampusHire.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next)
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
                await HandleException(context, ex);
            }
        }
        private async Task HandleException(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            ErrorResponse response = new();
            switch (ex)
            {
                case NotFoundException e:
                    context.Response.StatusCode = 404;
                    response.StatusCode = 404;
                    response.ErrorCode = e.ErrorCode;
                    response.Message = e.Message;
                    break;
                case BadRequestException e:
                    context.Response.StatusCode = 400;
                    response.StatusCode = 400;
                    response.ErrorCode = e.ErrorCode;
                    response.Message = e.Message;
                    break;
                case UnauthorizedException e:
                    context.Response.StatusCode = 401;
                    response.StatusCode = 401;
                    response.ErrorCode = e.ErrorCode;
                    response.Message = e.Message;
                    break;
                case ValidationException e:
                    context.Response.StatusCode = 400;
                    var validation = new ValidationErrorResponse
                    {
                        Errors = e.Errors
                    };
                    await context.Response.WriteAsync(JsonSerializer.Serialize(validation));
                    return;
                default:
                    context.Response.StatusCode = 500;
                    response.StatusCode = 500;
                    response.ErrorCode = "SERVER_ERROR";
                    response.Message = "Internal server error";
                    break;
            }
            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}