using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourGuideProtocol.DataStruct;

namespace TourGuideProtocol.DataInt
{
    public interface ITourOp
    {
        List<ATour> GetTours();
        List<ATour> GetTours(string keyword);
        List<AEvent> GetEvents(DateTime date);
        List<AEvent> GetEvents();
        ATour GetTourByID(string tourID);
        AEvent GetEvent(string id, DateTime date);
        bool AddReg(AReg reg);
        List<AUser> GetUsers();
        AUser GetUserByUsername(string username);
        bool AddUser(AUser userReg);
        List<AReg> GetRegistrations();
        bool AddTour(ATour tour);
        bool AddEvent(AEvent tourEvent);
        bool EditEvent(AEvent tourEvent);
        bool DeleteEvent(string tourid, DateTime tourdate);
        bool EditReg(AReg reg);
        bool DeleteReg(string id);
        bool EditTour(ATour tour);
        bool DeleteTour(string id);
        bool EditUser(AUser user);
        bool DeleteUser(string id);


    }
}
