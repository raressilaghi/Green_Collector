using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Green_Collector.Models;

namespace Green_Collector.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Display(Name = "Nume")]
        [Required(ErrorMessage = "You neeed to give us your first name.")]
        public string LastName { get; set; }

        [Display(Name = "Prenume")]
        [Required(ErrorMessage = "You neeed to give us your last name.")]
        public string FirstName { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        [Required(ErrorMessage = "You neeed to give us your email address.")]
        public string EmailAddress { get; set; }

        [Display(Name = "Numar de Telefon")]
        [Required(ErrorMessage = "You neeed to give us your email address.")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Adresa")]
        [Required(ErrorMessage = "You neeed to give us your email address.")]
        public string Address { get; set; }

        [Display(Name = "Cod de Acces")]
        public string UniqueCodeAccess { get; set; }

        [Display(Name = "Cantitate Colectată")]
        public int QuantityColected { get; set; }

    }
}