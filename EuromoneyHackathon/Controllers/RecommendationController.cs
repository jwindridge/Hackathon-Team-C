using EuromoneyHackathon.External;
using EuromoneyHackathon.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Mvc;


namespace EuromoneyHackathon.Controllers
{
    public class RecommendationController : Controller
    {
        //
        // GET: /Attendance/
        [HttpGet]
        public ActionResult Index()
        {
            String pid = Request.QueryString["pid"];  // personId
            Response response = new Response();

            Event[] recommendedEvent = JsonConvert.DeserializeObject<Event[]>(MarkLogicLayer.getRecommendedEventFromPersonId(pid).ToString());

            JArray jsonPayload = JArray.FromObject(recommendedEvent);
            response.payload = jsonPayload;
            return View(jsonPayload);
        }
    }
}
