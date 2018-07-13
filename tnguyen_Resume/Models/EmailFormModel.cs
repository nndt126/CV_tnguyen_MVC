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
        [Display(Name = "Phone *")]
        public string Phone { get; set; }
        public string Subject { get; set; }
        [Required]
        public string Message { get; set; }
    }
}