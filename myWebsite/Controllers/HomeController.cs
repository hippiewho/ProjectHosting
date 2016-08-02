using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace myWebsite.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
          //  Server.MapPath();
            //ViewBag.ListOfIMG = new List<String>(Directory.EnumerateDirectories)
            return View();
        }
    }
}