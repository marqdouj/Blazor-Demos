namespace AspireDemo.ApiService.Endpoints
{
    internal static class Development
    {
        public static void MapDevelopment(this WebApplication app, bool isDevelopment)
        {
            if (!isDevelopment)
                return;

            app.MapGet("/development/throw-exception", () =>
            {
                throw new Exception("This is a test exception.");
            })
            .WithName("Throw-Exception");
        }
    }
}
