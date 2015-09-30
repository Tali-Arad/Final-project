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
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime TourDate { get; set; }
        public DateTime TourOriginalDate { get; set; }
        [Required]
        public string TourGuide { get; set; }
        [Required]
        public int IsOn { get; set; }
    }
}
