using System.Net;

namespace SolarWatch.Services;

public class SunriseSunsetApi : ISunriseSunsetDataProvider
{
    private readonly ILogger<SunriseSunsetApi> _logger;

    public SunriseSunsetApi(ILogger<SunriseSunsetApi> logger)
    {
        _logger = logger;
    }

    public string GetCurrentSunriseSunset(string lat, string lon, string date)
    {
        var url = $"https://api.sunrise-sunset.org/json?lat={lat}&lng={lon}&date={date}";

        var client = new WebClient();
        
        _logger.LogInformation("Calling SunsetSunrise API with url: {url}", url);
        return client.DownloadString(url);
    }
}