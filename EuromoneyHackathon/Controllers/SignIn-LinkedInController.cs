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
        private static string profileFieldsList = "first-name,last-name,email-address,public-profile-url,interests,skills,three-current-positions";

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


            return View();
        }

    }
}
