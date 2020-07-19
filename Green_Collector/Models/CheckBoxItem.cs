using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Green_Collector.Models
{
    public class CheckBoxItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsChecked { get; set; }

        [Display(Name = "Cantitate(kg)")]
        [Required(ErrorMessage = "trebuie sa introduceți cantitatea")]
        public int Quantity { get; set; }
    }
}