using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace myWebsite.Controllers
{
    public class ProjectsController : Controller
    {
        // GET: Project
        public ActionResult Project(string id)
        {

            ViewBag.Title = "Projects";
            return View();
        }
    }
}