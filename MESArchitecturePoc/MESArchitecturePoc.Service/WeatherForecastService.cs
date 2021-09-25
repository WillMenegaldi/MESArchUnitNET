using MESArchitecturePoc.Domain.Interfaces;
using MESArchitecturePoc.Domain.Models;
using System.Collections.Generic;

namespace MESArchitecturePoc.Service
{
    public class WeatherForecastService : IWeatherForecastService
    {
        private readonly IWeatherForecastAgent _agent;

        public WeatherForecastService(IWeatherForecastAgent agent)
        {
            _agent = agent;
        }

        public IEnumerable<WeatherForecast> GetAll()
        {
            return _agent.GetAll();
        }
    }
}
