using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Green_Collector.Models
{
    public class WasteModel
    {
        public int TypeId { get; set; }

        [Display(Name = "Deșeu")]
        [Required(ErrorMessage = "Trebuie sa introduceti tipul deșeului")]
        public string TypeName { get; set; }
        public List<WasteOrderModel> WasteOrder { get; set; }
    }
}