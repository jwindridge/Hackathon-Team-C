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
        public string LinkedInUrl { get; set; }
        public string EmailAddress { get; set; }
        public string[] Interests { get; set; }
        public string[] CompanyName { get; set; }
        public string[] CompanyTitle { get; set; }
        public string Type = "Person";

        public string LinkedInAccessCode { get; set; }

        /// <summary>
        /// Create person, specifying id, first name & last name
        /// </summary>
        /// <param name="personId">id of person</param>
        /// <param name="firstName">Person's first name</param>
        /// <param name="lastName">Person's last name</param>
        public Person(string personId, string firstName, string lastName,string linkedInUrl, string emailAddress)
        {
            this.Id = personId;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.LinkedInUrl = linkedInUrl;
            this.EmailAddress = emailAddress;
            this.Interests = new string[] { };
        }
        /// <summary>
        /// Create person, generating id at random, but specifying name attributes
        /// </summary>
        /// <param name="firstName">Person's first name</param>
        /// <param name="lastName">Person's last name</param>
        public Person(string firstName, string lastName,string linkedInUrl,string emailAddress)
        {
            this.Id = Guid.NewGuid().ToString().Replace("{", "").Replace("}", "").Replace("-", "");
            this.FirstName = firstName;
            this.LastName = lastName;
            this.LinkedInUrl = linkedInUrl;
            this.EmailAddress = emailAddress;
            this.Interests = new string[] { };
        }

        public Person()
        {
            this.Id = Guid.NewGuid().ToString().Replace("{", "").Replace("}", "").Replace("-", "");
            this.Interests = new string[] { };
            this.CompanyName = new string[] { };
            this.CompanyTitle = new string[] { };
        }
    }
}