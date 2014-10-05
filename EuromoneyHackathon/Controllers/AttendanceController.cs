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
    public class AttendanceController : ApiController
    {
        //
        // GET: /Attendance/
        [HttpPut]
        public Response registerAttendance([FromBody] dynamic values)
        {
            /*
             {
                "pid" : "" ,
                "eid" : ""
             }
            */
            String pid = values.pid;  // personId
            String eid = values.eid;  // eventId
            Response response = new Response();
            //JObject eventJson = MarkLogicLayer.getEventMLById(eid);
            Event eventObject = JsonConvert.DeserializeObject<Event>(MarkLogicLayer.getEventMLById(eid).ToString());
            List<string> attendees = eventObject.Attendees.ToList();
            attendees.Add(pid);
            eventObject.Attendees = attendees.ToArray();
            MarkLogicLayer.putEvent(eventObject);
            //Person personObject = JsonConvert.DeserializeObject<Person>(MarkLogicLayer.getPersonMLById(eid).ToString());
            //String res1 = MarkLogicLayer.putEvent(new Event(pid));
            //String res2 = MarkLogicLayer.putPerson(new Person(pid));
            JObject jsonPayload = JObject.FromObject(eventObject);
            response.payload = jsonPayload;
            return response;
        }

    }
}
