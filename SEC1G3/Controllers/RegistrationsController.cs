﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SEC1G3.Models;

namespace SEC1G3.Controllers
{
    public class RegistrationsController : Controller
    {
        private Entities db = new Entities();

        // GET: Registrations
        public ActionResult Index()
        {
            var registrations = db.Registrations.Include(r => r.Cours).Include(r => r.Student).Include(r => r.StudyTerm);
            return View(registrations.ToList());
        }

        // GET: Registrations/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Registration registration = db.Registrations.Find(id);
            if (registration == null)
            {
                return HttpNotFound();
            }
            return View(registration);
        }

        // GET: Registrations/Create
        public ActionResult Create()
        {
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName");
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "StudentFirstName");
            ViewBag.TermID = new SelectList(db.StudyTerms, "TermID", "TermName");
            return View();
        }

        // POST: Registrations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StudentID,CourseID,TermID")] Registration registration)
        {
            if (ModelState.IsValid)
            {
                db.Registrations.Add(registration);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName", registration.CourseID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "StudentFirstName", registration.StudentID);
            ViewBag.TermID = new SelectList(db.StudyTerms, "TermID", "TermName", registration.TermID);
            return View(registration);
        }

        // GET: Registrations/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Registration registration = db.Registrations.Find(id);
            if (registration == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName", registration.CourseID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "StudentFirstName", registration.StudentID);
            ViewBag.TermID = new SelectList(db.StudyTerms, "TermID", "TermName", registration.TermID);
            return View(registration);
        }

        // POST: Registrations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentID,CourseID,TermID")] Registration registration)
        {
            if (ModelState.IsValid)
            {
                db.Entry(registration).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName", registration.CourseID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "StudentFirstName", registration.StudentID);
            ViewBag.TermID = new SelectList(db.StudyTerms, "TermID", "TermName", registration.TermID);
            return View(registration);
        }

        // GET: Registrations/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Registration registration = db.Registrations.Find(id);
            if (registration == null)
            {
                return HttpNotFound();
            }
            return View(registration);
        }

        // POST: Registrations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Registration registration = db.Registrations.Find(id);
            db.Registrations.Remove(registration);
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
