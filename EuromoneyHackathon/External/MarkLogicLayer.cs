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
using RestSharp;
using EuromoneyHackathon.Models;
using System.Configuration;
using System.Text;

namespace EuromoneyHackathon.External
{
    public class MarkLogicLayer
    {
        private string baseURL = @"http://emhackathon2014-ml-c.cloudapp.net:8004/v1/documents";
        
        private MarkLogicLayer instance;

        public MarkLogicLayer()
        {

        }

        public static String getFromML(string id)
        {
            var client = new RestClient("http://emhackathon2014-ml-c.cloudapp.net:8004/");
            var request = new RestRequest("v1/documents?uri=event_{id}.json", Method.GET);
            request.AddUrlSegment("id", id);

            RestResponse response = (RestResponse)client.Execute(request);
            var content = response.Content;
            //JObject result = JObject.Parse(content);

            /*var results = (from d in ret.Children()
                           where d.Contains("name")
                           select d).ToList();
            */

            return content;
        }

        public String putPerson(Person person){
            String requestUrl = baseURL + "?uri=person_" + person.Id + ".json";
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(requestUrl);
            string authInfo = "admin:M4rkL0gic";
            authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(authInfo));
            request.Headers["Authorization"] = "Basic " + authInfo;
            request.ContentType = "application/json";
            request.Method = "PUT";
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                string json = JObject.FromObject(person).ToString();
                streamWriter.Write(json);
                streamWriter.Flush();
            }

            var httpResponse = (HttpWebResponse)request.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
            }
            return httpResponse.StatusCode.ToString() + " - " + httpResponse.StatusDescription;
        }

    }
}