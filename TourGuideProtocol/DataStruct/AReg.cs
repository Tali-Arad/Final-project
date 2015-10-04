using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace TourGuideProtocol.DataStruct
{
    public class AReg
    {
        public string RegID { get; set; }
        public string TourID { get; set; }
        [Required]
        public DateTime TourDate { get; set; }
        public string UserID { get; set; }
        [Required]
        public string RegFirstName { get; set; }
        [Required]
        public string RegLastName { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime RegBirthday { get; set; }
        public DateTime RegTime { get; set; }
        [Required]
        public int WillAttend { get; set; }
        public int IsPaid { get; set; }
        public int IsSentEmail { get; set; }
        public int Attended { get; set; }

    }
}
