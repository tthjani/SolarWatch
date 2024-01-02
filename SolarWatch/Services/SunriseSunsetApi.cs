using System.Net;

namespace SolarWatch.Services;

public class SunriseSunsetApi : ISunriseSunsetDataProvider
{
    private readonly ILogger<SunriseSunsetApi> _logger;

    public SunriseSunsetApi(ILogger<SunriseSunsetApi> logger)
    {
        _logger = logger;
    }

    public async Task<string> GetCurrentSunriseSunset(string lat, string lon, string date)
    {
        var url = $"https://api.sunrise-sunset.org/json?lat={lat}&lng={lon}&date={date}";

       using var client = new HttpClient();
        _logger.LogInformation("Calling SunsetSunrise API with url: {url}", url);

        var response = await client.GetAsync(url);
        return await response.Content.ReadAsStringAsync();
    }
}