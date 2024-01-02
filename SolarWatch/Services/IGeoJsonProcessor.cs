namespace SolarWatch.Services;

public interface IGeoJsonProcessor
{
   Task<List<City>> Process(string data);
}