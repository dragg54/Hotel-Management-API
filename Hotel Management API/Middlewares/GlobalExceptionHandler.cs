using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Hotel_Management_API.Exceptions;

namespace Hotel_Management_API.Middlewares
{
    public class GlobalExceptionHandler
    {
        private readonly RequestDelegate _next;
    public GlobalExceptionHandler(RequestDelegate next)
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

    private async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        HttpStatusCode status;
        var stackTrace = ex.StackTrace;
        var message = ex.Message;

        switch (ex)
        {
            case NotFoundException _:
                status = HttpStatusCode.NotFound;
                break;
            case DuplicateRequestException _:
                status = HttpStatusCode.Conflict;
                break;
            case BadRequestException _:
                status = HttpStatusCode.BadRequest;
                break;
            case System.UnauthorizedAccessException _:
                status = HttpStatusCode.Unauthorized;
                break;
            default:
                status = HttpStatusCode.InternalServerError;
                break;
        }

        var result = JsonSerializer.Serialize(new { error = message, stackTrace });
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)status;
        await context.Response.WriteAsync(result);
    }
    }
}