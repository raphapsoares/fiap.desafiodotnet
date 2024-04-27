using Microsoft.AspNetCore.Http;
using System.Diagnostics;

namespace fiap.testedotnet.infrastructure.Middleware
{
    public class LogMiddleware
    {
        private readonly RequestDelegate _next;

        public LogMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            // Registro de informações de solicitação
            var stopwatch = Stopwatch.StartNew();
            var request = context.Request;
            var originalBodyStream = context.Response.Body;

            try
            {
                using (var responseBody = new MemoryStream())
                {
                    context.Response.Body = responseBody;

                    await _next(context);

                    // Registro de informações de resposta
                    var response = context.Response;
                    var elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
                    var responseBodyText = await FormatResponse(response);

                    Console.WriteLine($"Request: {request.Method} {request.Path} => Response: {response.StatusCode} Elapsed Time: {elapsedMilliseconds}ms");
                    Console.WriteLine($"Response Body: {responseBodyText}");

                    responseBody.Seek(0, SeekOrigin.Begin);
                    await responseBody.CopyToAsync(originalBodyStream);
                }
            }
            finally
            {
                context.Response.Body = originalBodyStream;
            }
        }
        private async Task<string> FormatResponse(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            var responseBodyText = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);
            return responseBodyText;
        }
    }
}
