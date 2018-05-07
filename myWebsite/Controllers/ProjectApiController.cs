using myWebsite.Models;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using myWebsite.Globals;
using System.Threading.Tasks;

namespace myWebsite.Controllers
{
    public class ProjectApiController : ApiController
    {
        private ProjectContext projectContext { get; } = new ProjectContext();

        // GET api/ProjectApiController
        public IEnumerable<ProjectModel> Get()
        {
            return projectContext.ProjectList.AsEnumerable();
        }

        // GET api/ProjectApiController/GetImageUserInfo
        [Route("api/ProjectApiController/GetImageUserInfo")]
        public Dictionary<string, Dictionary<string, string>> GetImagePathAndUserId()
        {
            Dictionary<string, Dictionary<string, string>> dict = new Dictionary<string, Dictionary<string, string>>();
            LoginContext loginContext = new LoginContext();

            const string userIds = "UserIds";
            const string imagePaths = "ImagePaths";

            AddUsersImagePaths(dict, userIds, imagePaths, loginContext);

            return dict;
        }

        private static void AddUsersImagePaths(Dictionary<string, Dictionary<string, string>> dict, string userIds, string imagePaths, LoginContext loginContext)
        {
            dict.Add(userIds, new Dictionary<string, string>());
            dict.Add(imagePaths, new Dictionary<string, string>());

            foreach (var user in loginContext.UserList)
            {
                dict[userIds].Add(user.Name, user.ID.ToString());
            }

            foreach (var currentPath in GlobalVariables.GetImagePathList(new HttpServerUtilityWrapper(HttpContext.Current.Server)))
            {
                dict[imagePaths].Add(currentPath, currentPath);
            }
        }

        // GET api/ProjectApiController/5
        public ProjectModel Get(int id)
        {
            return projectContext.ProjectList.FirstOrDefault(x => x.Id == id);
        }

        // POST api/ProjectApiController
        public async Task<bool> Post(ProjectModel value)
        {
            if (!ModelState.IsValid) return false;

            projectContext.ProjectList.Add(value);
            await projectContext.SaveChangesAsync();
            return true;
        }

        // PUT api/ProjectApiController/5
        public async Task<bool> PutAsync(int id, ProjectModel value)
        {
            ProjectModel project;
            if ((project = projectContext.ProjectList.Find(id)) == null || !ModelState.IsValid) return false;

            SetProjectModelProperties(project, value);

            projectContext.Entry(project).State = System.Data.Entity.EntityState.Modified;
            await projectContext.SaveChangesAsync();

            return true;
        }

        private void SetProjectModelProperties(ProjectModel project, ProjectModel editedProjectModel)
        {
            project.ImagePath = editedProjectModel.ImagePath;
            project.Name = editedProjectModel.Name;
            project.Description = editedProjectModel.Description;
            project.Position = editedProjectModel.Position;
            project.GitHubUrl = editedProjectModel.GitHubUrl;
            project.SiteUrl = editedProjectModel.SiteUrl;
            project.OtherUrl = editedProjectModel.OtherUrl;
            project.UserId = editedProjectModel.UserId;
        }

        // DELETE api/ProjectApiController/5
        public async Task<bool> DeleteAsync(int id)
        {
            ProjectModel project = projectContext.ProjectList.Find(id);

            if (project == null) return false;

            projectContext.ProjectList.Remove(project);
            await projectContext.SaveChangesAsync();
            return true;
        }
    }
}