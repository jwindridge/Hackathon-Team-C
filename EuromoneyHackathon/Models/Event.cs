using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EuromoneyHackathon.Models
{
    public class Event
    {
        public string eventId { get; set; }
        public string eventName { get; set; }

        /// <summary>
        /// Create event, generating new random eventId, with name specified by user
        /// </summary>
        public Event(string name)
        {
            this.eventId = Guid.NewGuid().ToString().Replace("{", "").Replace("}", "").Replace("-", "");
            this.eventName = name;
        }

        /// <summary>
        /// Create Event from existing Id and/or name
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="name"></param>
        public Event(string eventId, string name)
        {
            this.eventId = eventId;
            this.eventName = eventName;
        }

        public Event()
        {
            this.eventId = Guid.NewGuid().ToString().Replace("{", "").Replace("}", "").Replace("-", "");
        }

    }
}