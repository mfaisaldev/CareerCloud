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
    public class ApplicantSkillPocoesController : Controller
    {
        private CareerCloudContext db = new CareerCloudContext();

        // GET: ApplicantSkillPocoes
        public ActionResult Index()
        {
            var applicantSkills = db.ApplicantSkills.Include(a => a.ApplicantProfiles);
            return View(applicantSkills.ToList());
        }

        // GET: ApplicantSkillPocoes/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicantSkillPoco applicantSkillPoco = db.ApplicantSkills.Find(id);
            if (applicantSkillPoco == null)
            {
                return HttpNotFound();
            }
            return View(applicantSkillPoco);
        }

        // GET: ApplicantSkillPocoes/Create
        public ActionResult Create()
        {
            ViewBag.Applicant = new SelectList(db.SecurityLogins, "Id", "FullName");
            return View();
        }

        // POST: ApplicantSkillPocoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Applicant,Skill,SkillLevel,StartMonth,StartYear,EndMonth,EndYear")] ApplicantSkillPoco applicantSkillPoco)
        {
            if (ModelState.IsValid)
            {
                applicantSkillPoco.Id = Guid.NewGuid();
				applicantSkillPoco.ApplicantProfiles = db.ApplicantProfiles.Where(t => t.Login == applicantSkillPoco.Applicant).FirstOrDefault();
				db.ApplicantSkills.Add(applicantSkillPoco);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Applicant = new SelectList(db.SecurityLogins, "Id", "FullName", applicantSkillPoco.Applicant);
            return View(applicantSkillPoco);
        }

        // GET: ApplicantSkillPocoes/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicantSkillPoco applicantSkillPoco = db.ApplicantSkills.Find(id);
            if (applicantSkillPoco == null)
            {
                return HttpNotFound();
            }
            ViewBag.Applicant = new SelectList(db.SecurityLogins, "Id", "FullName", applicantSkillPoco.Applicant);
            return View(applicantSkillPoco);
        }

        // POST: ApplicantSkillPocoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Applicant,Skill,SkillLevel,StartMonth,StartYear,EndMonth,EndYear")] ApplicantSkillPoco applicantSkillPoco)
        {
            if (ModelState.IsValid)
            {
                db.Entry(applicantSkillPoco).State = EntityState.Modified;
				applicantSkillPoco.ApplicantProfiles = db.ApplicantProfiles.Where(t => t.Login == applicantSkillPoco.Applicant).FirstOrDefault();
				db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Applicant = new SelectList(db.SecurityLogins, "Id", "FullName", applicantSkillPoco.Applicant);
            return View(applicantSkillPoco);
        }

        // GET: ApplicantSkillPocoes/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicantSkillPoco applicantSkillPoco = db.ApplicantSkills.Find(id);
            if (applicantSkillPoco == null)
            {
                return HttpNotFound();
            }
            return View(applicantSkillPoco);
        }

        // POST: ApplicantSkillPocoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            ApplicantSkillPoco applicantSkillPoco = db.ApplicantSkills.Find(id);
            db.ApplicantSkills.Remove(applicantSkillPoco);
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
