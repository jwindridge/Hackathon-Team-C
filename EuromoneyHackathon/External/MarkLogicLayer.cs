using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace EuromoneyHackathon.External
{
    public class MarkLogicLayer
    {
        private MarkLogicLayer instance;

        private MarkLogicLayer()
        {

        }

        public static String getFromML(string id)
        {
            var client = new RestClient("http://emhackathon2014-ml-c.cloudapp.net:8004/");
            var request = new RestRequest("v1/documents?uri=event_{id}.json", Method.GET);
            request.AddUrlSegment("id",id);

            RestResponse response = (RestResponse) client.Execute(request);
            var content = response.Content;
            //JObject result = JObject.Parse(content);

            /*var results = (from d in ret.Children()
                           where d.Contains("name")
                           select d).ToList();
            */

            return content;
        }
    }
}