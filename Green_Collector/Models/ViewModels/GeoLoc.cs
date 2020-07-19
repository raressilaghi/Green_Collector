using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Green_Collector.Models.ViewModels
{
    public class GeoLoc
    {
        public int OrderId { get; set; }
        public int TypeId { get; set; }
        public int Quantity { get; set; }
        public string Address { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public DateTime Date { get; set; }
        public string Collected { get; set; }
    }
}