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
    public class CompanyJobEducationPocoesController : Controller
    {
        private CareerCloudContext db = new CareerCloudContext();

        // GET: CompanyJobEducationPocoes
        public ActionResult Index()
        {
            var companyJobEducations = db.CompanyJobEducations.Include(c => c.CompanyJobs.CompanyJobsDescriptions);
            return View(companyJobEducations.ToList());
        }

        // GET: CompanyJobEducationPocoes/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyJobEducationPoco companyJobEducationPoco = db.CompanyJobEducations.Find(id);
            if (companyJobEducationPoco == null)
            {
                return HttpNotFound();
            }
            return View(companyJobEducationPoco);
        }

        // GET: CompanyJobEducationPocoes/Create
        public ActionResult Create()
        {
            ViewBag.Job = new SelectList(db.CompanyJobsDescriptions, "Job", "JobName");
            return View();
        }

        // POST: CompanyJobEducationPocoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Job,Major,Importance")] CompanyJobEducationPoco companyJobEducationPoco)
        {
            if (ModelState.IsValid)
            {
                companyJobEducationPoco.Id = Guid.NewGuid();
				companyJobEducationPoco.CompanyJobs = db.CompanyJobs.Where(e => e.Id == companyJobEducationPoco.Job).FirstOrDefault();

				db.CompanyJobEducations.Add(companyJobEducationPoco);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Job = new SelectList(db.CompanyJobsDescriptions, "Job", "JobName", companyJobEducationPoco.Job);
            return View(companyJobEducationPoco);
        }

        // GET: CompanyJobEducationPocoes/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyJobEducationPoco companyJobEducationPoco = db.CompanyJobEducations.Find(id);
            if (companyJobEducationPoco == null)
            {
                return HttpNotFound();
            }
            ViewBag.Job = new SelectList(db.CompanyJobsDescriptions, "Job", "JobName", companyJobEducationPoco.Job);
            return View(companyJobEducationPoco);
        }

        // POST: CompanyJobEducationPocoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Job,Major,Importance")] CompanyJobEducationPoco companyJobEducationPoco)
        {
            if (ModelState.IsValid)
            {
                db.Entry(companyJobEducationPoco).State = EntityState.Modified;
				companyJobEducationPoco.CompanyJobs = db.CompanyJobs.Where(e => e.Id == companyJobEducationPoco.Job).FirstOrDefault();
				db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Job = new SelectList(db.CompanyJobsDescriptions, "Job", "JobName", companyJobEducationPoco.Job);
            return View(companyJobEducationPoco);
        }

        // GET: CompanyJobEducationPocoes/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyJobEducationPoco companyJobEducationPoco = db.CompanyJobEducations.Find(id);
            if (companyJobEducationPoco == null)
            {
                return HttpNotFound();
            }
            return View(companyJobEducationPoco);
        }

        // POST: CompanyJobEducationPocoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            CompanyJobEducationPoco companyJobEducationPoco = db.CompanyJobEducations.Find(id);
            db.CompanyJobEducations.Remove(companyJobEducationPoco);
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
