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
        // GET: Home
        public ActionResult Index()
        {
            
            List<string> dirs = new List<string>(Directory.EnumerateDirectories(Server.MapPath("~")));
            String[,] DTDirs = new String[3, 2];
            
            foreach (string dir in dirs)
            {
                if (DTDirs[0, 0] == null || Convert.ToDateTime(DTDirs[0, 1]) < Directory.GetLastWriteTime(dir))
                {
                    DTDirs[0, 0] = dir;
                    DTDirs[0, 1] = Directory.GetLastWriteTime(dir).ToString();
                }
                else if (DTDirs[1, 0] == null || Convert.ToDateTime(DTDirs[1, 1]) < Directory.GetLastWriteTime(dir))
                {
                    DTDirs[1, 0] = dir;
                    DTDirs[1, 1] = Directory.GetLastWriteTime(dir).ToString();
                }
                else if (DTDirs[2, 0] == null || Convert.ToDateTime(DTDirs[2, 1]) < Directory.GetLastWriteTime(dir))
                {
                    DTDirs[2, 0] = dir;
                    DTDirs[2, 1] = Directory.GetLastWriteTime(dir).ToString();
                }
            }

            ViewBag.dirs = DTDirs;
            return View();
        }
    }
}