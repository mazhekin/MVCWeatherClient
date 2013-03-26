using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using App.Web.Controllers;
using Moq;
using App.Core.Services;
using App.Core.Models;
using System.Collections.Generic;
using MvcContrib.TestHelper;
using System.Web.Mvc;

namespace App.Web.Tests.Controllers
{
    [TestClass]
    public class WeatherControllerTest
    {
        private WeatherController controller;

        private Mock<IWeatherService> weatherServiceMock;

        [TestInitialize]
        public void TestStartUp()
        {
            this.weatherServiceMock = new Mock<IWeatherService>();

            this.controller = new WeatherController(this.weatherServiceMock.Object);
        }

        [TestMethod]
        public void Index_Get_Returns_Default_View_With_Forecasts()
        {
            // Arrange
            Location location = new Location { City = "City", Country = "Country", State = "State", Point = "Point" };
            location.Forecasts = new List<Forecast> 
            {
                new Forecast { Icon = "icon1", Title = "title1", Text = "text1" },
                new Forecast { Icon = "icon2", Title = "title2", Text = "text2" },
                new Forecast { Icon = "icon3", Title = "title3", Text = "text3" }
            };

            this.weatherServiceMock.Setup(x => x.GetForecast("point")).Returns(location);

            // Act
            var result = this.controller.Index("point");

            // Assert
            var viewData = result.AssertResultIs<ViewResult>().ForView("").ViewData;
            Assert.AreEqual(viewData.Model, location);
            Assert.IsNotNull((viewData.Model as Location).Forecasts);
        }
    }
}
