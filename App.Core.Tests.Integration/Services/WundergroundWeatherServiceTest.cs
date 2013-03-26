using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using App.Core.Data;
using App.Core.Services;
using System.Linq;
using App.Core.Models;
using System.Threading.Tasks;

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
        public void GetLocations_Returns_Results_For_Search_Query()
        {
            // Act
            var resultsAllentown = this.weatherService.SearchLocations("Allentown");

            // Assert
            Assert.AreEqual(resultsAllentown.Count, 4);

            // Act
            var locationAllentown = this.weatherService.GetForecast(resultsAllentown.First().Point); 

            // Assert
            Assert.AreEqual(locationAllentown.City, "Allentown");

            // Act
            var resultsKaliningrad = this.weatherService.SearchLocations("Kaliningrad");
            

            // Assert
            Assert.AreEqual(resultsKaliningrad.Count, 1);

            // Act
            var locationKaliningrad = this.weatherService.GetForecast(resultsKaliningrad.First().Point); 

        }

        [TestMethod]
        public void GetLocationsAsync_Returns_Results_For_Search_Query()
        {
            // Act
            var resultsAllentown = this.weatherService.SearchLocationsAsync("Allentown");

            Task.WaitAll(resultsAllentown);

            // Assert
            Assert.AreEqual(resultsAllentown.Result.Count, 4);

            // Act
            var locationAllentown = this.weatherService.GetForecastAsync(resultsAllentown.Result.First().Point);

            Task.WaitAll(locationAllentown);

            //Assert
            Assert.AreEqual(locationAllentown.Result.City, "Allentown");
        }

    }
}
