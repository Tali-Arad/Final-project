using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using TourGuideProtocol.DataStruct;

namespace TourGuideWebsite.Models
{
    public class UserProfile
    {
        public List<AReg> UserRegs { get; set; }
        public UserChanges UserChanges { get; set;}
    }
}
   