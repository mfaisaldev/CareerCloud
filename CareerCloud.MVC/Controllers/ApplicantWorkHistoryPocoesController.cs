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
    public class ApplicantWorkHistoryPocoesController : Controller
    {
        private CareerCloudContext db = new CareerCloudContext();

        // GET: ApplicantWorkHistoryPocoes
        public ActionResult Index()
        {
            var applicantWorkHistory = db.ApplicantWorkHistory.Include(a => a.ApplicantProfiles).Include(a => a.CountryCodes);
            return View(applicantWorkHistory.ToList());
        }

        // GET: ApplicantWorkHistoryPocoes/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicantWorkHistoryPoco applicantWorkHistoryPoco = db.ApplicantWorkHistory.Find(id);
            if (applicantWorkHistoryPoco == null)
            {
                return HttpNotFound();
            }
            return View(applicantWorkHistoryPoco);
        }

        // GET: ApplicantWorkHistoryPocoes/Create
        public ActionResult Create()
        {
            ViewBag.Applicant = new SelectList(db.SecurityLogins, "Id", "FullName");
            ViewBag.CountryCode = new SelectList(db.SystemCountryCodes, "Code", "Name");
            return View();
        }

        // POST: ApplicantWorkHistoryPocoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Applicant,CompanyName,CountryCode,Location,JobTitle,JobDescription,StartMonth,StartYear,EndMonth,EndYear")] ApplicantWorkHistoryPoco applicantWorkHistoryPoco)
        {
            if (ModelState.IsValid)
            {
                applicantWorkHistoryPoco.Id = Guid.NewGuid();
                db.ApplicantWorkHistory.Add(applicantWorkHistoryPoco);
				applicantWorkHistoryPoco.ApplicantProfiles = db.ApplicantProfiles.Where(t => t.Login == applicantWorkHistoryPoco.Applicant).FirstOrDefault();
				db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Applicant = new SelectList(db.SecurityLogins, "Id", "FullName", applicantWorkHistoryPoco.Applicant);
            ViewBag.CountryCode = new SelectList(db.SystemCountryCodes, "Code", "Name", applicantWorkHistoryPoco.CountryCode);
            return View(applicantWorkHistoryPoco);
        }

        // GET: ApplicantWorkHistoryPocoes/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicantWorkHistoryPoco applicantWorkHistoryPoco = db.ApplicantWorkHistory.Find(id);
            if (applicantWorkHistoryPoco == null)
            {
                return HttpNotFound();
            }
            ViewBag.Applicant = new SelectList(db.SecurityLogins, "Id", "FullName", applicantWorkHistoryPoco.Applicant);
            ViewBag.CountryCode = new SelectList(db.SystemCountryCodes, "Code", "Name", applicantWorkHistoryPoco.CountryCode);
            return View(applicantWorkHistoryPoco);
        }

        // POST: ApplicantWorkHistoryPocoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Applicant,CompanyName,CountryCode,Location,JobTitle,JobDescription,StartMonth,StartYear,EndMonth,EndYear")] ApplicantWorkHistoryPoco applicantWorkHistoryPoco)
        {
            if (ModelState.IsValid)
            {
                db.Entry(applicantWorkHistoryPoco).State = EntityState.Modified;
				applicantWorkHistoryPoco.ApplicantProfiles = db.ApplicantProfiles.Where(t => t.Login == applicantWorkHistoryPoco.Applicant).FirstOrDefault();
				db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Applicant = new SelectList(db.SecurityLogins, "Id", "Fullname", applicantWorkHistoryPoco.Applicant);
            ViewBag.CountryCode = new SelectList(db.SystemCountryCodes, "Code", "Name", applicantWorkHistoryPoco.CountryCode);
            return View(applicantWorkHistoryPoco);
        }

        // GET: ApplicantWorkHistoryPocoes/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicantWorkHistoryPoco applicantWorkHistoryPoco = db.ApplicantWorkHistory.Find(id);
            if (applicantWorkHistoryPoco == null)
            {
                return HttpNotFound();
            }
            return View(applicantWorkHistoryPoco);
        }

        // POST: ApplicantWorkHistoryPocoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            ApplicantWorkHistoryPoco applicantWorkHistoryPoco = db.ApplicantWorkHistory.Find(id);
            db.ApplicantWorkHistory.Remove(applicantWorkHistoryPoco);
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
