using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using TourGuideProtocol.DataStruct;
using System.Web.Mvc;
using System.Data.Services.Client;
using System.Web;

namespace TourGuideService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface ITourGuideSvc
    {
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/GetTourDates", RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        List<SelectListItem> GetTourDates(TourName tourName);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/SortToursByTourField", RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        List<AEvent> SortToursByTourField(TourField tourField);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/SortToursByEventField", RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        List<AEvent> SortToursByEventField(EventField eventField);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/GetUpcomingEvents", RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        List<AEvent> GetUpcomingEvents();
       

    }

    [DataContract]
    public class TourName
    {
        [DataMember]
        public string Name { get; set; }
    }

    [DataContract]
    public class TourField
    {
        [DataMember]
        public string Field { get; set; }
    }

    [DataContract]
    public class EventField
    {
        [DataMember]
        public string Field { get; set; }
    }
       
}
