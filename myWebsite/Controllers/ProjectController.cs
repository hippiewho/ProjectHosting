using myWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using myWebsite.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace myWebsite.Controllers
{
    public class ProjectsController : Controller
    {
        private ProjectContext PC = new ProjectContext();

        public ProjectsController()
        {

        }
        
        // GET: Projects/Project
        public async Task<ActionResult> Project(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                ProjectModel projectModel = PC.ProjectList.Where(modelItem =>modelItem.Id == id).FirstOrDefault();
                if (projectModel == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                } else
                {
                    LoginContext loginContext = new LoginContext();
                    LoginModel loginModel = loginContext.UserList.Where(loginItem => loginItem.ID == projectModel.UserId).FirstOrDefault();
                    if (loginModel != null)
                    {
                        HttpClient httpClient = new HttpClient();
                        httpClient.DefaultRequestHeaders.Add("User-Agent", "Franks Server");
                        string httprequeststring = "Https://api.github.com/repos/" + loginModel.UserName.Trim() + "/" + projectModel.Name.Trim() + "/commits";
                        var apiCallResponse = await httpClient.GetStringAsync(httprequeststring);
                        JsonObject json = new JsonObject(apiCallResponse);

                        loginContext.Dispose();
                        return View(json.commits);
                    }
                    else
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }

                }
                
            }

        }

        // GET: Project/Index
        [Authorize]
        public ActionResult Index()
        {
            return View(PC.ProjectList.ToList());
        }

        // GET: Projects/Edit
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                ProjectModel projectModel = PC.ProjectList.Find(id);
                if (projectModel == null)
                {
                    return HttpNotFound();
                }
                return View(projectModel);
            }
        }

        // POST: Projects/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "Id, Name, Description, Url, ImagePath, Position")] ProjectModel projectModel)
        {
            if (ModelState.IsValid)
            {
                PC.Entry(projectModel).State = System.Data.Entity.EntityState.Modified;
                PC.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(projectModel);
        }

        // GET: Projects/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Projects/Create
        [Authorize]
        [HttpPost]
        public ActionResult Create([Bind(Include = "Id, Name, Description, Url, ImagePath, Position, UserId")] ProjectModel projectModel)
        {
            if (ModelState.IsValid)
            {
                LoginContext loginContext = new LoginContext();
                LoginModel loginModel = loginContext.UserList.Where(loginItem => loginItem.UserName == projectModel.UserId);
                if (loginModel != null)
                {

                }
                PC.ProjectList.Add(projectModel);
                PC.SaveChanges();
            }
            return Redirect("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                PC.Dispose();
            }
            base.Dispose(disposing);
        }
    }

   
}