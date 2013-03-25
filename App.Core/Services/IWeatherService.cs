using App.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Services
{
    public interface IWeatherService
    {
        IList<Location> SearchLocations(string query);
        IList<Forecast> GetForecast(string point, out Location location);
    }
}
