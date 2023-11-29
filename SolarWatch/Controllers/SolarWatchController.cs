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
    
    public SolarWatchController(ILogger<SolarWatchController> logger, ISunriseSunsetDataProvider sunriseSunsetDataProvider, IJsonProcessor jsonProcessor)
    {
        _logger = logger;
        _sunriseSunsetDataProvider = sunriseSunsetDataProvider;
        _jsonProcessor = jsonProcessor;
    }
    
    [HttpGet("GetSunriseAndSunsetTime")]
    public ActionResult<SolarWatch> GetCurrent()
    {
        //Budapest
        var lat = "47.497913";
        var lon = "19.040236";
        string date = DateOnly.FromDateTime(DateTime.Today).ToString("yyyy-MM-dd");

        try
        {
            var sunriseSunsetData = _sunriseSunsetDataProvider.GetCurrentSunriseSunset(lat, lon, date);
            return Ok(_jsonProcessor.Process(sunriseSunsetData, date));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error getting sunrise and sunset data");
            return NotFound("Error getting sunrise and sunset data");
        }
    }
}