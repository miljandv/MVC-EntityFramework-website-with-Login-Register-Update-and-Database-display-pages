using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MVC_test.BL.Records.Model
{
    public class RecordEntityModel
    {
        [Display(Name = "First Name"), Required(ErrorMessage = "Field cannot be left empty"), StringLength(100, ErrorMessage = "Invalid name length!"), DataType(DataType.Text)]
        public string Fname { get; set; }
        [Display(Name = "Last Name"), Required(ErrorMessage = "Field cannot be left empty"), StringLength(100, ErrorMessage = "Invalid surname length!"), DataType(DataType.Text)]
        public string Lname { get; set; }
        [Display(Name = "Phone"), Required(ErrorMessage = "Field cannot be left empty"), StringLength(15, ErrorMessage = "Invalid phone length!"), DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        [Display(Name = "Password"), Required(ErrorMessage = "Field cannot be left empty"), StringLength(100, MinimumLength = 8, ErrorMessage = "Invalid password length!"), DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
