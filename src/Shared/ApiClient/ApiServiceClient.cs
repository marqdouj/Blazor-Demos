namespace AspireDemo.ApiClient
{
    public interface IApiServiceClient
    {
        INewsletterClient Newsletter { get; }
        IWeatherClient Weather { get; }
    }

    public class ApiServiceClient(HttpClient httpClient) : IApiServiceClient
    {
        public INewsletterClient Newsletter { get; } = new NewsletterClient(httpClient);
        public IWeatherClient Weather { get; } = new WeatherClient(httpClient);
    }
}
