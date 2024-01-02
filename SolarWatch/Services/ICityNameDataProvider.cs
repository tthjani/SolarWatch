namespace SolarWatch.Services;

public interface ICityNameDataProvider
{
   Task<string> GetCityCoordinates(string name);
}