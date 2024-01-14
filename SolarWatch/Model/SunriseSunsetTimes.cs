namespace SolarWatch;

public class SunriseSunsetTimes
{
    public int Id { get; set; }
    public DateTimeOffset SunriseDate { get; set; }
    public DateTimeOffset SunsetDate { get; set; }
    public int CityId { get; set; }
}