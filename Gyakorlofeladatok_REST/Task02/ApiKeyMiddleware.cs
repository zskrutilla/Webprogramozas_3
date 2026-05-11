namespace Task02
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate next;

        public ApiKeyMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            // Allow Swagger without API key
            if (context.Request.Path.StartsWithSegments("/swagger"))
            {
                await next(context);
                return;
            }

            // Read API key from request header
            var apiKey = context.Request.Headers["X-API-KEY"].FirstOrDefault();

            // Check if the API key is valid
            if (apiKey != "school-secret-2026")
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Unauthorized - Invalid API Key");
                return;
            }

            // Continue to the next middleware or controller
            await next(context);
        }
    }
}
