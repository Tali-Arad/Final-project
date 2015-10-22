using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TourGuideWebsite.Models
{
    public class UserChanges
    {
        [Required]
        public string UserPhone { get; set; }
        [Required]
        public string UserEmail { get; set; }
    
    }
}