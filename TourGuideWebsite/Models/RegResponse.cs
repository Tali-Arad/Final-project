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
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [DataType(DataType.Date)] 
        public DateTime Birthday { get; set; }
        [Required]
        public bool WillAttend { get; set; }
        public AEvent EventInfo { get; set; }
        public AUser UserInfo { get; set; }  
    }
}

