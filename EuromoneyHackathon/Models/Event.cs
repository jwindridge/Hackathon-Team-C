using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EuromoneyHackathon.Models
{
    public class Event
    {
        public string Id { get; set; }
        public string EventName { get; set; }

        /// <summary>
        /// Create event, generating new random Id, with name specified by user
        /// </summary>
        public Event(string name)
        {
            this.Id = Guid.NewGuid().ToString().Replace("{", "").Replace("}", "").Replace("-", "");
            this.EventName = name;
        }

        /// <summary>
        /// Create Event from existing Id and/or name
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="name"></param>
        public Event(string eventId, string name)
        {
            this.Id = eventId;
            this.EventName = EventName;
        }

        public Event()
        {
            this.Id = Guid.NewGuid().ToString().Replace("{", "").Replace("}", "").Replace("-", "");
        }

    }
}