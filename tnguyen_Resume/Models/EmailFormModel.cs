using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace tnguyen_Resume.Models
{
    public class EmailFormModel
    {
        [Display(Name = "Name *")]
        public string FirstName { get; set; }
        [Display(Name = "Phone *"), StringLength(11, MinimumLength = 8, ErrorMessage = "Phone number must be at least 8 characters")]
        public string Phone { get; set; }
        public string Subject { get; set; }
        [Required]
        public string Message { get; set; }
    }
}