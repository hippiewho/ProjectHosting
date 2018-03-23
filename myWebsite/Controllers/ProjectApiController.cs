using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using myWebsite.Models;

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
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}