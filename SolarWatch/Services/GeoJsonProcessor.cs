using System.Text.Json;

namespace SolarWatch.Services;

public class GeoJsonProcessor : IGeoJsonProcessor
{
    public List<City> Process(string data)
    {
        List<City> cities = new List<City>();

        JsonDocument json = JsonDocument.Parse(data);
        JsonElement root = json.RootElement;

        if (root.ValueKind == JsonValueKind.Array)
        {
            foreach (JsonElement cityElement in root.EnumerateArray())
            {
                JsonElement name = cityElement.GetProperty("name");
                JsonElement lon = cityElement.GetProperty("lon");
                JsonElement lat = cityElement.GetProperty("lat");

                City cityData = new City
                {
                    Name = name.GetString(),
                    Longitude = lon.GetDouble().ToString(), 
                    Latitude = lat.GetDouble().ToString()
                };

                cities.Add(cityData);
            }
        }

        return cities;
    }
}