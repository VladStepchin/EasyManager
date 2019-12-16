using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EasyManager.Controllers.ApiControllers
{
    public class ProjectStatusController : ApiController
    {

        private EasyManagerEntities db = new EasyManagerEntities();

        // GET: api/ProjectStatus

        public IQueryable<ProjectStatu> Get()
        {
            return db.ProjectStatus;
        }

        // GET: api/ProjectStatus/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/ProjectStatus
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/ProjectStatus/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ProjectStatus/5
        public void Delete(int id)
        {
        }
    }
}
