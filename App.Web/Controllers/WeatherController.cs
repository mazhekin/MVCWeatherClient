﻿using App.Core.Models;
using App.Core.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Web.Controllers
{
    public class WeatherController : Controller
    {
        private readonly IWeatherService weatherService;

        public WeatherController(IWeatherService weatherService)
        {
            this.weatherService = weatherService;
        }

        //
        // GET: /Weather/

        public ActionResult Index(string point)
        {
            var location = this.weatherService.GetForecast(point);
            return View(location);
        }

    }
}
