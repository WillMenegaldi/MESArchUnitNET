using MESArchitecturePoc.Domain.Models;
using System.Collections.Generic;

namespace MESArchitecturePoc.Domain.Interfaces
{
    public interface IWeatherForecastAgent
    {
        IEnumerable<WeatherForecast> GetAll();
    }
}
