using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TourGuideProtocol.DataStruct
{
    public class ATour
    {
        public string TourID { get; set; }
        [Required]
        public string TourName { get; set; }
        [Required]
        public string TourLocation  { get; set; }
        [Required]
        public string TourArea { get; set; }
        [Required]
        public string TourCategory { get; set; }
        [Required]
        public int TourDuration { get; set; }
        [Required]
        public decimal TourPrice{ get; set; }
        [Required]
        public int MinReg { get; set; }
        [Required]
        public int MaxReg { get; set; }
        [Required]
        public string TourDescription { get; set; }
    }
}
