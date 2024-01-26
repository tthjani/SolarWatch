using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SolarWatch.Repository;
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
    private ICityRepository _cityRepository;
    private ISunriseSunsetRepository _sunriseSunsetRepository;
    
    public SolarWatchController(ILogger<SolarWatchController> logger, ISunriseSunsetDataProvider sunriseSunsetDataProvider, IJsonProcessor jsonProcessor, ICityNameDataProvider cityNameDataProvider, IGeoJsonProcessor geoJsonProcessor, ICityRepository cityRepository, ISunriseSunsetRepository sunriseSunsetRepository)
    {
        _logger = logger;
        _sunriseSunsetDataProvider = sunriseSunsetDataProvider;
        _jsonProcessor = jsonProcessor;
        _cityNameDataProvider = cityNameDataProvider;
        _geoJsonProcessor = geoJsonProcessor;
        _cityRepository = cityRepository;
        _sunriseSunsetRepository = sunriseSunsetRepository;
    }
    
    [HttpGet("GetSunriseAndSunsetTime"), Authorize(Roles = "Admin")]
    public async Task<ActionResult<SunriseSunsetTimes>> GetCurrent([Required] string city)
    {
        try
        {
            var res = await _cityRepository.FindCityByName(city);

            if (res == null)
            {
                var cityData = await _cityNameDataProvider.GetCityCoordinates(city);
                var jsonData = await _geoJsonProcessor.Process(cityData);
            
                if (jsonData == null)
                {
                    return BadRequest();
                }
                _cityRepository.AddCity(jsonData);
            
                res = jsonData;
            }
        
            var lat = res.Latitude;
            var lon = res.Longitude;
            var date = DateOnly.FromDateTime(DateTime.Today).ToString("yyyy-MM-dd");
            
            
            var sunriseSunsetData = await _sunriseSunsetDataProvider.GetCurrentSunriseSunset(lat, lon, date);
            var sunriseSunsetAdd = _jsonProcessor.Process(sunriseSunsetData, res.Id);
            await _sunriseSunsetRepository.AddSunriseSunsetTime(sunriseSunsetAdd);
            return Ok(sunriseSunsetAdd);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error getting sunrise and sunset data");
            Console.WriteLine("----------------------------------------------------asd");
            Console.WriteLine(e);
            return NotFound("Error getting sunrise and sunset data");
        }
    }

    [HttpGet("Test"), Authorize(Roles="Admin")]
    public IActionResult Test()
    {
        return Ok();
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