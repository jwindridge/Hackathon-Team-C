using EuromoneyHackathon.External;
using EuromoneyHackathon.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Http;


namespace EuromoneyHackathon.Controllers
{
    public class RecommendationController : ApiController
    {
        //
        // GET: /Attendance/
        [HttpGet]
        public Response GetRecommendation()
        {
            String pid = HttpContext.Current.Request.QueryString["pid"];
            //String pid = Request.QueryString["pid"];  // personId
            Response response = new Response();

            Event[] recommendedEvent = JsonConvert.DeserializeObject<Event[]>(MarkLogicLayer.getRecommendedEventFromPersonId(pid).ToString());

            JArray jsonPayload = JArray.FromObject(recommendedEvent);
            response.payload = jsonPayload;
            return response;
        }
    }
}
