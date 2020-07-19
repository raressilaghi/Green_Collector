using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Models
{
    public class OrderModel
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string Town { get; set; }
        public int Number { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime Date { get; set; }
        public int CustomerId { get; set; }
    }
}
