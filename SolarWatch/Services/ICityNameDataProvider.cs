namespace SolarWatch.Services;

public interface ICityNameDataProvider
{
   string GetCityCoordinates(string name);
}