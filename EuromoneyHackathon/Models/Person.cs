using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EuromoneyHackathon.Models
{
    public class Person
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        /// <summary>
        /// Create person, specifying id, first name & last name
        /// </summary>
        /// <param name="personId">id of person</param>
        /// <param name="firstName">Person's first name</param>
        /// <param name="lastName">Person's last name</param>
        public Person(string personId, string firstName, string lastName)
        {
            this.Id = personId;
            this.FirstName = firstName;
            this.LastName = lastName;
        }
        /// <summary>
        /// Create person, generating id at random, but specifying name attributes
        /// </summary>
        /// <param name="firstName">Person's first name</param>
        /// <param name="lastName">Person's last name</param>
        public Person(string firstName, string lastName)
        {
            this.Id = Guid.NewGuid().ToString().Replace("{", "").Replace("}", "").Replace("-", "");
            this.FirstName = firstName;
            this.LastName = lastName;
        }

        public Person()
        {
            this.Id = Guid.NewGuid().ToString().Replace("{", "").Replace("}", "").Replace("-", "");
        }
    }
}