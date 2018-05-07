﻿using myWebsite.Globals;
using myWebsite.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace myWebsite.Controllers
{
    public class ProjectsController : Controller
    {
        private ProjectContext PC = new ProjectContext();
        private LoginContext LC = new LoginContext();

        // GET: Projects/Project
        public async Task<ActionResult> Project(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            ProjectModel projectModel = PC.ProjectList.FirstOrDefault(modelItem => modelItem.Id == id);
            if (projectModel == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            LoginModel loginModel = LC.UserList.FirstOrDefault(loginItem => loginItem.ID == projectModel.UserId);
            if (loginModel == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);


            ProjectJsonObject projectJson = new ProjectJsonObject(projectModel.Name.Trim(), projectModel.ImagePath, projectModel.GitHubUrl, projectModel.SiteUrl, projectModel.OtherUrl, id);

            return View(projectJson);

        }

        public async Task<String> GetRawGitCommits(int? id)
        {
            if (id == null) return null;

            ProjectModel projectModel = PC.ProjectList.FirstOrDefault(modelItem => modelItem.Id == id);
            if (projectModel == null) return GetCommitsToString(new ProjectJsonObject().Commits);

            LoginModel loginModel = LC.UserList.FirstOrDefault(loginItem => loginItem.ID == projectModel.UserId);
            if (loginModel == null) return GetCommitsToString(new ProjectJsonObject().Commits);

            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("User-Agent", "Franks Server");
            string httprequeststring = "Https://api.github.com/repos/" + loginModel.UserName.Trim() + "/" + projectModel.Name.Trim() + "/commits";

            return await httpClient.GetStringAsync(httprequeststring);

        }

        public async Task<String> GetGitCommits(int? id)
        {
            if (id == null) return null;

            ProjectModel projectModel = PC.ProjectList.FirstOrDefault(modelItem => modelItem.Id == id);
            if (projectModel == null) return GetCommitsToString(new ProjectJsonObject().Commits);

            LoginModel loginModel = LC.UserList.FirstOrDefault(loginItem => loginItem.ID == projectModel.UserId);
            if (loginModel == null) return GetCommitsToString(new ProjectJsonObject().Commits);

            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("User-Agent", "Franks Server");
            string httprequeststring = "Https://api.github.com/repos/" + loginModel.UserName.Trim() + "/" + projectModel.Name.Trim() + "/commits";
            ProjectJsonObject projectJson;
            try
            {
                var apiCallResponse = await httpClient.GetStringAsync(httprequeststring);
                projectJson = new ProjectJsonObject(apiCallResponse, projectModel.Name, projectModel.Description, projectModel.GitHubUrl, projectModel.SiteUrl, projectModel.OtherUrl, projectModel.ImagePath, projectModel.Id);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());
                projectJson = new ProjectJsonObject(projectModel.Name.Trim(), projectModel.ImagePath, projectModel.GitHubUrl, projectModel.SiteUrl, projectModel.OtherUrl, id);
            }
            return GetCommitsToString(projectJson.Commits);
        }

        public string GetCommitsToString(IEnumerable<CommitObject> commits)
        {
            if (!commits.Any()) return "No commits or Error Contacting Server";
            StringBuilder returnObject = new StringBuilder();

            foreach (var commit in commits)
            {
                returnObject.Append("<li>Author:");
                returnObject.Append(commit.Author);
                returnObject.Append("</li>");
                returnObject.Append("<ul>");
                returnObject.Append("<li class=\"CommitArea\">Commit SHA:");
                returnObject.Append(commit.Sha);
                returnObject.Append("</li>");
                returnObject.Append("<li>Commit Date: ");
                returnObject.Append(commit.Date);
                returnObject.Append("</li>");
                returnObject.Append("<li>Commit Message: ");
                returnObject.Append(commit.Message);
                returnObject.Append("</li>");
                returnObject.Append("</ul>");
                returnObject.Append("<br/>");
                returnObject.Append("\n");
            }
            return returnObject.ToString();
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
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            ProjectModel projectModel = PC.ProjectList.Find(id);
            if (projectModel == null) return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            ProjectHelperClass model = new ProjectHelperClass()
            {
                Name = projectModel.Name,
                Description = projectModel.Description,
                GitHubUrl = projectModel.GitHubUrl,
                SiteUrl = projectModel.SiteUrl,
                OtherUrl = projectModel.OtherUrl,
                ImagePath = projectModel.ImagePath,
                Position = projectModel.Position,
                UserId = projectModel.UserId
            };
            ConfigureHelperClass(model);
            
            return View(model);
        }

        // POST: Projects/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit(ProjectHelperClass projectHelperModel)
        {
            if (!ModelState.IsValid) return View(projectHelperModel);

            ProjectModel project = new ProjectModel()
            {
                Id = projectHelperModel.Id,
                Description = projectHelperModel.Description,
                GitHubUrl = projectHelperModel.GitHubUrl,
                ImagePath = projectHelperModel.ImagePath,
                Name = projectHelperModel.Name,
                OtherUrl = projectHelperModel.OtherUrl,
                Position = projectHelperModel.Position,
                SiteUrl = projectHelperModel.SiteUrl,
                UserId = projectHelperModel.UserId
            };

            PC.Entry(project).State = System.Data.Entity.EntityState.Modified;
            PC.SaveChanges();
            return RedirectToAction("Index");
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

            List<string> imagePathList = GlobalVariables.GetImagePathList(Server);

            model.ImagePathSelectList = imagePathList.Select(m => new SelectListItem
            {
                Value = GlobalVariables.ImageFolderPath + m.Substring(m.LastIndexOf("\\", StringComparison.Ordinal) + 1),
                Text = m.Substring(m.LastIndexOf("\\", StringComparison.Ordinal) + 1)
            });
        }

        // POST: Projects/Create
        [Authorize]
        [HttpPost]
        public ActionResult Create(ProjectHelperClass model)
        {
            if (ModelState.IsValid)
            {
                ProjectModel project = new ProjectModel
                {
                    Name = model.Name,
                    Description = model.Description,
                    GitHubUrl = model.GitHubUrl,
                    SiteUrl = model.SiteUrl,
                    OtherUrl = model.OtherUrl,
                    ImagePath = model.ImagePath,
                    Position = model.Position,
                    UserId = model.UserId
                };

                PC.ProjectList.Add(project);
                PC.SaveChanges();

                return Redirect("Index");
            }
            else
            {
                ConfigureHelperClass(model);
                return View(model);
            }
        }

        // GET: Projects/ProjectDetails/id
        public ActionResult ProjectDetails(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            ProjectModel project = PC.ProjectList.FirstOrDefault(modelItem => modelItem.Id == id);
            if (project == null)return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            return View(project);
            
        }

        // GET: Projects/UploadImage
        [Authorize]
        public ActionResult UploadImage()
        {
            Configuration config = WebConfigurationManager.OpenWebConfiguration("~");
            if (config.GetSection("system.web/httpRuntime") is HttpRuntimeSection section)
            {
                double maxFileSize = Math.Round(section.MaxRequestLength / 1024.0, 1);
                ViewBag.FileSizeLimit = $"Make sure your file is under {maxFileSize:0.#} MB.";
            }

            return View();
        }

        // POST: Projects/UploadImage
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public string UploadImage(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0 && file.ContentLength < 20970000 && file.ContentType.Contains("image"))
            {
                String filename = Path.GetFileName(file.FileName);
                if (filename != null)
                {
                    String folderPath = Path.Combine(Server.MapPath("~/Content/Images"), filename);
                    file.SaveAs(folderPath);
                }

                return "True";
            }
            return "Something Went Horribly Wrong!! or image is too large..";

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