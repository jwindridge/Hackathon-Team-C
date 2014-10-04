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
using EuromoneyHackathon.Models;

namespace EuromoneyHackathon.External
{
    public class MarkLogicLayer
    {
        private string baseURL = @"http://emhackathon2014-ml-c.cloudapp.net:8004";
        
        //private MarkLogicLayer instance;

        public MarkLogicLayer()
        {

        }

        public static JObject getEventML(string id)
        {
            var client = new RestClient("http://emhackathon2014-ml-c.cloudapp.net:8004/");
            client.Authenticator = new HttpBasicAuthenticator("admin", "M4rkL0gic");
            var request = new RestRequest("v1/documents?uri=event_{id}.json", Method.GET);
            request.AddUrlSegment("id", id);

            RestResponse response = (RestResponse)client.Execute(request);
            var content = response.Content;
            JObject result = JObject.Parse(content);

            /*var results = (from d in ret.Children()
                           where d.Contains("name")
                           select d).ToList();
            */

            return result;
        }

        public static JObject getPersonML(string id)
        {
            var client = new RestClient("http://emhackathon2014-ml-c.cloudapp.net:8004/");
            client.Authenticator = new HttpBasicAuthenticator("admin", "M4rkL0gic");
            var request = new RestRequest("v1/documents?uri=person_{id}.json", Method.GET);
            request.AddUrlSegment("id", id);

            RestResponse response = (RestResponse)client.Execute(request);
            var content = response.Content;
            JObject result = JObject.Parse(content);

            /*var results = (from d in ret.Children()
                           where d.Contains("name")
                           select d).ToList();
            */

            return result;
        }


        public IRestResponse putPerson(Person person)
        {
            RestClient client = new RestClient();
            client.Authenticator = new HttpBasicAuthenticator("admin","M4rkL0gic");
            RestRequest request = new RestRequest(Method.PUT);
            client.BaseUrl = baseURL;
            request.Resource = "v1/documents?uri={uri}.json";
            request.AddUrlSegment("uri", "person_" + person.personId);
            request.AddHeader("Content-Type","application/json");
            return client.Execute(request);
        }
    }
}