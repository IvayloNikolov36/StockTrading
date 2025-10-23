using Orders.Service.Infrastructure.Exceptions;
using Orders.Service.Infrastructure.Models;
using System.Net;
using System.Net.Mime;
using System.Text.Json;

namespace Orders.Service.Infrastructure.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await this.next(context);
        }
        catch (Exception ex)
        {
            await this.HandleExceptionAsync(context, ex, Guid.NewGuid());
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception, Guid guid)
    {
        context.Response.ContentType = MediaTypeNames.Application.Json;
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        HttpErrorResult? errorResult = null;

        switch (exception)
        {
            case ActionableException actionableException:
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                errorResult = new HttpErrorResult(guid, actionableException);
                break;
            case UnauthorizedException unauthorizedException:
                context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                errorResult = new HttpErrorResult(guid, unauthorizedException);
                break;
            default:
                errorResult = new HttpErrorResult(guid);
                break;
        }

        JsonSerializerOptions options = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        string jsonResult = JsonSerializer.Serialize(errorResult, options);

        return context.Response.WriteAsync(jsonResult);
    }
}
