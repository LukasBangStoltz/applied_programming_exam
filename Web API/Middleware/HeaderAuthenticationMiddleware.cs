namespace Web_API.Middleware
{
    public class HeaderAuthenticationMiddleware
    {
        private const string MY_SECRET_VALUE = "Abc123!!!";
        private readonly RequestDelegate _next;

        public HeaderAuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue("X-My-Request-Header", out var extractedHeaderValue))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Authentication header is missing.");
                return;
            }

            if (extractedHeaderValue != MY_SECRET_VALUE)
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Invalid authentication header value.");
                return;
            }

            await _next(context);
        }
    }

    public static class HeaderAuthenticationMiddlewareExtensions
    {
        public static IApplicationBuilder UseHeaderAuthenticationMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<HeaderAuthenticationMiddleware>();
        }
    }
}
