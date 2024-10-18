using Birdie.WebApiReactiveTest.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace Birdie.WebApiReactiveTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly ErrorHandler _errorHandler;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, ErrorHandler errorHandler)
        {
            _logger = logger;
            _errorHandler = errorHandler;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            Console.WriteLine($"{DateTime.Now}: error generado");
            _errorHandler.OnError(new Exception($"{DateTime.Now}: Se ha producido un error en el controlador WeatherForecastController"));
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
