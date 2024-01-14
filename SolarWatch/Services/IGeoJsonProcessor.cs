namespace SolarWatch.Services;

public interface IGeoJsonProcessor
{
   Task<City?> Process(string data);
}