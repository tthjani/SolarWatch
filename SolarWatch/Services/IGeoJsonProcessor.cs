namespace SolarWatch.Services;

public interface IGeoJsonProcessor
{
   List<City> Process(string data);
}