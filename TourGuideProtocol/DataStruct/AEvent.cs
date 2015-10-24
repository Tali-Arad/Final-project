using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TourGuideProtocol.DataStruct
{
    public class AEvent
    {
        public string TourID { get; set; }
        public string TourName { get; set; }
        [DataType(DataType.DateTime)]
        [CustomEventDateAttribute]
        public DateTime TourDate { get; set; }
        public DateTime TourOriginalDate { get; set; }
        [Required(ErrorMessage="Enter a name of a tour guide")]
        public string TourGuide { get; set; }
        public bool IsOn { get; set; }
    }

    public class CustomEventDateAttribute : RangeAttribute
    {
        public CustomEventDateAttribute()
            : base(typeof(DateTime),
                    DateTime.Now.ToShortDateString(),
                    DateTime.MaxValue.ToShortDateString())
        { }
    }


}
