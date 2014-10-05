using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EuromoneyHackathon.Controllers
{
    public class LinkedInController : Controller
    {
        //
        // GET: /LinkedIn/

        public ActionResult Index()
        {
            byte[] randStr = new byte[32];
            new Random().NextBytes(randStr);
            String state = Convert.ToBase64String(randStr);
            RedirectResult result = Redirect(String.Format("https://www.linkedin.com/uas/oauth2/authorization?response_type=code&client_id={0}&scope={1}&state={2}&redirect_uri={3}",
            "75o0z7p1kzjgby",
            "r_fullprofile%20r_emailaddress%20r_network",
            state,
            "http://localhost:12067/signinlinkedin"));
            return result;
        }

    }
}
