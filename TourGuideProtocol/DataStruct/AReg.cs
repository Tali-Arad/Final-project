using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourGuideProtocol.DataStruct
{
    public class AReg
    {
        public string RegID { get; set; }
        public string TourID { get; set; }
        public DateTime TourDate { get; set; }
        public string UserID { get; set; }
        public string RegFirstName { get; set; }
        public string RegLastName { get; set; }
        public DateTime RegBirthday { get; set; }
        public DateTime RegTime { get; set; }
        public int WillAttend { get; set; }
        public int IsPaid { get; set; }
        public int IsSentEmail { get; set; }
        public int Attended { get; set; }

    }
}
