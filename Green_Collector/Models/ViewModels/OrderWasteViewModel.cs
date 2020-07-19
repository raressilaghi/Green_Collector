using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Green_Collector.Models;

namespace Green_Collector.ViewModels
{
    public class OrderWasteViewModel
    {
        public int OrderId { get; set; }

        [Display(Name = "Oraș")]
        [Required(ErrorMessage = "Trebuie să introduceți un nume de oras")]
        public string Town { get; set; }

        [Display(Name = "Numar")]
        [Required(ErrorMessage = "Trebuie să introduceți un numarul domiciliului")]
        public int Number { get; set; }

        [Display(Name = "Strada")]
        [Required(ErrorMessage = "Trebuie să introduceți numele strazii")]
        public string Address { get; set; }

        [Display(Name = "Telefon")]
        [Required(ErrorMessage = "Trebuie să introduceți un număr de telefor")]
        public string PhoneNumber { get; set; }

        [DataType(DataType.Date)]
        public string Date { get; set; }
        
        public List<CheckBoxItem> WasteTypes { get; set; }

        
    }
}