using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Models
{
    public class Location
    {
        public string City { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string Point { get; set; }

        public IList<Forecast> Forecasts { get; set; }
    }
}
