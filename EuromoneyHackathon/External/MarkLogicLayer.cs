using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
<<<<<<< HEAD
using System.Net.Http;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
=======
using RestSharp;
using EuromoneyHackathon.Models;
>>>>>>> 6fced3428fea5e2b6e5844de432068459291695b

namespace EuromoneyHackathon.External
{
    public class MarkLogicLayer
    {
        private string baseURL = @"http://emhackathon2014-ml-c.cloudapp.net:8004";
        
        private MarkLogicLayer instance;

        public MarkLogicLayer()
        {

        }

<<<<<<< HEAD
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
=======
        public IRestResponse putPerson(Person person){
            RestClient client = new RestClient();
            client.Authenticator = new SimpleAuthenticator("username", "admin", "password", "M4rkL0gic");
            RestRequest request = new RestRequest(Method.PUT);
            client.BaseUrl = baseURL;
            request.Resource = "v1/documents?uri={uri}.json";
            request.AddUrlSegment("uri", "person_" + person.personId);
            request.AddHeader("Content-Type","application/json");
            return client.Execute(request);
>>>>>>> 6fced3428fea5e2b6e5844de432068459291695b
        }
    }
}