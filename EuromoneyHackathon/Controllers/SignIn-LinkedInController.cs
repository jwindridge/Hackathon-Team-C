using EuromoneyHackathon.External;
using EuromoneyHackathon.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace EuromoneyHackathon.Controllers
{
    public class SignInLinkedInController : Controller
    {
        private static string linkedInUrl = "https://www.linkedin.com/uas/oauth2/accessToken"
            + "?grant_type=authorization_code&code={0}&redirect_uri={1}&client_id={2}&client_secret={3}";
        private static string linkedInProfileURL = "https://api.linkedin.com/v1/people/~:({0})?oauth2_access_token={1}";
        private static string profileFieldsList = "first-name,last-name,email-address,public-profile-url,interests,skills,positions";

        //
        // GET: /SignIn-LinkedIn/

        public ActionResult Index()
        {
            String code = Request.QueryString["code"];
            String state = Request.QueryString["state"];

            String authRequestUrl = String.Format(linkedInUrl, code, "http://localhost:12067/signinlinkedin", "75o0z7p1kzjgby", "12II1nRBNvbTb00l");

            HttpWebRequest authRequest = (HttpWebRequest)HttpWebRequest.Create(authRequestUrl);
          
            authRequest.Method = "POST";
            var httpAuthResponse = (HttpWebResponse)authRequest.GetResponse();
            JObject authResultObject;
            using (var streamReader = new StreamReader(httpAuthResponse.GetResponseStream()))
            {
                var authResult = streamReader.ReadToEnd();
                authResultObject = JObject.Parse(authResult);
            }

            String accessToken = authResultObject.GetValue("access_token").ToString();


            String profileRequestUrl = String.Format(linkedInProfileURL, profileFieldsList,accessToken);
            HttpWebRequest profileRequest = (HttpWebRequest)HttpWebRequest.Create(profileRequestUrl);
            profileRequest.Method = "GET";
            profileRequest.Headers.Add("x-li-format: json");
           
            var httpProfileResponse = (HttpWebResponse)profileRequest.GetResponse();
            JObject profileResultObject;
            using (var streamReader = new StreamReader(httpProfileResponse.GetResponseStream()))
            {
                var profileResult = streamReader.ReadToEnd();
                profileResultObject = JObject.Parse(profileResult);
            }
            //TODO: Extract company and interests from profileResultObject
            JArray positions = (JArray)profileResultObject["positions"]["values"];
            JArray currPosArray = JArray.FromObject(positions.Where(x => (bool)JObject.FromObject(x).GetValue("isCurrent") == true));
            if (currPosArray.Count != 0) {
                queryWiki(currPosArray[0]["company"]["name"].ToString());
            }

            JArray skills = (JArray)profileResultObject["skills"]["values"];
            string[] interests = new string[] { };
            if (profileResultObject["interests"] != null)
            {
                interests = profileResultObject["interests"].ToString().Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            }
            
            string[] companyName = currPosArray.Select(x => x["company"]["name"].ToString()).ToArray();
            string[] companyPosn = currPosArray.Select(x => x["title"].ToString()).ToArray();
            string[] skillsArr = skills.Select(x => x["skill"]["name"].ToString()).ToArray();

            List<string> interestList = interests.ToList();
            interestList.AddRange(skillsArr);
            interests = interestList.ToArray();


            Person markLogicPerson = new Person(); //MarkLogicLayer.getPersonMLByEmail(profileResultObject.GetValue("emailAddress").ToString());
            markLogicPerson.FirstName = profileResultObject["firstName"].ToString();
            markLogicPerson.LastName = profileResultObject["lastName"].ToString();
            markLogicPerson.EmailAddress = profileResultObject["emailAddress"].ToString();
            markLogicPerson.LinkedInUrl = profileResultObject["publicProfileUrl"].ToString();
            markLogicPerson.CompanyName = companyName;
            markLogicPerson.CompanyTitle = companyPosn;
            markLogicPerson.Interests = interests;
            markLogicPerson.LinkedInAccessCode = accessToken;

            Person existMLPerson = MarkLogicLayer.getPersonMLByEmail(markLogicPerson.EmailAddress);
            if (existMLPerson != null)
            {
                markLogicPerson.Id = existMLPerson.Id;
            }

            MarkLogicLayer.putPerson(markLogicPerson);
            return View(JObject.FromObject(markLogicPerson));
            //RedirectToAction();
        }

        public JObject queryWiki(string companyName)
        {
            companyName = HttpUtility.UrlEncode(companyName);
            String wikiBaseUrl = "http://en.wikipedia.org/w/api.php?action=query&titles={0}&prop=revisions&rvprop=content&rvsection=0&format=json";

            String queryWikiUrl = String.Format(wikiBaseUrl, companyName);
            HttpWebRequest queryWikiRequest = (HttpWebRequest) HttpWebRequest.Create(queryWikiUrl);
            queryWikiRequest.Method = "GET";
            queryWikiRequest.Headers.Add("x-li-format: json");

            var companyWikiResponse = (HttpWebResponse) queryWikiRequest.GetResponse();
            JObject companyIntroObject;
            using (var streamReader = new StreamReader(companyWikiResponse.GetResponseStream())){
                var wikiResult = streamReader.ReadToEnd();
                companyIntroObject = JObject.Parse(wikiResult);
            }
            return companyIntroObject;
        }

    }
}
