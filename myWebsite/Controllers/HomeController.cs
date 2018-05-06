using myWebsite.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace myWebsite.Controllers
{
    public class HomeController : Controller
    {
        private ProjectContext PC = new ProjectContext();

        // GET: Home
        public ActionResult Index()
        {
            var queryList = PC.ProjectList.Where(entry => entry.Position == 1 || 
                                                          entry.Position == 2 || 
                                                          entry.Position == 3 ||
                                                          entry.Position == 4 ||
                                                          entry.Position == 5 ||
                                                          entry.Position == 6 )
                                          .ToList();

            return View(queryList);
        }
    }
}