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
        Task<IList<Models.Location>> SearchLocationsAsync(string query);

        Location GetForecast(string point);
        Task<Location> GetForecastAsync(string point);
    }
}
