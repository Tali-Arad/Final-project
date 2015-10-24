using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TourGuideWebsite.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage="Please enter your username")]
        public string UserName { get; set; }
        [Required(ErrorMessage="Please enter your password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

}


