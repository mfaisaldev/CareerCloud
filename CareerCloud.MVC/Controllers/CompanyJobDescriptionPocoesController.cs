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
    public class CompanyJobDescriptionPocoesController : Controller
    {
        private CareerCloudContext db = new CareerCloudContext();

        // GET: CompanyJobDescriptionPocoes
        public ActionResult Index()
        {
            var companyJobsDescriptions = db.CompanyJobsDescriptions.Include(c => c.CompanyJobs);
            return View(companyJobsDescriptions.ToList());
        }

        // GET: CompanyJobDescriptionPocoes/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyJobDescriptionPoco companyJobDescriptionPoco = db.CompanyJobsDescriptions.Find(id);
            if (companyJobDescriptionPoco == null)
            {
                return HttpNotFound();
            }
            return View(companyJobDescriptionPoco);
        }

        // GET: CompanyJobDescriptionPocoes/Create
        public ActionResult Create()
        {
            ViewBag.Job = new SelectList(db.CompanyJobs, "Id", "Id");
            return View();
        }

        // POST: CompanyJobDescriptionPocoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Job,JobName,JobDescriptions")] CompanyJobDescriptionPoco companyJobDescriptionPoco)
        {
            if (ModelState.IsValid)
            {
                companyJobDescriptionPoco.Id = Guid.NewGuid();
                db.CompanyJobsDescriptions.Add(companyJobDescriptionPoco);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Job = new SelectList(db.CompanyJobs, "Id", "Id", companyJobDescriptionPoco.Job);
            return View(companyJobDescriptionPoco);
        }

        // GET: CompanyJobDescriptionPocoes/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyJobDescriptionPoco companyJobDescriptionPoco = db.CompanyJobsDescriptions.Find(id);
            if (companyJobDescriptionPoco == null)
            {
                return HttpNotFound();
            }
            ViewBag.Job = new SelectList(db.CompanyJobs, "Id", "Id", companyJobDescriptionPoco.Job);
            return View(companyJobDescriptionPoco);
        }

        // POST: CompanyJobDescriptionPocoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Job,JobName,JobDescriptions")] CompanyJobDescriptionPoco companyJobDescriptionPoco)
        {
            if (ModelState.IsValid)
            {
                db.Entry(companyJobDescriptionPoco).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Job = new SelectList(db.CompanyJobs, "Id", "Id", companyJobDescriptionPoco.Job);
            return View(companyJobDescriptionPoco);
        }

        // GET: CompanyJobDescriptionPocoes/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyJobDescriptionPoco companyJobDescriptionPoco = db.CompanyJobsDescriptions.Find(id);
            if (companyJobDescriptionPoco == null)
            {
                return HttpNotFound();
            }
            return View(companyJobDescriptionPoco);
        }

        // POST: CompanyJobDescriptionPocoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            CompanyJobDescriptionPoco companyJobDescriptionPoco = db.CompanyJobsDescriptions.Find(id);
            db.CompanyJobsDescriptions.Remove(companyJobDescriptionPoco);
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
