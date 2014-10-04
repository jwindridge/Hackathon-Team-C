using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EuromoneyHackathon.Models
{
    public class Person
    {
        public string personId { get; set; }
        public string personFirstName { get; set; }
        public string personLastName { get; set; }

        public Person()
        {
            this.personId = Guid.NewGuid().ToString().Replace("{", "").Replace("}", "").Replace("-", "");
        }

        public Person(String personId)
        {
            this.personId = personId;
        }
    }
}