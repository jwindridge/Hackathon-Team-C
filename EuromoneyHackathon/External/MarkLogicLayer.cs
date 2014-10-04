using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RestSharp;
using EuromoneyHackathon.Models;

namespace EuromoneyHackathon.External
{
    public class MarkLogicLayer
    {
        private string baseURL = @"http://emhackathon2014-ml-c.cloudapp.net:8004";
        
        private MarkLogicLayer instance;

        public MarkLogicLayer()
        {

        }

        public IRestResponse putPerson(Person person){
            RestClient client = new RestClient();
            client.Authenticator = new SimpleAuthenticator("username", "admin", "password", "M4rkL0gic");
            RestRequest request = new RestRequest(Method.PUT);
            request.Resource = "v1/documents?uri={uri}.json";
            request.AddUrlSegment("uri", "person_" + person.personId);
            request.AddHeader("Content-Type","application/json");
            return client.Execute(request);
        }
    }
}