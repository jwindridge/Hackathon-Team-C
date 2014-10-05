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
          new Person("John","Smith","https://www.linkedin.com/pub/john-smith/12/345/678","john.smith@example.com"),
          new Person("Alex","Jones","https://www.linkedin.com/pub/alex-jones/98/765/432","alex.jones@test.com")
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

        public Response getPerson(string pid)
        {
            Response response = new Response();
            var person = MarkLogicLayer.getPersonML(pid);
            //var _event = events.FirstOrDefault((p)=> p.EventName == name);         

            if (person == null)
            {
                response.errorCode = 404;
                response.errorText = "EVENT_NOT_FOUND";
            }
            else
            {
                response.payload = person;
            }

            return response; 
        }
        [HttpPut]
        public Response PutPerson([FromBody]Person person)
        {
            Response response = new Response();
            String mlResponse = MarkLogicLayer.putPerson(person);
            JObject jsonPayload = JObject.FromObject(person);
            response.payload = jsonPayload;
            return response;
        }

        
    }
}