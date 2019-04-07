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
    public class CompanyJobSkillPocoesController : Controller
    {
        private CareerCloudContext db = new CareerCloudContext();

        // GET: CompanyJobSkillPocoes
        public ActionResult Index()
        {
            var companyJobSkills = db.CompanyJobSkills.Include(c => c.CompanyJobs).Include(e=>e.CompanyJobs.CompanyJobsDescriptions);
            return View(companyJobSkills.ToList());
        }

        // GET: CompanyJobSkillPocoes/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyJobSkillPoco companyJobSkillPoco = db.CompanyJobSkills.Find(id);
            if (companyJobSkillPoco == null)
            {
                return HttpNotFound();
            }
            return View(companyJobSkillPoco);
        }

        // GET: CompanyJobSkillPocoes/Create
        public ActionResult Create()
        {
            ViewBag.Job = new SelectList(db.CompanyJobsDescriptions, "Job", "JobName");
            return View();
        }

        // POST: CompanyJobSkillPocoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Job,Skill,SkillLevel,Importance")] CompanyJobSkillPoco companyJobSkillPoco)
        {
            if (ModelState.IsValid)
            {
                companyJobSkillPoco.Id = Guid.NewGuid();
                db.CompanyJobSkills.Add(companyJobSkillPoco);
				companyJobSkillPoco.CompanyJobs = db.CompanyJobs.Where(e => e.Id == companyJobSkillPoco.Job).FirstOrDefault();
				db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Job = new SelectList(db.CompanyJobsDescriptions, "Job", "JobName", companyJobSkillPoco.Job);
            return View(companyJobSkillPoco);
        }

        // GET: CompanyJobSkillPocoes/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyJobSkillPoco companyJobSkillPoco = db.CompanyJobSkills.Find(id);
            if (companyJobSkillPoco == null)
            {
                return HttpNotFound();
            }
            ViewBag.Job = new SelectList(db.CompanyJobsDescriptions, "Job", "JobName", companyJobSkillPoco.Job);
            return View(companyJobSkillPoco);
        }

        // POST: CompanyJobSkillPocoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Job,Skill,SkillLevel,Importance")] CompanyJobSkillPoco companyJobSkillPoco)
        {
            if (ModelState.IsValid)
            {
                db.Entry(companyJobSkillPoco).State = EntityState.Modified;
				companyJobSkillPoco.CompanyJobs = db.CompanyJobs.Where(e => e.Id == companyJobSkillPoco.Job).FirstOrDefault();
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Job = new SelectList(db.CompanyJobsDescriptions, "Job", "JobName", companyJobSkillPoco.Job);
            return View(companyJobSkillPoco);
        }

        // GET: CompanyJobSkillPocoes/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyJobSkillPoco companyJobSkillPoco = db.CompanyJobSkills.Find(id);
            if (companyJobSkillPoco == null)
            {
                return HttpNotFound();
            }
            return View(companyJobSkillPoco);
        }

        // POST: CompanyJobSkillPocoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            CompanyJobSkillPoco companyJobSkillPoco = db.CompanyJobSkills.Find(id);
            db.CompanyJobSkills.Remove(companyJobSkillPoco);
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
