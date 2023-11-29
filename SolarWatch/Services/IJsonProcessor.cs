namespace SolarWatch.Services;

public interface IJsonProcessor
{
    SolarWatch Process(string data, string date);
}