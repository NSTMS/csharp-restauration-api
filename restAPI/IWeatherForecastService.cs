using System.Collections.Generic;

namespace restAPI
{
    public interface IWeatherForecastService
    {
        IEnumerable<WeatherForecast> Get();
        bool Get(int take, int minTemp, int maxTemp);
    }
}