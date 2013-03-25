using App.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Web.Controllers
{
    public class SearchController : Controller
    {
        private readonly IWeatherService weatherService;

        public SearchController(IWeatherService weatherService)
        {
            this.weatherService = weatherService;
        }

        //
        // GET: /Search/

        public ActionResult Index(string q)
        {
            if (!String.IsNullOrWhiteSpace(q))
            {
                ViewBag.Results = this.weatherService.SearchLocations(q);
            }

            return View();
        }

    }
}
