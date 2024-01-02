using System.Net;

namespace SolarWatch.Services;

public class GeocodingApi : ICityNameDataProvider
{
    private readonly ILogger<GeocodingApi> _logger;

    public GeocodingApi(ILogger<GeocodingApi> logger)
    {
        _logger = logger;
    }
    
    
    public async Task<string> GetCityCoordinates(string name)
    {

        var apiKey = "ac6d81bdf4f56a57f233fb602e5b9aff";
        var url = $"http://api.openweathermap.org/geo/1.0/direct?q={name}&limit=1&appid={apiKey}";

        using var client = new HttpClient();
        _logger.LogInformation("Calling Geocoding API with url: {url}", url);

        var response = await client.GetAsync(url);
        return await response.Content.ReadAsStringAsync();
    }
    
}