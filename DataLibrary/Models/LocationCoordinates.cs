using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Models
{
    public class LocationCoordinates
    {
        public int OrderId { get; set; }
        public int TypeId { get; set; }
        public int Quantity { get; set; }
        public string Address { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public DateTime Date { get; set; }
        public string Collected { get; set; }
        public string ClientName { get; set; }

    }
}
