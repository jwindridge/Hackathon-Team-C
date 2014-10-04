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
        /// Create event, generating new random eventId
        /// </summary>
        public Event()
        {
            this.eventId = Guid.NewGuid().ToString().Replace("{", "").Replace("}", "").Replace("-", "");
        }

        /// <summary>
        /// Create event, specifing eventId (used when initialising event class from data)
        /// </summary>
        /// <param name="eventId"></param>
        public Event(string eventId)
        {
            this.eventId = eventId;
        }
    }
}