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


        public List<AUser> GetUsers()
        {
            return TourDAL.GetUsers();
        }

        public List<AReg> GetRegistrations()
        {
            return TourDAL.GetRegistrations();
        }

        public AUser GetUserByUsername(string username)
        {
            return TourDAL.GetUserByUsername(username);
        }

        public bool AddUser(AUser userReg)
        {
            return TourDAL.AddUser(userReg);
        }

        public bool AddTour(ATour tour)
        {
            return TourDAL.AddTour(tour);
        }

        public bool AddEvent(AEvent tourEvent)
        {
            return TourDAL.AddEvent(tourEvent);
        }
        public bool AddReg(AReg reg)
        {
            return TourDAL.AddReg(reg);
        }
        public bool EditEvent(AEvent tourEvent)
        {
            return TourDAL.EditEvent(tourEvent);
        }

        public bool DeleteEvent(string tourid, DateTime tourdate)
        {
            return TourDAL.DeleteEvent(tourid, tourdate);
        }
        public bool EditReg(AReg reg)
        {
            return TourDAL.EditReg(reg);
        }
        public bool DeleteReg(string id)
        {
            return TourDAL.DeleteReg(id);
        }
        public bool EditTour(ATour tour)
        {
            return TourDAL.EditTour(tour);
        }
        public bool DeleteTour(string id)
        {
            return TourDAL.DeleteTour(id);
        }
        public bool EditUser(AUser user)
        {
            return TourDAL.EditUser(user);
        }
        public bool DeleteUser(string id)
        {
            return TourDAL.DeleteUser(id);
        }
        public List<AEvent> GetEvents(string tourName)
        {
            return TourDAL.GetEvents(tourName);
        }
        public List<ATour> GetTours(string keyword)
        {
            return TourDAL.GetTours(keyword);
        }
        public List<AEvent> GetEventsByTourId(string id)
        {
            return TourDAL.GetEventsByTourId(id);
        }
        public List<AEvent> GetEventsByTourField(string sort)
        {
            return TourDAL.GetEventsByTourField(sort);
        }

        public List<AEvent> GetEventsByEventField(string sort)
        {
            return TourDAL.GetEventsByEventField(sort);
        }


    }
}

