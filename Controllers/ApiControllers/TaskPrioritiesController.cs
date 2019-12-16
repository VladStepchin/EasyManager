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
using EasyManager;

namespace EasyManager.Controllers.ApiControllers
{
    public class TaskPrioritiesController : ApiController
    {
        private EasyManagerEntities db = new EasyManagerEntities();

        // GET: api/TaskPriorities
        public IQueryable<TaskPriority> GetTaskPriorities()
        {
            return db.TaskPriorities;
        }

        // GET: api/TaskPriorities/5
        [ResponseType(typeof(TaskPriority))]
        public IHttpActionResult GetTaskPriority(int id)
        {
            TaskPriority taskPriority = db.TaskPriorities.Find(id);
            if (taskPriority == null)
            {
                return NotFound();
            }

            return Ok(taskPriority);
        }

        // PUT: api/TaskPriorities/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTaskPriority(int id, TaskPriority taskPriority)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != taskPriority.ID)
            {
                return BadRequest();
            }

            db.Entry(taskPriority).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskPriorityExists(id))
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

        // POST: api/TaskPriorities
        [ResponseType(typeof(TaskPriority))]
        public IHttpActionResult PostTaskPriority(TaskPriority taskPriority)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TaskPriorities.Add(taskPriority);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = taskPriority.ID }, taskPriority);
        }

        // DELETE: api/TaskPriorities/5
        [ResponseType(typeof(TaskPriority))]
        public IHttpActionResult DeleteTaskPriority(int id)
        {
            TaskPriority taskPriority = db.TaskPriorities.Find(id);
            if (taskPriority == null)
            {
                return NotFound();
            }

            db.TaskPriorities.Remove(taskPriority);
            db.SaveChanges();

            return Ok(taskPriority);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TaskPriorityExists(int id)
        {
            return db.TaskPriorities.Count(e => e.ID == id) > 0;
        }
    }
}