using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using TourGuideBLL;
using TourGuideProtocol.DataStruct;
using System.Web;
using System.Web.Mvc;
using System.Data.Services.Client;


namespace TourGuideService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class TourGuideSvc : ITourGuideSvc
    {
        public List<SelectListItem> GetTourDates(TourName tourName)
        {
            BTourGuideOp op = new BTourGuideOp();
            List<AEvent> tourEvents = op.GetEvents(tourName.Name);
            List<SelectListItem> tourDates = new List<SelectListItem>();
            foreach (AEvent tourEvent in tourEvents)
            {
                tourDates.Add(new SelectListItem { Text = tourEvent.TourDate.ToString(), Value = tourEvent.TourDate.ToString() });
            }
            return tourDates;
        }

        public List<AEvent> SortToursByTourField(TourField tourField)
        {
            BTourGuideOp op = new BTourGuideOp();
            List<AEvent> tourEvents = op.GetEventsByTourField(tourField.Field);
            return tourEvents;
        }

        public List<AEvent> SortToursByEventField(EventField eventField)
        {
            BTourGuideOp op = new BTourGuideOp();
            List<AEvent> tourEvents = op.GetEventsByEventField(eventField.Field);
            return tourEvents;
        }

        public List<EventDetails> GetUpcomingEvents()
        {
            // prevent the browser from caching WCF JSON responses
            HttpContext.Current.Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            HttpContext.Current.Response.Cache.SetNoStore();
            //---------------------------------------------------

            BTourGuideOp op = new BTourGuideOp();
            List<EventDetails> tourEvents = new List<EventDetails>();
            List<AEvent> events = op.GetEvents();
            List<ATour> tours = op.GetTours();
            foreach (AEvent tourEvent in events)
            {
                EventDetails eventDetails = new EventDetails();
                if (tourEvent.TourDate > DateTime.Now)
                {
                    if (tourEvents.Count > 2)
                        break;
                    eventDetails.EventInfo = tourEvent;
                    foreach(ATour tour in tours)
                    {
                        if (tour.TourID == tourEvent.TourID)
                        {
                            eventDetails.TourInfo = tour;
                        }
                    }
                    tourEvents.Add(eventDetails);
                }
            }
            return tourEvents;
        }
    }
    }

