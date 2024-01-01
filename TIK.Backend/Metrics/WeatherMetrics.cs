using System.Diagnostics;
using System.Diagnostics.Metrics;

namespace TIK.Frontend.Server.Metrics
{
    public class WeatherMetrics
    {
        public const string MetricsName = "Weather.API";

        private readonly Counter<long> _weatherRequestCounter;
        private readonly Histogram<double> _weatherRequestDuration;

        public WeatherMetrics(IMeterFactory meterFactory)
        {
            var meter = meterFactory.Create(MetricsName);
            _weatherRequestCounter = meter.CreateCounter<long>(
                "watherapp.api.weather_requests.count");

            _weatherRequestDuration = meter.CreateHistogram<double>(
                "watherapp.api.weather_requests.duration",
                "ms");
        }

        public void IncreaseWeatherRequestCount()
        {
            _weatherRequestCounter.Add(1);
        }

        public TrackedRequestDuration MeasureRequestDuration()
        {
            return new TrackedRequestDuration(_weatherRequestDuration);
        }
    }

    public class TrackedRequestDuration : IDisposable
    {
        private Histogram<double> _histogram { get; }
        private readonly long requestStartTime = TimeProvider.System.GetTimestamp();

        public TrackedRequestDuration(Histogram<double> histogram)
        {
            _histogram = histogram;
        }


        public void Dispose()
        {
            var elapsed = TimeProvider.System.GetElapsedTime(requestStartTime);
            _histogram.Record(elapsed.TotalMilliseconds);
        }
    }
}
