namespace SolarWatch.Repository;

public interface ICityRepository
{
    Task<City?> FindCityByName(string cityName);
    void AddCity(City city);
}