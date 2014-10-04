using EuromoneyHackathon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using EuromoneyHackathon.External;

namespace EuromoneyHackathon.Controllers
{
    public class EventController : ApiController
    {
        Event[] events = new Event[]{
            new Event("ASP.NET Talk"),
            new Event("Big Data & MarkLog")
        };

        /// <summary>
        /// Get all events in JSON format
        /// </summary>
        /// <returns></returns>
        /// 
        public Response GetAllEvents()
        {
            Response response = new Response();
            response.payload = events;
            return response;
        }

        /// <summary>
        /// Get one event with event name in JSON format
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Response GetEvent(string eid)
        {
            Response response = new Response();
            var _event = MarkLogicLayer.getEventML(eid);
            //var _event = events.FirstOrDefault((p)=> p.EventName == name);         
          
            if (_event == null)
            {
                response.errorCode = 404;
                response.errorText = "EVENT_NOT_FOUND" ; 
            }
            else {
                response.payload = _event;
            }
            
            return response; 
        }

       // public void queryEvent(string eid)
        //{
       //    MarkLogicLayer.getEventML(eid);
        //}

        public Response PutEvent([FromBody]Event eventToPut)
        {
            Response response = new Response();
            String mlResponse = MarkLogicLayer.putEvent(eventToPut);
            JObject jsonPayload = new JObject();
            jsonPayload.Add("server-message", mlResponse);
            response.payload = jsonPayload;
            return response;
        }
    }
}