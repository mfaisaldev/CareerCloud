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
    public class CompanyDescriptionPocoesController : Controller
    {
        private CareerCloudContext db = new CareerCloudContext();

        // GET: CompanyDescriptionPocoes
        public ActionResult Index()
        {
            var companyDescriptions = db.CompanyDescriptions.Include(c => c.CompanyProfiles).Include(c => c.SystemLanguageCodes);
            return View(companyDescriptions.ToList());
        }

        // GET: CompanyDescriptionPocoes/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyDescriptionPoco companyDescriptionPoco = db.CompanyDescriptions.Find(id);
            if (companyDescriptionPoco == null)
            {
                return HttpNotFound();
            }
            return View(companyDescriptionPoco);
        }

        // GET: CompanyDescriptionPocoes/Create
        public ActionResult Create()
        {
            ViewBag.Company = new SelectList(db.CompanyDescriptions, "Company", "CompanyName");
            ViewBag.LanguageId = new SelectList(db.SystemLanguageCodes, "LanguageID", "Name");
            return View();
        }

        // POST: CompanyDescriptionPocoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Company,LanguageId,CompanyName,CompanyDescription")] CompanyDescriptionPoco companyDescriptionPoco)
        {
            if (ModelState.IsValid)
            {
                companyDescriptionPoco.Id = Guid.NewGuid();
				companyDescriptionPoco.CompanyProfiles = db.CompanyProfiles.Where(e => e.Id == companyDescriptionPoco.Company).FirstOrDefault();
                db.CompanyDescriptions.Add(companyDescriptionPoco);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Company = new SelectList(db.CompanyDescriptions, "Company", "CompanyName", companyDescriptionPoco.Company);
            ViewBag.LanguageId = new SelectList(db.SystemLanguageCodes, "LanguageID", "Name", companyDescriptionPoco.LanguageId);
            return View(companyDescriptionPoco);
        }

        // GET: CompanyDescriptionPocoes/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyDescriptionPoco companyDescriptionPoco = db.CompanyDescriptions.Find(id);
            if (companyDescriptionPoco == null)
            {
                return HttpNotFound();
            }
            ViewBag.Company = new SelectList(db.CompanyDescriptions, "Company", "CompanyName", companyDescriptionPoco.Company);
            ViewBag.LanguageId = new SelectList(db.SystemLanguageCodes, "LanguageID", "Name", companyDescriptionPoco.LanguageId);
            return View(companyDescriptionPoco);
        }

        // POST: CompanyDescriptionPocoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Company,LanguageId,CompanyName,CompanyDescription")] CompanyDescriptionPoco companyDescriptionPoco)
        {
            if (ModelState.IsValid)
            {
                db.Entry(companyDescriptionPoco).State = EntityState.Modified;
				companyDescriptionPoco.CompanyProfiles = db.CompanyProfiles.Where(e => e.Id == companyDescriptionPoco.Company).FirstOrDefault();
				db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Company = new SelectList(db.CompanyDescriptions, "Company", "CompanyName", companyDescriptionPoco.Company);
            ViewBag.LanguageId = new SelectList(db.SystemLanguageCodes, "LanguageID", "Name", companyDescriptionPoco.LanguageId);
            return View(companyDescriptionPoco);
        }

        // GET: CompanyDescriptionPocoes/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyDescriptionPoco companyDescriptionPoco = db.CompanyDescriptions.Find(id);
            if (companyDescriptionPoco == null)
            {
                return HttpNotFound();
            }
            return View(companyDescriptionPoco);
        }

        // POST: CompanyDescriptionPocoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            CompanyDescriptionPoco companyDescriptionPoco = db.CompanyDescriptions.Find(id);
            db.CompanyDescriptions.Remove(companyDescriptionPoco);
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
