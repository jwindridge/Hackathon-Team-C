using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using EuromoneyHackathon.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using EuromoneyHackathon.External;
using RestSharp;



namespace EuromoneyHackathon.Controllers
{

    public class PersonController : ApiController
    {
        Person[] people = new Person[]{
          new Person("John","Smith"),
          new Person("Alex","Jones")
        };
        /// <summary>
        /// Basic method to return a list of all people stored in the database (currently maintaned as static above)
        /// </summary>
        /// <returns>Array of all people stored in the system</returns>
        public Response GetAllPeople()
        {
            Response response = new Response();
            response.payload = people;
            return response;
        }

        public Response getPerson(string firstName, string lastName)
        {
            Response response = new Response();
            var person = people.FirstOrDefault((x)=>(x.personFirstName == firstName && x.personLastName == lastName));
            if (person == null){
                response.errorCode = 404;
                response.errorText = "PERSON_NOT_FOUND";
            } else {
                response.payload = person;
            }
            return response;
        }
        [HttpPut]
        public Response PutPerson([FromBody]Person person)
        {
            Response response = new Response();
            MarkLogicLayer layer = new MarkLogicLayer();
            RestResponse mlResponse = (RestResponse)layer.putPerson(person);
            JObject jsonPayload = new JObject();
            jsonPayload.Add(mlResponse.StatusCode + " - " + mlResponse.StatusDescription);
            response.payload = jsonPayload;
            return response;
        }

        public void queryPerson(string eid)
        {
            MarkLogicLayer.getPersonML(eid);
        }

    }
}