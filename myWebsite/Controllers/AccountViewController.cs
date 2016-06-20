using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using myWebsite.Models;

namespace myWebsite.Controllers
{
    public class AccountViewController : Controller
    {
        // POST: AccountView

        public ActionResult AccountViewer(int empID)
        { 
            LoginContext AllLogins = new LoginContext();
            LoginModel User = AllLogins.UserList.Single(Person => Person.ID == empID);
            return View(User);
        }

        public ActionResult AccountChooser()
        {
            LoginContext AllLogins = new LoginContext();
            List<LoginModel> Users = AllLogins.UserList.ToList();
            return View(Users);
        }
    }
}