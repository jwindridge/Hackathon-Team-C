using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using EuromoneyHackathon.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;



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
        public String GetAllPeople()
        {
            JObject response = new JObject();
            response.Add("errorCode", 0);
            response.Add("response", JsonConvert.SerializeObject(people));
            return response.ToString();
        }

        public String getPerson(string firstName, string lastName)
        {
            JObject response = new JObject();
            response.Add("errorCode",0);
            var person = people.FirstOrDefault((x)=>(x.personFirstName == firstName && x.personLastName == lastName));
            if (person == null){
                response.Add("errorCode","PERSON_NOT_FOUND");
            } else {
                response.Add("response",JsonConvert.SerializeObject(person));
            }
            return response.ToString();
        }
    }
}