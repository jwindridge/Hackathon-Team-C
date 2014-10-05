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
using System.Configuration;
using System.Text;

namespace EuromoneyHackathon.External
{
    public class MarkLogicLayer
    {
        private static string baseDocumentUrl = @"http://emhackathon2014-ml-c.cloudapp.net:8004/v1/documents";
        private static string baseSearchUrl = @"http://emhackathon2014-ml-c.cloudapp.net:8004/v1/keyvalue?key={0}&value={1}&format=json";
        
        //private MarkLogicLayer instance;

        public MarkLogicLayer()
        {

        }

        public static JObject getEventMLById(string id)
        {
            var client = new RestClient(baseDocumentUrl);
            client.Authenticator = new HttpBasicAuthenticator("admin", "M4rkL0gic");
            var request = new RestRequest("?uri=event_{id}.json", Method.GET);
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

        public static JObject getPersonMLById(string id)
        {
            var client = new RestClient(baseDocumentUrl);
            client.Authenticator = new HttpBasicAuthenticator("admin", "M4rkL0gic");
            var request = new RestRequest("?uri=person_{id}.json", Method.GET);
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

        public static Person getPersonMLByEmail(string email)
        {
            String personSearchURL = String.Format(baseSearchUrl, "EmailAddress", HttpUtility.UrlEncode(email));
            HttpWebRequest personSearchRequest = (HttpWebRequest)HttpWebRequest.Create(personSearchURL);
            personSearchRequest.Method = "GET";
            personSearchRequest.Accept = "application/json";
            string authInfo = "admin:M4rkL0gic";
            authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(authInfo));
            personSearchRequest.Headers["Authorization"] = "Basic " + authInfo;
            var httpPersonSearchResponse = (HttpWebResponse)personSearchRequest.GetResponse();
            JObject personSearchResultObject;
            using (var streamReader = new StreamReader(httpPersonSearchResponse.GetResponseStream()))
            {
                var personSearchResult = streamReader.ReadToEnd();
                personSearchResultObject = JObject.Parse(personSearchResult);
            }

            if (int.Parse(personSearchResultObject.GetValue("total").ToString()) == 1)
            {
                //String personSearchResultObject.GetValue("results")
                JArray jResultArray = JArray.FromObject(personSearchResultObject.GetValue("results"));
                JObject result = JObject.FromObject(jResultArray[0]);
                String personUri = result["uri"].ToString();

                String personGetUrl = String.Format(baseDocumentUrl + "?uri={0}", personUri);
                HttpWebRequest personGetRequest = (HttpWebRequest)HttpWebRequest.Create(personGetUrl);
                personGetRequest.Method = "GET";
                personGetRequest.Accept = "application/json";
                personGetRequest.Headers["Authorization"] = "Basic " + authInfo;
                var httpPersonGetResponse = (HttpWebResponse)personGetRequest.GetResponse();
                Person personGetResultObject;
                using (var streamReader = new StreamReader(httpPersonGetResponse.GetResponseStream()))
                {
                    var personGetResult = streamReader.ReadToEnd();
                    personGetResultObject = JsonConvert.DeserializeObject<Person>(personGetResult);
                }
                return personGetResultObject;
                //Add LinkedIn information               
            }

            return null;
        }

        public static String putPerson(Person person){
            String requestUrl = baseDocumentUrl + "?uri=person_" + person.Id + ".json";
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

        public static String putEvent(Event eventToPut)
        {
            String requestUrl = baseDocumentUrl + "?uri=event_" + eventToPut.Id + ".json";
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(requestUrl);
            string authInfo = "admin:M4rkL0gic";
            authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(authInfo));
            request.Headers["Authorization"] = "Basic " + authInfo;
            request.ContentType = "application/json";
            request.Method = "PUT";
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                string json = JObject.FromObject(eventToPut).ToString();
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


        public static JArray getRecommendedEventFromPersonId(string pid)
        {
            String recommendUrl = "http://emhackathon2014-ml-c.cloudapp.net:8003/people2.xqy";
            var client = new RestClient(recommendUrl);
            client.Authenticator = new HttpBasicAuthenticator("admin", "M4rkL0gic");
            var request = new RestRequest("?id = {id}", Method.GET);
            request.AddUrlSegment("id", pid);

            RestResponse response = (RestResponse)client.Execute(request);
            var content = response.Content;
            JArray result = JArray.Parse(content);

            return result;
        }

    }
}