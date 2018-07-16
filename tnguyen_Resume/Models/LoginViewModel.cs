using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace tnguyen_Resume.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Username don't exist")]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password don't exist")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        public bool Remember { get; set; }
    }
}