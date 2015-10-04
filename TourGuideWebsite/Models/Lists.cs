using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TourGuideBLL;
using TourGuideProtocol.DataStruct;

namespace TourGuideWebsite.Models
{
    public class Lists
    {
        public static List<SelectListItem> CreateTourList()
        {
            // Creating a dropdown list for TourNames:
            List<SelectListItem> tourNames = new List<SelectListItem>();
            BTourGuideOp tourOp = new BTourGuideOp();
            List<ATour> tours = tourOp.GetTours();
            foreach (ATour tour in tours)
            {
                tourNames.Add(new SelectListItem { Text = tour.TourName, Value = tour.TourName });
            }
            return tourNames;
        }

        public static List<SelectListItem> CreateUserList()
        {
            // Creating a dropdown list for Users Names:
            List<SelectListItem> userNames = new List<SelectListItem>();
            BTourGuideOp tourOp = new BTourGuideOp();
            List<AUser> users = tourOp.GetUsers();
            foreach (AUser user in users)
            {
                userNames.Add(new SelectListItem { Text = user.Username, Value = user.Username });
            }
            return userNames;
        }

        public static List<SelectListItem> CreateTourDateList(string tourName)
        {
            // Creating a dropdown list for TourDates:
            List<SelectListItem> tourDates = new List<SelectListItem>();
            BTourGuideOp tourOp = new BTourGuideOp();
            List<AEvent> events = tourOp.GetEvents(tourName);
            foreach (AEvent tourEvent in events)
            {
                tourDates.Add(new SelectListItem { Text = tourEvent.TourDate.ToString(), Value = tourEvent.TourDate.ToString() });
            }
            return tourDates;
        }

        // TODO: Add functions that create lists for TourLocation, TourArea and TourCategory (create these tables in the DB first)
        

    }
}