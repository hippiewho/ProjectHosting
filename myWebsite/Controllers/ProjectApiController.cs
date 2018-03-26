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

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
            throw new NotImplementedException();
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
            throw new NotImplementedException();

        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}