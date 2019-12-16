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
    public class TimeLogsController : ApiController
    {
        private EasyManagerEntities db = new EasyManagerEntities();

        // GET: api/TimeLogs
        public IQueryable<TimeLog> GetTimeLogs()
        {
            return db.TimeLogs;
        }

        // GET: api/TimeLogs/5
        [ResponseType(typeof(TimeLog))]
        public IHttpActionResult GetTimeLog(int ID)
        {
            var user = db.People.Find(ID);

            IQueryable<TimeLog> timeLog = db.TimeLogs.Where( t=> t.Person == user.Login);

            if (timeLog == null)
            {
                return NotFound();
            }

            return Ok(timeLog);
        }

        // PUT: api/TimeLogs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTimeLog(int id, TimeLog timeLog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != timeLog.ID)
            {
                return BadRequest();
            }

            db.Entry(timeLog).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TimeLogExists(id))
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

        // POST: api/TimeLogs
        [ResponseType(typeof(TimeLog))]
        public IHttpActionResult PostTimeLog(TimeLog timeLog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TimeLogs.Add(timeLog);

            var taskToLog = db.Tasks.Where(t => t.ID == timeLog.TaskID).FirstOrDefault();
            taskToLog.Logged += timeLog.Time;

            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = timeLog.ID }, timeLog);
        }

        // DELETE: api/TimeLogs/5
        [ResponseType(typeof(TimeLog))]
        public IHttpActionResult DeleteTimeLog(int id)
        {
            TimeLog timeLog = db.TimeLogs.Find(id);
            if (timeLog == null)
            {
                return NotFound();
            }

            db.TimeLogs.Remove(timeLog);
            
            db.SaveChanges();

            return Ok(timeLog);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TimeLogExists(int id)
        {
            return db.TimeLogs.Count(e => e.ID == id) > 0;
        }
    }
}