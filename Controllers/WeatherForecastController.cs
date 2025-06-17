using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Diagnostics;

namespace IoptionsDiffAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController(IOptions<AppSettingOption> optionsNormal,
        IOptionsSnapshot<AppSettingOption> optionsSnapshot, IOptionsMonitor<AppSettingOption> optionsMonitor,
        ILogger<WeatherForecastController> _logger) : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private string PrintOptions(AppSettingOption options)
        {
            return $"Name: {options.Name}, Theme: {options.Theme}, URL: {options.URL}";
        }

        [HttpGet("GetOptionsValue")]
        public IEnumerable<string> GetOptionsValue()
        {
            yield return PrintOptions(optionsNormal.Value);
            yield return PrintOptions(optionsSnapshot.Value);
            optionsMonitor.OnChange(options =>
            {
                Debug.WriteLine("optionsMonitor value is changed:" + PrintOptions(options));
            });
            yield return PrintOptions(optionsMonitor.CurrentValue);
        }



        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
