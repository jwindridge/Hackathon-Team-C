using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EuromoneyHackathon.Models
{
    public class Conference
    {
        public string conferenceId { get; set; }
        public string conferenceName { get; set; }
        
        /// <summary>
        /// create new conference ID
        /// </summary>
        public Conference() 
        {
            String rawId = Guid.NewGuid().ToString();
            rawId = rawId.Replace("{", "").Replace("}", "").Replace("-", "");
            conferenceId = rawId;
        }
        /// <summary>
        /// create Conference form id
        /// </summary>
        /// <param name="confId"></param>
        public Conference(string conferenceId)
        {
            this.conferenceId = conferenceId;
        }

    }
}