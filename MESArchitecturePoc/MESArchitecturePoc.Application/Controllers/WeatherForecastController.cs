using MESArchitecturePoc.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MESArchitecturePoc.Application.Controllers
{
    [ApiController]
    [Route("weatherForecast")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IWeatherForecastService _service;

        public WeatherForecastController(IWeatherForecastService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult Get()
        {
            var response = _service.GetAll();

            return Ok(response);
        }
    }
}
