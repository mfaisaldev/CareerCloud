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
    public class ApplicantEducationPocoesController : Controller
    {
        private CareerCloudContext db = new CareerCloudContext();

        // GET: ApplicantEducationPocoes
        public ActionResult Index()
        {
            var applicantEducations = db.ApplicantEducations.Include(a => a.ApplicantProfiles);
            return View(applicantEducations.ToList());
        }

        // GET: ApplicantEducationPocoes/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicantEducationPoco applicantEducationPoco = db.ApplicantEducations.Find(id);
            if (applicantEducationPoco == null)
            {
                return HttpNotFound();
            }
            return View(applicantEducationPoco);
        }

        // GET: ApplicantEducationPocoes/Create
        public ActionResult Create()
        {
            ViewBag.Applicant = new SelectList(db.SecurityLogins, "Id", "FullName");
            return View();
        }

        // POST: ApplicantEducationPocoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Applicant,Major,CertificateDiploma,StartDate,CompletionDate,CompletionPercent")] ApplicantEducationPoco applicantEducationPoco)
        {
            if (ModelState.IsValid)
            {
                applicantEducationPoco.Id = Guid.NewGuid();
				//ApplicantProfilePoco applicantProfilePoco = new ApplicantProfilePoco();
				applicantEducationPoco.ApplicantProfiles = db.ApplicantProfiles.Where(t => t.Login == applicantEducationPoco.Applicant).FirstOrDefault();
                db.ApplicantEducations.Add(applicantEducationPoco);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Applicant = new SelectList(db.SecurityLogins, "Id", "FullName", applicantEducationPoco.Applicant);
            return View(applicantEducationPoco);
        }

        // GET: ApplicantEducationPocoes/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicantEducationPoco applicantEducationPoco = db.ApplicantEducations.Find(id);
            if (applicantEducationPoco == null)
            {
                return HttpNotFound();
            }
            ViewBag.Applicant = new SelectList(db.SecurityLogins, "Id", "FullName", applicantEducationPoco.Applicant);
            return View(applicantEducationPoco);
        }

        // POST: ApplicantEducationPocoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Applicant,Major,CertificateDiploma,StartDate,CompletionDate,CompletionPercent")] ApplicantEducationPoco applicantEducationPoco)
        {
            if (ModelState.IsValid)
            {
                db.Entry(applicantEducationPoco).State = EntityState.Modified;
				applicantEducationPoco.ApplicantProfiles = db.ApplicantProfiles.Where(t => t.Login == applicantEducationPoco.Applicant).FirstOrDefault();
				db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Applicant = new SelectList(db.SecurityLogins, "Id", "FullName", applicantEducationPoco.Applicant);
            return View(applicantEducationPoco);
        }

        // GET: ApplicantEducationPocoes/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicantEducationPoco applicantEducationPoco = db.ApplicantEducations.Find(id);
            if (applicantEducationPoco == null)
            {
                return HttpNotFound();
            }
            return View(applicantEducationPoco);
        }

        // POST: ApplicantEducationPocoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            ApplicantEducationPoco applicantEducationPoco = db.ApplicantEducations.Find(id);
            db.ApplicantEducations.Remove(applicantEducationPoco);
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
