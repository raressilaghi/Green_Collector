using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Green_Collector.Models.ViewModels
{
    public class ViewStatus
    {
        public string Waste { get; set; }
        public int Quantity { get; set; }
        public string Address { get; set; }
        public DateTime Data { get; set; }
    }
}