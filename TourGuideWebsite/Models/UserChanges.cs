﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TourGuideWebsite.Models
{
    public class UserChanges
    {
        [Required(ErrorMessage = "Please enter your phone number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression("^\\s*\\+?\\s*([0-9][\\s-]*){9,}$",
            ErrorMessage = "Please enter a valid phone number")]
        public string UserPhone { get; set; }
        [Required(ErrorMessage = "Please enter your Email")]
        [RegularExpression(".+\\@.+\\..+",
        ErrorMessage = "Please enter a valid email address")]
        [DataType(DataType.EmailAddress)]
        public string UserEmail { get; set; }
    
    }
}