using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TourGuideWebsite.Models
{
    public class UserDetails
    {   [Required]
        public string UserFirstName { get; set; }
        [Required]
        public string UserLastName { get; set; }
        [Required]
        public string UserPhone { get; set; }
        [Required]
        public string UserEmail { get; set; }
        [Required]
        [DataType(DataType.Date)] 
        public DateTime UserBirthday { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)] 
        public string UserPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string ConfirmPass { get; set; }
       
    }
}