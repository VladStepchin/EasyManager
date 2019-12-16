using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

using System.Web.Security;

namespace EasyManager.Controllers.ApiControllers
{
    public class ProjectsController : ApiController
    {
        private EasyManagerEntities db = new EasyManagerEntities();

        // GET: api/Projects

        [Authorize(Roles="Manager")]
        public IQueryable<Project> GetProjects()
        {
            return db.Projects;
        }

        // GET: api/Projects/5
        [Authorize(Roles = "Manager")]
        [ResponseType(typeof(Project))]
        public IHttpActionResult GetProject(int id)
        {
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return NotFound();
            }

            return Ok(project);
        }

        // PUT: api/Projects/5
        [Authorize(Roles = "Manager")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProject(int id, Project project)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != project.ID)
            {
                return BadRequest();
            }

            db.Entry(project).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Projects
        [Authorize(Roles = "Manager")]
        [ResponseType(typeof(Project))]
        public IHttpActionResult PostProject(Project project)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            project.Logged = 0;
            db.Projects.Add(project);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = project.ID }, project);
        }

        // DELETE: api/Projects/5
        [Authorize(Roles = "Manager")]
        [ResponseType(typeof(Project))]
        public IQueryable<Project> DeleteProject(int id)
        {
            Project project = db.Projects.Find(id);

            if (project == null)
            {
                var res1 = db.Projects.Where(u => u.Owner == User.Identity.Name);

                return res1;
            }
            var res2 = db.Projects.Where(u => u.Owner == User.Identity.Name);

            db.Projects.Remove(project);
            db.SaveChanges();

            return res2;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProjectExists(int id)
        {
            return db.Projects.Count(e => e.ID == id) > 0;
        }
    }
}