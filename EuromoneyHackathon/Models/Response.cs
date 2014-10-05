using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EuromoneyHackathon.Models
{
    public class Response
    {
        public int errorCode { get; set; }
        public string errorText { get; set; }
        public object payload { get; set; }

        public Response()
        {
            errorCode = 0;
        }
    }
}