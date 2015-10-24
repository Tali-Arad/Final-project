using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TourGuideWebsite.Models
{
    public class UserDetails
    {
        [Required(ErrorMessage = "Please enter your first name")]
        public string UserFirstName { get; set; }
        [Required(ErrorMessage = "Please enter your last name")]
        public string UserLastName { get; set; }
        [Required(ErrorMessage = "Please enter your phone number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression("^\\s*\\+?\\s*([0-9][\\s-]*){9,}$",
            ErrorMessage="Please enter a valid phone number")]
        public string UserPhone { get; set; }
        [Required(ErrorMessage = "Please enter your Email")]
        [RegularExpression(".+\\@.+\\..+",
        ErrorMessage = "Please enter a valid email address")]
        [DataType(DataType.EmailAddress)]
        public string UserEmail { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [CustomDateAttribute]
        [Display(Name = "Birthday")]
        public DateTime UserBirthday { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [StringLength(10, MinimumLength=6)]
        public string UserPassword { get; set; }
        [Required]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [StringLength(10, MinimumLength = 6)]
        [Compare("UserPassword")]
        public string ConfirmPass { get; set; }
    }
}