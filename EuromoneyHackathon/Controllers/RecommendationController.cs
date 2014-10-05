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
using System.Web.Http;

namespace EuromoneyHackathon.Controllers
{
    public class RecommendationController : ApiController
    {
        //
        // GET: /Attendance/
        [HttpPut]
        public Response Recommend([FromBody] dynamic values)
        {
            String pid = values.pid;  // personId
            String eid = values.eid;  // eventId
            Response response = new Response();

            Event[] recommendedEvent = JsonConvert.DeserializeObject<Event[]>(MarkLogicLayer.getRecommendedEventFromPersonId(eid).ToString());

            JArray jsonPayload = JArray.FromObject(recommendedEvent);
            response.payload = jsonPayload;
            return response;
        }
    }
}
