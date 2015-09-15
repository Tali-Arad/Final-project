using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TourGuideProtocol.DataStruct;

namespace TourGuideWebsite.Models
{
    public class EventDetails
    {
        public AEvent eventInfo { get; set; } 
        public ATour tourInfo { get; set; }
    }
}