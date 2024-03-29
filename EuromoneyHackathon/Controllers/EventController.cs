﻿using EuromoneyHackathon.Models;
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
            new Event("5F399EF38C824242810224EA877AFFF2","ASP.NET Talk"),
            new Event("D5DB9C9B995D43829A70467AFA0C1229","Big Data & MarkLog")
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
            var _event = MarkLogicLayer.getEventMLById(eid);
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
       //    MarkLogicLayer.getEventMLById(eid);
        //}

        public Response PutEvent([FromBody]Event eventToPut)
        {
            Response response = new Response();
            String mlResponse = MarkLogicLayer.putEvent(eventToPut);
            JObject jsonPayload = JObject.FromObject(eventToPut);
            response.payload = jsonPayload;
            return response;
        }
    }
}