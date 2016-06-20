using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
namespace myWebsite.Controllers
{
    public class DownloadsController : Controller
    {
        // GET: Downloads
        public ActionResult Files()
        {
 
            ViewBag.Directories = new List<String>(Directory.EnumerateFiles("C:/Users/hippi/OneDrive/Documents/Visual Studio 2015/Projects/myWebsite/myWebsite/Content/Images"));
            return View();
        }
    }
}