using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace aspfunda.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(32 , ErrorMessage ="Name can not be too long")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email is required."), 
            RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
            ErrorMessage = "Email is Not Valid.")]
       // [Display(Name="Office Email")]
        public string Email{ get; set; }
        [Required]

        public dept?  Department { get; set; }
        public string PhotoPath { get; set; }

    }
}
