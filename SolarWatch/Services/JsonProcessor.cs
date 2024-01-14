using System.Text.Json;
using DateTimeOffset = System.DateTimeOffset;

namespace SolarWatch.Services;

public class JsonProcessor : IJsonProcessor
{
    public SunriseSunsetTimes Process(string data, int cityId)
    {
        JsonDocument json = JsonDocument.Parse(data);
        JsonElement results = json.RootElement.GetProperty("results");
        JsonElement sunrise = results.GetProperty("sunrise");
        JsonElement sunset = results.GetProperty("sunset");

        SunriseSunsetTimes sunriseData = new SunriseSunsetTimes()
        {
            SunriseDate = DateTimeOffset.Parse(sunrise.GetString()),
            SunsetDate = DateTimeOffset.Parse(sunset.GetString()),
            CityId = cityId
        };

        return sunriseData;
    }
    
}