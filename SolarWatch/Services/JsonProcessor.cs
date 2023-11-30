using System.Text.Json;
using DateTimeOffset = System.DateTimeOffset;

namespace SolarWatch.Services;

public class JsonProcessor : IJsonProcessor
{
    public SolarWatch Process(string data, string date, string city)
    {
        JsonDocument json = JsonDocument.Parse(data);
        JsonElement results = json.RootElement.GetProperty("results");
        JsonElement sunrise = results.GetProperty("sunrise");
        JsonElement sunset = results.GetProperty("sunset");

        SolarWatch sunriseData = new SolarWatch
        {
            Date = date,
            City = city,
            Sunrise = sunrise.GetString(),
            Sunset = sunset.GetString()
        };

        return sunriseData;
    }
}