namespace AspireDemo.ApiService.Endpoints
{
    internal static class Development
    {
        public static void MapDevelopment(this WebApplication app)
        {
            if (!app.Environment.IsDevelopment())
                return;

            app.MapGet("/development/throw-exception", () =>
            {
                throw new Exception("This is a test exception.");
            })
            .WithName("Throw-Exception");
        }
    }
}
