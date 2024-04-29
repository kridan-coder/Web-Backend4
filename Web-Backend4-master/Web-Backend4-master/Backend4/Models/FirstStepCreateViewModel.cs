using System;
using System.ComponentModel.DataAnnotations;

namespace Backend4.Models
{
    public class FirstStepCreateViewModel
    {
        [Required]
        public String FirstName { get; set; }

        [Required]
        public String LastName { get; set; }

        public Int32 Day { get; set; }
        public Int32 Month { get; set; }
        public Int32 Year { get; set; }

        [Required]
        public Int32? Gender { get; set; }
    }
}
