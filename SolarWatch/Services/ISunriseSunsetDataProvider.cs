namespace SolarWatch.Services;

public interface ISunriseSunsetDataProvider
{
    string GetCurrentSunriseSunset(string lat, string lon, string date);
}