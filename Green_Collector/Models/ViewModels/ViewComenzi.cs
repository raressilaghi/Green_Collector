using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Green_Collector.Models.ViewModels
{
    public class ViewComenzi
    {

        public int IdComanda { get; set; }

        [Display(Name = "Utilizator")]
        public string UserName { get; set; }
        [Display(Name = "Tip deșeu")]
        public string WasteType { get; set; }
        [Display(Name = "Cantitate")]
        public int Quantity { get; set; }
        [Display(Name = "Oraș")]
        public string Town { get; set; }
        [Display(Name = "Stradă")]
        public string Address { get; set; }
        [Display(Name = "Număr")]
        public int Number { get; set; }
        [Display(Name = "Număr de telefon")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Dată")]
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public int IdType { get; set; }


    }
}