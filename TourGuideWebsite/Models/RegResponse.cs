using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TourGuideProtocol.DataStruct;
using System.ComponentModel.DataAnnotations;

namespace TourGuideWebsite.Models
{
    public class RegResponse
    {
        [Required(ErrorMessage = "Please enter first name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please enter last name")]
        public string LastName { get; set; }
        [Required]
        [CustomDateAttribute]
        [DataType(DataType.Date)]
        [Display(Name = "Birthday")]
        public DateTime Birthday { get; set; }
        public bool WillAttend { get; set; }
        public AEvent EventInfo { get; set; }
        public AUser UserInfo { get; set; }  
    }


    public class CustomDateAttribute : RangeAttribute
    {
        public CustomDateAttribute()
            : base(typeof(DateTime),
                    DateTime.Now.AddYears(-120).ToShortDateString(),
                    DateTime.Now.ToShortDateString())
        { }
    }
}

