namespace AspireDemo.ApiClient
{
    public interface IApiServiceClient
    {
        IWeatherClient Weather { get; }
    }

    public class ApiServiceClient(HttpClient httpClient) : IApiServiceClient
    {
        public IWeatherClient Weather { get; } = new WeatherClient(httpClient);
    }
}
