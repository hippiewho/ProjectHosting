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

//using System.IdentityModel.Claims;

namespace myWebsite.Controllers
{
    [Authorize]
    public class LoginController : Controller
    {
        private LoginContext db = new LoginContext();
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

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
        public ActionResult Login(LoginModel model, string retunUrl)
        {
          
            String UserName = model.UserName;
            String Password = model.Password;

            LoginContext LC = new LoginContext();
            LoginModel ValidUser = LC.UserList.Single(Person => Person.UserName == UserName && Person.Password == Password);


            if (ValidUser != null)
            {
                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Name, ValidUser.Name));
                
                var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
                System.Web.HttpContext.Current.GetOwinContext().Authentication.SignIn(identity);

                AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.Name;
                return Redirect("Index");
            }
            return View(model);
        }




        // GET: Login Index of users
        [AllowAnonymous]
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

        // GET: Login/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Login/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Email,Password,Name,UserName")] LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                db.UserList.Add(loginModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(loginModel);
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
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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
