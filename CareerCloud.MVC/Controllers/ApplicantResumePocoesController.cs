using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;

namespace CareerCloud.MVC.Controllers
{
    public class ApplicantResumePocoesController : Controller
    {
        private CareerCloudContext db = new CareerCloudContext();

        // GET: ApplicantResumePocoes
        public ActionResult Index()
        {
            var applicantResumes = db.ApplicantResumes.Include(a => a.ApplicantProfiles);
            return View(applicantResumes.ToList());
        }

        // GET: ApplicantResumePocoes/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicantResumePoco applicantResumePoco = db.ApplicantResumes.Find(id);
            if (applicantResumePoco == null)
            {
                return HttpNotFound();
            }
            return View(applicantResumePoco);
        }

        // GET: ApplicantResumePocoes/Create
        public ActionResult Create()
        {
			//ViewBag.Applicant = new SelectList(db.ApplicantProfiles, "Id", "Currency");
			ViewBag.Applicant = new SelectList(db.SecurityLogins, "Id", "FullName");
			return View();
        }

        // POST: ApplicantResumePocoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Applicant,Resume,LastUpdated")] ApplicantResumePoco applicantResumePoco)
        {
            if (ModelState.IsValid)
            {
                applicantResumePoco.Id = Guid.NewGuid();
				applicantResumePoco.ApplicantProfiles = db.ApplicantProfiles.Where(t => t.Login == applicantResumePoco.Applicant).FirstOrDefault();
				db.ApplicantResumes.Add(applicantResumePoco);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Applicant = new SelectList(db.SecurityLogins, "Id", "FullName", applicantResumePoco.Applicant);
            return View(applicantResumePoco);
        }

        // GET: ApplicantResumePocoes/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicantResumePoco applicantResumePoco = db.ApplicantResumes.Find(id);
            if (applicantResumePoco == null)
            {
                return HttpNotFound();
            }
            ViewBag.Applicant = new SelectList(db.SecurityLogins, "Id", "FullName", applicantResumePoco.Applicant);
            return View(applicantResumePoco);
        }

        // POST: ApplicantResumePocoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Applicant,Resume,LastUpdated")] ApplicantResumePoco applicantResumePoco)
        {
            if (ModelState.IsValid)
            {
				
				db.Entry(applicantResumePoco).State = EntityState.Modified;
				applicantResumePoco.ApplicantProfiles = db.ApplicantProfiles.Where(t => t.Login == applicantResumePoco.Applicant).FirstOrDefault();
				db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Applicant = new SelectList(db.SecurityLogins, "Id", "FullName", applicantResumePoco.Applicant);
            return View(applicantResumePoco);
        }

        // GET: ApplicantResumePocoes/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicantResumePoco applicantResumePoco = db.ApplicantResumes.Find(id);
            if (applicantResumePoco == null)
            {
                return HttpNotFound();
            }
            return View(applicantResumePoco);
        }

        // POST: ApplicantResumePocoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            ApplicantResumePoco applicantResumePoco = db.ApplicantResumes.Find(id);
            db.ApplicantResumes.Remove(applicantResumePoco);
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
