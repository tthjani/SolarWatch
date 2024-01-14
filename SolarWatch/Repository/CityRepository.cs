using Microsoft.EntityFrameworkCore;
using SolarWatch.Data;

namespace SolarWatch.Repository;

public class CityRepository : ICityRepository
{
    private readonly SolarWatchContext _context;

    public CityRepository(SolarWatchContext context)
    {
        _context = context;
    }


    public async Task<City?> FindCityByName(string cityName)
    {
        var res = await _context.Cities.FirstOrDefaultAsync(c => c.Name == cityName);
        
        return res;
    }

    public async void AddCity(City city)
    {
        await _context.AddAsync(city);
        await _context.SaveChangesAsync();
    }
}