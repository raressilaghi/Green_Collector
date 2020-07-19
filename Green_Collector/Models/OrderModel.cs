using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Green_Collector.Models
{
    public class OrderModel
    {   public int Id { get; set; }

        [Display(Name = "Strada")]
        [Required(ErrorMessage = "Trebuie să introduceți numele străzii")]
        public string Address { get; set; }

        [Display(Name = "Oras")]
        public string Town { get; set; }

        [Display(Name = "Numar")]
        [Required(ErrorMessage = "Trebuie să introduceți numarul domiciliului")]
        public int Number { get; set; }

        [Display(Name = "Telefon")]
        [Required(ErrorMessage = "Trebuie să introduceți un număr de telefon")]
        public string PhoneNumber { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; } 
        public List<WasteOrderModel> WasteOrder { get; set; }

        public int CustomerId { get; set; }

    }
}