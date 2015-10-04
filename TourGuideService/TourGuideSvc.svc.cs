using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using TourGuideBLL;
using TourGuideProtocol.DataStruct;
using System.Web.Mvc;

namespace TourGuideService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class TourGuideSvc : ITourGuideSvc
    {
        public List<SelectListItem> GetTourDates(TourName tourName)
        {
          //  string tourName = "Jerusalem highlights";
            BTourGuideOp op = new BTourGuideOp();
            List<AEvent> tourEvents = op.GetEvents(tourName.Name);
            List<SelectListItem> tourDates = new List<SelectListItem>();
            foreach (AEvent tourEvent in tourEvents)
            {
                tourDates.Add(new SelectListItem { Text = tourEvent.TourDate.ToString(), Value = tourEvent.TourDate.ToString() });
            }
            return tourDates;
            
        }
    }
}
