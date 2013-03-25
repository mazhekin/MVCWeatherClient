using App.Core.Data;
using App.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace App.Core.Services
{

    public class WundergroundWeatherService : IWeatherService
    {
        public class GeoLookup
        {
            public class Location
            {
                public string city;
                public string country_name;
                public string state;
                public string zmw;
                public string tz_long;
            }
            public class Response
            {
                public string version;
                public Location[] results;
            }
            public Response response;
            public Location location;
        }

        public class ForecastObject
        {
            public class Response
            {
                public string version;
            }
            public class Forecast
            {
                public class Txt_Forecast
                {
                    public class Forecastday
                    {
                        public string icon_url;
                        public string title;
                        public string fcttext_metric;
                    }
                    public Forecastday[] forecastday;
                }
                public Txt_Forecast txt_forecast;
            }
            public Response response;
            public Forecast forecast;
            public GeoLookup.Location location;
        }

        private const string serviceAddress = "http://api.wunderground.com/api/b02392a91af46ebb/";
        private const string geolookupTemplate = "geolookup/q/{0}.json";
        private const string forecastTemplate = "forecast/geolookup/q/{0}.json";

        private Models.Location ToLocation(GeoLookup.Location location)
        {
            var loc = new Location
            {
                Country = location.country_name,
                City = location.city,
                State = location.state,
                Point = location.zmw
            };

            // fix
            if (!String.IsNullOrWhiteSpace(location.tz_long))
            {
                loc.Point = location.tz_long;
                return loc;
            }

            if (loc.Point.Substring(0, 6).Equals("00000."))
            {
                loc.Point = loc.Country + "/" + loc.City;
            }

            return loc;
        }

        private Models.Forecast ToForecast(ForecastObject.Forecast.Txt_Forecast.Forecastday forecast)
        {
            return new Models.Forecast
            {
                Icon = forecast.icon_url,
                Title = forecast.title,
                Text = forecast.fcttext_metric
            };
        }

        IList<Models.Location> IWeatherService.SearchLocations(string query)
        {
            var address = String.Format(serviceAddress + geolookupTemplate, query); 
            var response = HTTPReader.Load(address);

            var serializer = new JavaScriptSerializer();
            var geoLookupObject = serializer.Deserialize<GeoLookup>(response);

            if (geoLookupObject.response.results != null)
            {
                return geoLookupObject.response.results.Select(x => this.ToLocation(x)).ToList();
            }

            if (geoLookupObject.location != null)
            {
                var location = geoLookupObject.location;
                return new List<Models.Location> { this.ToLocation(location) };
            }

            return new List<Models.Location>();
        }

        IList<Models.Forecast> IWeatherService.GetForecast(string point, out Location location)
        {
            var address = String.Format(serviceAddress + forecastTemplate, point);
            var response = HTTPReader.Load(address);

            var serializer = new JavaScriptSerializer();
            var forecastObject = serializer.Deserialize<ForecastObject>(response);

            location = this.ToLocation(forecastObject.location);

            return forecastObject.forecast.txt_forecast.forecastday.Select(x => this.ToForecast(x)).ToList();
        }
    }
}
