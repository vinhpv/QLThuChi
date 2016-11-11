using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestSharp;

namespace QLThuChi.API.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        [HttpPost]
        public ActionResult Index(string user, string pass)
        {
            ViewBag.Title = "Home Page";

            var client = new RestClient(HttpContext.Request.Url.AbsoluteUri.Replace(HttpContext.Request.Url.AbsolutePath,"") + "/OAuth/token");
            var request = new RestRequest(Method.POST);
            //request.AddHeader("postman-token", "135fe896-390d-b37d-c811-b9dd4aedc02c");
            //request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", "UserName=sysadmin&Password=Tinhyeu#318&grant_type=password", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return View();
        }

    }
}
