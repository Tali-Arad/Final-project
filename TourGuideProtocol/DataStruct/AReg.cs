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
        [DataType(DataType.DateTime)]
        public DateTime TourDate { get; set; }
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string TourName { get; set; }
        [Required]
        public string RegFirstName { get; set; }
        [Required]
        public string RegLastName { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime RegBirthday { get; set; }
        public DateTime RegTime { get; set; }
        [Required]
        public bool WillAttend { get; set; }
        public bool IsPaid { get; set; }
        public bool IsSentEmail { get; set; }
        public bool Attended { get; set; }

    }
}
