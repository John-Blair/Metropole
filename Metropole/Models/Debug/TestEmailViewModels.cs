using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace Metropole.Models
{
    public class TestEmailViewModel
    {
        [Required]
        [Display(Name = "Email To:")]
        [EmailAddress]
        public string EmailTo { get; set; }


        [Required]
        [StringLength(255, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 1)]
        [Display(Name = "Subject")]
        public string Subject { get; set; }

        [Required]
        [StringLength(10000, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 1)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Body")]
        public string Body { get; set; }
    }

}