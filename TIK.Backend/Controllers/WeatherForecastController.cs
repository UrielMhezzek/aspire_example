using Azure.Security.KeyVault.Secrets;
using Microsoft.AspNetCore.Mvc;
using TIK.Frontend.Server.Metrics;
using TIK.Shared;

namespace TIK.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {


        private static readonly string[] Summaries = new[]
        {
            "Eisig", "Erfrischend", "Kühl", "Angenehm", "Mild", "Warm", "Sanft", "Heiß", "Schwül", "Sengend"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly WeatherMetrics _weatherMetrics;


        public WeatherForecastController(ILogger<WeatherForecastController> logger, WeatherMetrics weatherMetrics)
        {
            _logger = logger;
            this._weatherMetrics = weatherMetrics;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            using var _ = _weatherMetrics.MeasureRequestDuration();
            try
            {
                return Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                })
                .ToArray();
            }
            finally 
            {
                _weatherMetrics.IncreaseWeatherRequestCount();
            }
           
        }
    }
}
