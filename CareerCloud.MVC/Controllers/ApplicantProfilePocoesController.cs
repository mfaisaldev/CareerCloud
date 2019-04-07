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
    public class ApplicantProfilePocoesController : Controller
    {
        private CareerCloudContext db = new CareerCloudContext();

        // GET: ApplicantProfilePocoes
        public ActionResult Index()
        {
            var applicantProfiles = db.ApplicantProfiles.Include(a => a.CountryCodes).Include(a => a.SecurityLogins);
            return View(applicantProfiles.ToList());
        }

        // GET: ApplicantProfilePocoes/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicantProfilePoco applicantProfilePoco = db.ApplicantProfiles.Find(id);
            if (applicantProfilePoco == null)
            {
                return HttpNotFound();
            }
            return View(applicantProfilePoco);
        }

        // GET: ApplicantProfilePocoes/Create
        public ActionResult Create()
        {
            ViewBag.Country = new SelectList(db.SystemCountryCodes, "Code", "Name");
            ViewBag.Login = new SelectList(db.SecurityLogins, "Id", "Login");
            return View();
        }

        // POST: ApplicantProfilePocoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Login,CurrentSalary,CurrentRate,Currency,Country,Province,Street,City,PostalCode")] ApplicantProfilePoco applicantProfilePoco)
        {
            if (ModelState.IsValid)
            {
                applicantProfilePoco.Id = Guid.NewGuid();
                db.ApplicantProfiles.Add(applicantProfilePoco);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Country = new SelectList(db.SystemCountryCodes, "Code", "Name", applicantProfilePoco.Country);
            ViewBag.Login = new SelectList(db.SecurityLogins, "Id", "Login", applicantProfilePoco.Login);
            return View(applicantProfilePoco);
        }

        // GET: ApplicantProfilePocoes/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicantProfilePoco applicantProfilePoco = db.ApplicantProfiles.Find(id);
            if (applicantProfilePoco == null)
            {
                return HttpNotFound();
            }
            ViewBag.Country = new SelectList(db.SystemCountryCodes, "Code", "Name", applicantProfilePoco.Country);
            ViewBag.Login = new SelectList(db.SecurityLogins, "Id", "Login", applicantProfilePoco.Login);
            return View(applicantProfilePoco);
        }

        // POST: ApplicantProfilePocoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Login,CurrentSalary,CurrentRate,Currency,Country,Province,Street,City,PostalCode")] ApplicantProfilePoco applicantProfilePoco)
        {
            if (ModelState.IsValid)
            {
                db.Entry(applicantProfilePoco).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Country = new SelectList(db.SystemCountryCodes, "Code", "Name", applicantProfilePoco.Country);
            ViewBag.Login = new SelectList(db.SecurityLogins, "Id", "Login", applicantProfilePoco.Login);
            return View(applicantProfilePoco);
        }

        // GET: ApplicantProfilePocoes/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicantProfilePoco applicantProfilePoco = db.ApplicantProfiles.Find(id);
            if (applicantProfilePoco == null)
            {
                return HttpNotFound();
            }
            return View(applicantProfilePoco);
        }

        // POST: ApplicantProfilePocoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            ApplicantProfilePoco applicantProfilePoco = db.ApplicantProfiles.Find(id);
            db.ApplicantProfiles.Remove(applicantProfilePoco);
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
