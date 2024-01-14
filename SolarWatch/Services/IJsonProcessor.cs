namespace SolarWatch.Services;

public interface IJsonProcessor
{
    SunriseSunsetTimes Process(string data, int cityId);
}