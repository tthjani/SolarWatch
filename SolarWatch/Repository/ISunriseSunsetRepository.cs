namespace SolarWatch.Repository;

public interface ISunriseSunsetRepository
{
    Task AddSunriseSunsetTime(SunriseSunsetTimes time);
}