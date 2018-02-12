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
using Microsoft.AspNet.Identity.EntityFramework;

namespace myWebsite.Controllers
{
    public class ProjectsController : Controller
    {
        private ProjectContext PC = new ProjectContext();
        private LoginContext LC = new LoginContext();

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
                    LoginModel loginModel = LC.UserList.Where(loginItem => loginItem.ID == projectModel.UserId).FirstOrDefault();
                    if (loginModel != null)
                    {
                        HttpClient httpClient = new HttpClient();
                        httpClient.DefaultRequestHeaders.Add("User-Agent", "Franks Server");
                        string httprequeststring = "Https://api.github.com/repos/" + loginModel.UserName.Trim() + "/" + projectModel.Name.Trim() + "/commits";
                        var apiCallResponse = await httpClient.GetStringAsync(httprequeststring);
                        JsonObject json = new JsonObject(apiCallResponse, projectModel.Name, projectModel.Description, projectModel.Url, projectModel.ImagePath);

                        return View(json);
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
        public ActionResult Edit(ProjectModel projectModel)
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
            ProjectHelperClass model = new ProjectHelperClass();
            ConfigureHelperClass(model);

            return View(model);
        }

        private void ConfigureHelperClass(ProjectHelperClass model)
        {
            model.UserSelectList = LC.UserList.Select(m => new SelectListItem
            {
                Value = m.ID.ToString(),
                Text = m.UserName
            });
        }

        // POST: Projects/Create
        [Authorize]
        [HttpPost]
        public ActionResult Create( ProjectHelperClass model )
        {
            if (ModelState.IsValid)
            {
                ProjectModel project = new ProjectModel
                {
                    Name = model.Name,
                    Description = model.Description,
                    Url = model.Url,
                    ImagePath = model.ImagePath,
                    Position = model.Position,
                    UserId = model.UserId
                };

                if (project != null)
                {
                    PC.ProjectList.Add(project);
                    PC.SaveChanges();
                }
                return Redirect("Index");
            } else
            {
                ConfigureHelperClass(model);
                return View(model);
            }
        }

        public ActionResult ProjectDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            } else
            {
                ProjectModel project = PC.ProjectList.Where(modelItem => modelItem.Id == id).FirstOrDefault();
                if (project == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                return View(project);
            }

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                PC.Dispose();
                LC.Dispose();
            }
            base.Dispose(disposing);
        }
    }

   
}