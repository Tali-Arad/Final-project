using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourGuideProtocol.DataStruct
{
    public class ATour
    {
        public string TourID { get; set; }
        public string TourName { get; set; }
        public string TourLocation  { get; set; }
        public string TourArea { get; set; }
        public string TourCategory { get; set; }
        public int TourDuration { get; set; }
        public double TourPrice{ get; set; }
        public int MinReg { get; set; }
        public int MaxReg { get; set; }
        public string TourDescription { get; set; }
    }
}
