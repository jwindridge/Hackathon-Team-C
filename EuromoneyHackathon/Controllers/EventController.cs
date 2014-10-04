using EuromoneyHackathon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EuromoneyHackathon.Controllers
{
    public class EventController:ApiController
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
        public JObject GetAllEvents()
        {
            JObject response = new JObject();
            response.Add("errorCode", 0);
            JArray payload = JArray.FromObject(events);
            response.Add("response", payload);
            return response;
        }

        /// <summary>
        /// Get one event with event name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public String GetEvent(string name)
        {
            var _event = events.FirstOrDefault((p)=> p.eventName == name);         

            JObject _response = new JObject();

            _response.Add("errorCode",0);
            if (_event == null)
            {
                _response.Add("errorCode","EVENT_NOT_FOUND");//NotFound();
            }
            else {
                _response.Add("response", JsonConvert.SerializeObject(_event));
            }
            
            return _response.ToString(); 
        }
    }
}