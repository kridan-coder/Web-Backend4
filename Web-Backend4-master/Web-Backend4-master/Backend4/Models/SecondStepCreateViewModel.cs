using System;
using System.ComponentModel.DataAnnotations;

namespace Backend4.Models
{
    public class SecondStepCreateViewModel : FirstStepCreateViewModel
    {
        [Required]
        [EmailAddress]
        public String Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public String Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public String ConfirmPassword { get; set; }

        public Boolean IsRemembered { get; set; }

        public Boolean Existed { get; set; }
    }
}
