using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Green_Collector.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required(ErrorMessage = "Câmpul Email este obligatoriu")]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Câmpul Parolă este obligatoriu")]
        [DataType(DataType.Password)]
        [Display(Name = "Parolă")]
        public string Password { get; set; }

        [Display(Name = "Ține minte")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Câmpul email este obligatoriu")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
       
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Parola")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirma parola")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Prenume")]
        [Required(ErrorMessage = "Campul Prenume este obligatoriu")]
        public string FirstName { get; set; }

        [Display(Name = "Nume")]
        [Required(ErrorMessage = "Campul Nume este obligatoriu")]
        public string LastName { get; set; }
              
        [DataType(DataType.Date)]
        [Display(Name = "Data Nasterii")]
        [Required(ErrorMessage = "Campul Data Nasterii este obligatoriu")]
        public DateTime? BirthDate { get; set; }

        [Display(Name = "Cod de Acces")]
        [Required(ErrorMessage = "Campul Cod de Access este obligatoriu")]
        public string AccessCode { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "Câmpul email este obligatoriu")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Câmpul Parolă Nouă este obligatoriu")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Parolă Nouă")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmă Parola")]
        [Compare("Password", ErrorMessage = "Parola și confirmarea parolei nu se potrivesc")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
