using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web;
using System.Web.Configuration;
using System.Net.Http;
using System.Net;

namespace myWebsite.Globals
{
    public static class GlobalVariables
    {
        public static string ImageFolderPath { get; } = "/Content/Images/";

        public static int PasswordMinLength { get; } = 2;

        public static List<string> GetImagePathList(HttpServerUtilityBase server)
        {
            return new List<string>(Directory.EnumerateFiles(server.MapPath(ImageFolderPath)));
        }
    }




}