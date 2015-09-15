using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourGuideProtocol.DataInt;
using TourGuideProtocol.DataStruct;
using TourGuideDAL;


namespace TourGuideBLL
{
    public class BTourGuideOp
    {
        ITourOp TourDAL = null;

        public BTourGuideOp()
        {
            TourDAL = (ITourOp)new TourGuideOp();
        }
        public List<ATour> GetTours()
        {
            return TourDAL.GetTours();
        }

        public ATour GetTourByID(string tourID)
        {
            return TourDAL.GetTourByID(tourID);
        }

        public List<AEvent> GetEvents()
        {
            return TourDAL.GetEvents();
        }

        public AEvent GetEvent(string id, DateTime date)
        {
            return TourDAL.GetEvent(id, date);
        }

        public bool AddReg(AReg reg)
        {
            return TourDAL.AddReg(reg);
        }

        public List<AUser> GetUsers()
        {
            return TourDAL.GetUsers();
        }

        public AUser GetUserByUsername(string username)
        {
            return TourDAL.GetUserByUsername(username);
        }

        public bool AddUser(AUser userReg)
        {
            return TourDAL.AddUser(userReg);
        }
    }
}

