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

        /// <summary>
        /// Create person, specifying id, first name & last name
        /// </summary>
        /// <param name="personId">id of person</param>
        /// <param name="firstName">Person's first name</param>
        /// <param name="lastName">Person's last name</param>
        public Person(string personId, string firstName, string lastName)
        {
            this.personId = personId;
            this.personFirstName = firstName;
            this.personLastName = lastName;
        }
        /// <summary>
        /// Create person, generating id at random, but specifying name attributes
        /// </summary>
        /// <param name="firstName">Person's first name</param>
        /// <param name="lastName">Person's last name</param>
        public Person(string firstName, string lastName)
        {
            this.personId = Guid.NewGuid().ToString().Replace("{", "").Replace("}", "").Replace("-", "");
            this.personFirstName = firstName;
            this.personLastName = lastName;
        }

        public Person()
        {
            this.personId = Guid.NewGuid().ToString().Replace("{", "").Replace("}", "").Replace("-", "");
        }
    }
}