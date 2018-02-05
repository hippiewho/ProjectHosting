using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using myWebsite.Models;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using Microsoft.AspNet.Identity.Owin;
using System.Web;
using System.Web.Helpers;

namespace myWebsite.Controllers
{
    [Authorize]
    public class LoginController : Controller
    {
        private LoginContext db = new LoginContext();
        public LoginController(){}

        [AllowAnonymous]
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            LoginContext LC = new LoginContext();
            try
            {
                LoginModel ValidUser = LC.UserList.Single(Person => Person.UserName == model.UserName);
                bool isValid = ValidUser != null;
                bool isVerifiedPassowrd = Crypto.VerifyHashedPassword(ValidUser.Password, model.Password);


                if (isValid && isVerifiedPassowrd)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, ValidUser.Name)
                    };
                    var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
                    System.Web.HttpContext.Current.GetOwinContext().Authentication.SignIn(identity);
                    LC.Dispose();
                    return Redirect("Index");
                  }
            }
            catch (Exception e)
            {
                var GenericErrorMessage = "Error Occured: " + e.ToString();
                Console.WriteLine(GenericErrorMessage);
            }
            return View(model);
        }

        // GET : Log off
        public ActionResult LogOff()
        {
            System.Web.HttpContext.Current.Request.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return Redirect("/");
        }


        // GET: Login Index of users
        [Authorize]
        public ActionResult Index()
        {
            return View(db.UserList.ToList());
        }

        // GET: Login/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoginModel loginModel = db.UserList.Find(id);
            if (loginModel == null)
            {
                return HttpNotFound();
            }
            return View(loginModel);
        }

        [AllowAnonymous]
        // GET: Login/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Login/Create
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Create([Bind(Include = "ID,Email,Password,Name,UserName")] LoginModel loginModel)
        {

            if (ModelState.IsValid)
            {
                LoginContext LC = new Models.LoginContext();
                var EmailCheck = LC.UserList
                             .Where(User => User.Email == loginModel.Email)
                             .FirstOrDefault();
                var UserNameCheck = LC.UserList
                             .Where(User => User.UserName == loginModel.UserName)
                             .FirstOrDefault();

                bool isEmailInDatabase = EmailCheck != null;
                bool isUserNameInDatabase = UserNameCheck != null;



                if (!isEmailInDatabase && !isUserNameInDatabase)
                {
                    String unecryptedPassword = loginModel.Password;
                    loginModel.Password = Crypto.HashPassword(loginModel.Password);
                    db.UserList.Add(loginModel);
                    db.SaveChanges();
                    loginModel.Password = unecryptedPassword;
                    return Login(loginModel);
                } else
                {
                    if (isEmailInDatabase) ViewBag.UserNameError = "User Name in use!";
                    if (isUserNameInDatabase) ViewBag.EmailError = "Email already in use!";
                }

            }

            return View();
        }

        // GET: Login/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoginModel loginModel = db.UserList.Find(id);
            if (loginModel == null)
            {
                return HttpNotFound();
            }
            return View(loginModel);
        }

        // POST: Login/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Email,Password,Name,UserName")] LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loginModel).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(loginModel);
        }

        // GET: Login/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoginModel loginModel = db.UserList.Find(id);
            if (loginModel == null)
            {
                return HttpNotFound();
            }
            return View(loginModel);
        }

        // POST: Login/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LoginModel loginModel = db.UserList.Find(id);
            db.UserList.Remove(loginModel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
