using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourGuideProtocol.DataStruct
{
    public class AEvent
    {
        public string TourID { get; set; }
        public string TourName { get; set; }
        public DateTime TourDate { get; set; }
        public string TourGuide { get; set; }
        public int IsOn { get; set; }
    }
}
