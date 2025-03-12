using System;

namespace WebApi.Middlewares;

public class CustomExceptionMiddleware
{
    private readonly RequestDelegate next;

    public CustomExceptionMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        string message = "[Request] HTTP" + httpContext.Request.Method + " - " + httpContext.Request.Path;
        Console.WriteLine(message);
        await next(httpContext);
    }
}
