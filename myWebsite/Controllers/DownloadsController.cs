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
        [Authorize]
        // GET: Downloads
        public ActionResult Files(string FileName)
        {
            ViewBag.Files = new List<String>(Directory.EnumerateFiles(Server.MapPath("~/Content/Images/")));
            if ( FileName != null)
            {
                return Download(FileName);
            }
            return View();
        }
    
        public FileResult Download(String name)
        {
            string PathToFile = Server.MapPath("~/Content/Images/" + name );
            string MimeType = "image/" + name.Substring(name.Length - 3);
            FileStreamResult FSR = new FileStreamResult(new FileStream(PathToFile, FileMode.Open, FileAccess.Read), MimeType);
            FSR.FileDownloadName = name;
            return FSR;
        }
    }
}