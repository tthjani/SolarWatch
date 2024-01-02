namespace SolarWatch.Services;

public interface ISunriseSunsetDataProvider
{
    Task<string> GetCurrentSunriseSunset(string lat, string lon, string date);
}