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
        [DataType(DataType.DateTime)]
        public DateTime TourDate { get; set; }
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string TourName { get; set; }
        [Required(ErrorMessage="Please enter first name")]
        public string RegFirstName { get; set; }
        [Required(ErrorMessage = "Please enter last name")]
        public string RegLastName { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [CustomDateAttribute]
        [Display(Name = "Birthday")]
        public DateTime RegBirthday { get; set; }
        public DateTime RegTime { get; set; }
        [Required]
        public bool WillAttend { get; set; }
        public bool IsPaid { get; set; }
        public bool IsSentEmail { get; set; }
        public bool Attended { get; set; }

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
