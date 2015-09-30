using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TourGuideProtocol.DataStruct
{
    public class AUser
    {
        public string UserID { get; set; }
        public DateTime? RegTime { get; set; }
        public string UserIP { get; set; }
        [Required]
        public string UserFirstName { get; set; }
        [Required]
        public string UserLastName { get; set; }
        [Required]
        public string UserPhone { get; set; }
        [Required]
        public string UserEmail { get; set; }
        [Required]
        public string UserPassword { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime UserBirthday { get; set; }
    }
}
