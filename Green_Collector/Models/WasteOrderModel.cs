using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Green_Collector.Models
{
    public class WasteOrderModel
    {
        public int OrderId { get; set; }
        public OrderModel Order { get; set; }
        public int TypeId { get; set; }
        public WasteModel Waste { get; set; }
        public int Quantity { get; set; }
        public string ClientName { get; set; }

    }
}