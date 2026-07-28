using Serilog;
using System.Diagnostics;
using System.Text;

namespace CampusHire.API.Middleware
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        public RequestLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();
            context.Request.EnableBuffering();
            string requestBody = "";
            if (context.Request.ContentLength > 0)
            {
                using (var reader = new StreamReader(
                    context.Request.Body,
                    Encoding.UTF8,
                    leaveOpen: true))
                {
                    requestBody = await reader.ReadToEndAsync();
                    context.Request.Body.Position = 0;
                }
            }
            Log.Information(
                "HTTP Request: {Method} {Path} Body: {Body}",
                context.Request.Method,
                context.Request.Path,
                requestBody);
            var originalBodyStream = context.Response.Body;
            using var responseBody = new MemoryStream();
            context.Response.Body = responseBody;
            await _next(context);
            stopwatch.Stop();
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            string responseBodyText =
                await new StreamReader(context.Response.Body).ReadToEndAsync();
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            await responseBody.CopyToAsync(originalBodyStream);
            Log.Information(
                "HTTP Response: {StatusCode} TimeTaken: {ElapsedMilliseconds}ms Response: {Response}",
                context.Response.StatusCode,
                stopwatch.ElapsedMilliseconds,
                responseBodyText);
        }
    }
}