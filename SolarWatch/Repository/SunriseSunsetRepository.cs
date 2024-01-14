using SolarWatch.Data;

namespace SolarWatch.Repository;

public class SunriseSunsetRepository : ISunriseSunsetRepository
{
    private readonly SolarWatchContext _context;

    public SunriseSunsetRepository(SolarWatchContext context)
    {
        _context = context;
    }
    
    public async Task AddSunriseSunsetTime(SunriseSunsetTimes time)
    {
       await _context.AddAsync(time);
       await _context.SaveChangesAsync();
    }
}