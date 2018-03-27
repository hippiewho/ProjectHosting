using myWebsite.Models;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

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

        // GET api/ProjectApiController/5
        public ProjectModel Get(int id)
        {
            return projectContext.ProjectList.FirstOrDefault(x => x.Id == id);
        }

        // POST api/ProjectApiController
        public void Post(ProjectModel value)
        {
            if (ModelState.IsValid)
            {
                projectContext.ProjectList.Add(value);
                projectContext.SaveChanges();
            }
        }

        // May not implement due to SQL auto increment
        // PUT api/ProjectApiController/5
        public void Put(int id, [FromBody]string value)
        {
            throw new NotImplementedException();

        }

        // DELETE api/ProjectApiController/5
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}