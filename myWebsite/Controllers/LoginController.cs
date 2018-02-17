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
        private LoginContext LC = new LoginContext();
        public LoginController(){}

        [AllowAnonymous]
        // GET: Login
        public ActionResult Login()
        {
            if (Request.IsAuthenticated)
            {
                return Redirect("Index");
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            string qString = Request.QueryString["ReturnUrl"];
            string redirectUrl = (qString == null ) ? "/Login/Index" : Request.QueryString["ReturnUrl"];

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
                    return Redirect(redirectUrl);
                  }
            }
            catch (Exception e)
            {
                var GenericErrorMessage = "Error Occured: " + e.ToString();
                Console.WriteLine(GenericErrorMessage);
            }
            ViewBag.IncorrectLogin = "Credentials Incorrect.";
            return View();
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
            return View(LC.UserList.ToList());
        }

        // GET: Login/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoginModel loginModel = LC.UserList.Find(id);
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
                    LC.UserList.Add(loginModel);
                    LC.SaveChanges();
                    loginModel.Password = unecryptedPassword;
                    if (Request.IsAuthenticated)
                    {
                        return Redirect("Index");
                    } else
                    {
                        return Login(loginModel);
                    }
                } else
                {
                    if (isUserNameInDatabase) ViewBag.UserNameError = "User Name already in use!";
                    if (isEmailInDatabase) ViewBag.EmailError = "Email already in use!";
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
            else
            {
                LoginModel loginModel = LC.UserList.Find(id);
                if (loginModel == null)
                {
                    return HttpNotFound();
                }
                return View(loginModel);
            }
        }

        // POST: Login/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Email,Password,Name,UserName")] LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                loginModel.Password = Crypto.HashPassword(loginModel.Password);
                LC.Entry(loginModel).State = System.Data.Entity.EntityState.Modified;
                LC.SaveChanges();
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
            LoginModel loginModel = LC.UserList.Find(id);
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
            LoginModel loginModel = LC.UserList.Find(id);
            LC.UserList.Remove(loginModel);
            LC.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                LC.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
