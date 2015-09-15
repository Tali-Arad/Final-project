using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourGuideProtocol.DataStruct
{
    public class AUser
    {
        public string UserID { get; set; }
        public DateTime? RegTime { get; set; }
        public string UserIP { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string UserPhone { get; set; }
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
        public string Username { get; set; }
        public DateTime UserBirthday { get; set; }
    }
}
