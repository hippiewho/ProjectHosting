using myWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace myWebsite.Controllers
{
    public class ProjectsController : Controller
    {
        private ProjectContext PC = new ProjectContext();
        // GET: Project
        
        public ActionResult Project(string id)
        {

            ViewBag.Title = "Projects";
            return View();
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
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
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