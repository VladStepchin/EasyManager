
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EasyManager;

namespace EasyManager.Controllers
{
    public class TasksController : Controller
    {
        private EasyManagerEntities db = new EasyManagerEntities();

        // GET: Tasks
        public ActionResult Index(int? id)
        {
            ViewBag.id = id;
            return View();
        }

        public ActionResult ListForUser()
        {
            return View();
        }

        // GET: Tasks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        // GET: Tasks/Create
        public ActionResult Create()
        {
            ViewBag.AsigneeID = new SelectList(db.People, "ID", "Name");
            ViewBag.ProjectID = new SelectList(db.Projects, "ID", "Summary");
            ViewBag.Priority = new SelectList(db.TaskPriorities, "ID", "Name");
            ViewBag.Status = new SelectList(db.TaskStatus, "ID", "Name");
            ViewBag.Type = new SelectList(db.TaskTypes, "ID", "Name");
            return View();
        }

        // POST: Tasks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ProjectID,Summary,Created,DueDate,Status,AsigneeID,Description,Logged,Estimated,Type,Priority")] Task task)
        {
            if (ModelState.IsValid)
            {
                db.Tasks.Add(task);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AsigneeID = new SelectList(db.People, "ID", "Name", task.AsigneeID);
            ViewBag.ProjectID = new SelectList(db.Projects, "ID", "Summary", task.ProjectID);
            ViewBag.Priority = new SelectList(db.TaskPriorities, "ID", "Name", task.Priority);
            ViewBag.Status = new SelectList(db.TaskStatus, "ID", "Name", task.Status);
            ViewBag.Type = new SelectList(db.TaskTypes, "ID", "Name", task.Type);
            return View(task);
        }

        // GET: Tasks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            ViewBag.AsigneeID = new SelectList(db.People, "ID", "Name", task.AsigneeID);
            ViewBag.ProjectID = new SelectList(db.Projects, "ID", "Summary", task.ProjectID);
            ViewBag.Priority = new SelectList(db.TaskPriorities, "ID", "Name", task.Priority);
            ViewBag.Status = new SelectList(db.TaskStatus, "ID", "Name", task.Status);
            ViewBag.Type = new SelectList(db.TaskTypes, "ID", "Name", task.Type);
            return View(task);
        }

        // POST: Tasks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ProjectID,Summary,Created,DueDate,Status,AsigneeID,Description,Logged,Estimated,Type,Priority")] Task task)
        {
            if (ModelState.IsValid)
            {
                db.Entry(task).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AsigneeID = new SelectList(db.People, "ID", "Name", task.AsigneeID);
            ViewBag.ProjectID = new SelectList(db.Projects, "ID", "Summary", task.ProjectID);
            ViewBag.Priority = new SelectList(db.TaskPriorities, "ID", "Name", task.Priority);
            ViewBag.Status = new SelectList(db.TaskStatus, "ID", "Name", task.Status);
            ViewBag.Type = new SelectList(db.TaskTypes, "ID", "Name", task.Type);
            return View(task);
        }

        // GET: Tasks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        // POST: Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Task task = db.Tasks.Find(id);
            db.Tasks.Remove(task);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
