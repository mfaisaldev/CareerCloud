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
    public class ApplicantJobApplicationPocoesController : Controller
    {
        private CareerCloudContext db = new CareerCloudContext();

        // GET: ApplicantJobApplicationPocoes
        public ActionResult Index()
        {
			var applicantJobApplications = db.ApplicantJobApplications.Include(a => a.ApplicantProfiles).Include(a => a.CompanyJobs).Include(a=>a.CompanyJobs.CompanyJobsDescriptions);
            return View(applicantJobApplications.ToList());
        }

        // GET: ApplicantJobApplicationPocoes/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicantJobApplicationPoco applicantJobApplicationPoco = db.ApplicantJobApplications.Find(id);
            if (applicantJobApplicationPoco == null)
            {
                return HttpNotFound();
            }
            return View(applicantJobApplicationPoco);
        }

        // GET: ApplicantJobApplicationPocoes/Create
        public ActionResult Create()
        {
            ViewBag.Applicant = new SelectList(db.SecurityLogins, "Id", "Fullname");
            ViewBag.Job = new SelectList(db.CompanyJobsDescriptions, "Job", "JobName");
            return View();
        }

        // POST: ApplicantJobApplicationPocoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Applicant,Job,ApplicationDate")] ApplicantJobApplicationPoco applicantJobApplicationPoco)
        {
            if (ModelState.IsValid)
            {
                applicantJobApplicationPoco.Id = Guid.NewGuid();
				applicantJobApplicationPoco.ApplicantProfiles = db.ApplicantProfiles.Where(t => t.Login == applicantJobApplicationPoco.Applicant).FirstOrDefault();
				applicantJobApplicationPoco.CompanyJobs = db.CompanyJobs.Where(t => t.Id == applicantJobApplicationPoco.Job).FirstOrDefault();
				db.ApplicantJobApplications.Add(applicantJobApplicationPoco);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Applicant = new SelectList(db.SecurityLogins, "Id", "FullName", applicantJobApplicationPoco.Applicant);
            ViewBag.Job = new SelectList(db.CompanyJobsDescriptions, "Job", "JobName", applicantJobApplicationPoco.Job);
            return View(applicantJobApplicationPoco);
        }

        // GET: ApplicantJobApplicationPocoes/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicantJobApplicationPoco applicantJobApplicationPoco = db.ApplicantJobApplications.Find(id);
            if (applicantJobApplicationPoco == null)
            {
                return HttpNotFound();
            }
            ViewBag.Applicant = new SelectList(db.SecurityLogins, "Id", "FullName", applicantJobApplicationPoco.Applicant);
            ViewBag.Job = new SelectList(db.CompanyJobsDescriptions, "Job", "JobName", applicantJobApplicationPoco.Job);
            return View(applicantJobApplicationPoco);
        }

        // POST: ApplicantJobApplicationPocoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Applicant,Job,ApplicationDate")] ApplicantJobApplicationPoco applicantJobApplicationPoco)
        {
            if (ModelState.IsValid)
            {
                db.Entry(applicantJobApplicationPoco).State = EntityState.Modified;
				applicantJobApplicationPoco.ApplicantProfiles = db.ApplicantProfiles.Where(t => t.Login == applicantJobApplicationPoco.Applicant).FirstOrDefault();
				applicantJobApplicationPoco.CompanyJobs = db.CompanyJobs.Where(t => t.Id == applicantJobApplicationPoco.Job).FirstOrDefault();
				db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Applicant = new SelectList(db.SecurityLogins, "Id", "FullName", applicantJobApplicationPoco.Applicant);
            ViewBag.Job = new SelectList(db.CompanyJobsDescriptions, "Job", "JobName", applicantJobApplicationPoco.Job);
            return View(applicantJobApplicationPoco);
        }

        // GET: ApplicantJobApplicationPocoes/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicantJobApplicationPoco applicantJobApplicationPoco = db.ApplicantJobApplications.Find(id);
            if (applicantJobApplicationPoco == null)
            {
                return HttpNotFound();
            }
            return View(applicantJobApplicationPoco);
        }

        // POST: ApplicantJobApplicationPocoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            ApplicantJobApplicationPoco applicantJobApplicationPoco = db.ApplicantJobApplications.Find(id);
            db.ApplicantJobApplications.Remove(applicantJobApplicationPoco);
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
