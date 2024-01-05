using TIK.Shared;

namespace TIK.Frontend.Server
{
    public class WeatherApiClient(HttpClient client)
    {
        public async Task<WeatherForecast[]> GetWeatherAsync()
        {
            return await client.GetFromJsonAsync<WeatherForecast[]>("/api/weatherforecast") ?? [];
        }
    }
}
