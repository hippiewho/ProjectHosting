using myWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace myWebsite.Controllers
{
    public class ProjectsController : Controller
    {
        private ProjectContext PC = new ProjectContext();
        // GET: Project
        
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
                    HttpClient httpClient = new HttpClient();
                    httpClient.DefaultRequestHeaders.Add("User-Agent", "Franks Server");
                    var apiCallResponse = await httpClient.GetStringAsync("Https://api.github.com/repos/hippiewho/rccontroller/commits");
                    JsonObject json = new JsonObject(apiCallResponse);

                    return View();
                }
                
            }

        }

        // GET: Project/Index
        [Authorize]
        public ActionResult Index()
        {
            return View(PC.ProjectList.ToList());
        }
        // GET: Project/Edit
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
    }
}