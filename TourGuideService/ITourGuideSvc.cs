using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using TourGuideProtocol.DataStruct;
using System.Web.Mvc;

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

    }

    [DataContract]
    public class TourName
    {
        [DataMember]
        public string Name { get; set; }
    }
       
}
