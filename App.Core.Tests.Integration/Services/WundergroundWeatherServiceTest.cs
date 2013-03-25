using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using App.Core.Data;
using App.Core.Services;
using System.Linq;
using App.Core.Models;

namespace App.Core.Tests.Integration.Services
{
    [TestClass]
    public class WundergroundWeatherServiceTest
    {
        private IWeatherService weatherService;

        [TestInitialize]
        public void Init()
        {
            this.weatherService = new WundergroundWeatherService();
        }

        [TestMethod]
        public void GetSearchResults_Returns_Results_For_Search_Query()
        {
            // Act
            var resultsAllentown = this.weatherService.SearchLocations("Allentown");

            // Assert
            Assert.AreEqual(resultsAllentown.Count, 4);

            // Act
            Location locationAllentown;
            var forecastAllentown = this.weatherService.GetForecast(resultsAllentown.First().Point, out locationAllentown); 

            // Assert
            Assert.AreEqual(locationAllentown.City, "Allentown");

            // Act
            var resultsKaliningrad = this.weatherService.SearchLocations("Kaliningrad");
            

            // Assert
            Assert.AreEqual(resultsKaliningrad.Count, 1);

            // Act
            Location locationKaliningrad;
            var forecastKaliningrad = this.weatherService.GetForecast(resultsKaliningrad.First().Point, out locationKaliningrad); 

        }
    }
}
