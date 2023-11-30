using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using SolarWatch.Services;

namespace SolarWatch.Controllers;

[ApiController]
[Route("[controller]")]
public class SolarWatchController : ControllerBase
{
    
    private readonly ILogger<SolarWatchController> _logger;
    private ISunriseSunsetDataProvider _sunriseSunsetDataProvider;
    private IJsonProcessor _jsonProcessor;
    private ICityNameDataProvider _cityNameDataProvider;
    private IGeoJsonProcessor _geoJsonProcessor;
    
    public SolarWatchController(ILogger<SolarWatchController> logger, ISunriseSunsetDataProvider sunriseSunsetDataProvider, IJsonProcessor jsonProcessor, ICityNameDataProvider cityNameDataProvider, IGeoJsonProcessor geoJsonProcessor)
    {
        _logger = logger;
        _sunriseSunsetDataProvider = sunriseSunsetDataProvider;
        _jsonProcessor = jsonProcessor;
        _cityNameDataProvider = cityNameDataProvider;
        _geoJsonProcessor = geoJsonProcessor;
    }
    
    [HttpGet("GetSunriseAndSunsetTime")]
    public ActionResult<SolarWatch> GetCurrent([Required] string city)
    {
        var cityData = _cityNameDataProvider.GetCityCoordinates(city);
        var jsonData =  _geoJsonProcessor.Process(cityData);
        
        var lat = "";
        var lon = "";
        string date = DateOnly.FromDateTime(DateTime.Today).ToString("yyyy-MM-dd");

        foreach (var cityName in jsonData)
        {
            lat = cityName.Latitude;
            lon = cityName.Longitude;
        }

        try
        {
            var sunriseSunsetData = _sunriseSunsetDataProvider.GetCurrentSunriseSunset(lat, lon, date);
            return Ok(_jsonProcessor.Process(sunriseSunsetData, date, city));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error getting sunrise and sunset data");
            return NotFound("Error getting sunrise and sunset data");
        }
    }
    
    //Test the other api
    
    /*[HttpGet("GetCityCoordinates")]
    public ActionResult<City> GetCurrentCityCoordinates([Required] string city)
    {
        try
        {
            var cityData = _cityNameDataProvider.GetCityCoordinates(city);
            return Ok(_geoJsonProcessor.Process(cityData));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error getting city data");
            return NotFound("Error getting city data");
        }
    }*/
}