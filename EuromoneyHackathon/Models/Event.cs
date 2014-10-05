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
        public string[] Interests { get; set; }
        public string[] Attendees { get; set; }
        public string Location { get; set; }
        public string Type = "Event";
        public DateTime EventStartTime { get; set; }
        public DateTime EventEndTime { get; set; }

        /// <summary>
        /// Create event, generating new random Id, with name specified by user
        /// </summary>
        public Event(string name)
        {
            this.Id = Guid.NewGuid().ToString().Replace("{", "").Replace("}", "").Replace("-", "");
            this.EventName = name;
            this.Interests = new string[] { };
            this.Attendees = new string[] { };
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
            this.Interests = new string[] { };
            this.Attendees = new string[] { };
        }

        public Event()
        {
            this.Id = Guid.NewGuid().ToString().Replace("{", "").Replace("}", "").Replace("-", "");
            this.Interests = new string[] { };
            this.Attendees = new string[] { };
            this.EventStartTime = new DateTime();
            this.EventEndTime = new DateTime();
        }

    }
}