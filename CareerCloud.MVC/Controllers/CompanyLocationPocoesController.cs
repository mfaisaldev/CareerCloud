﻿using System;
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
    public class CompanyLocationPocoesController : Controller
    {
        private CareerCloudContext db = new CareerCloudContext();

        // GET: CompanyLocationPocoes
        public ActionResult Index()
        {
            var companyLocations = db.CompanyLocations.Include(c => c.CompanyProfiles);
            return View(companyLocations.ToList());
        }

        // GET: CompanyLocationPocoes/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyLocationPoco companyLocationPoco = db.CompanyLocations.Find(id);
            if (companyLocationPoco == null)
            {
                return HttpNotFound();
            }
            return View(companyLocationPoco);
        }

        // GET: CompanyLocationPocoes/Create
        public ActionResult Create()
        {
            ViewBag.Company = new SelectList(db.CompanyProfiles, "Id", "CompanyWebsite");
            return View();
        }

        // POST: CompanyLocationPocoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Company,CountryCode,Province,Street,City,PostalCode")] CompanyLocationPoco companyLocationPoco)
        {
            if (ModelState.IsValid)
            {
                companyLocationPoco.Id = Guid.NewGuid();
                db.CompanyLocations.Add(companyLocationPoco);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Company = new SelectList(db.CompanyProfiles, "Id", "CompanyWebsite", companyLocationPoco.Company);
            return View(companyLocationPoco);
        }

        // GET: CompanyLocationPocoes/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyLocationPoco companyLocationPoco = db.CompanyLocations.Find(id);
            if (companyLocationPoco == null)
            {
                return HttpNotFound();
            }
            ViewBag.Company = new SelectList(db.CompanyProfiles, "Id", "CompanyWebsite", companyLocationPoco.Company);
            return View(companyLocationPoco);
        }

        // POST: CompanyLocationPocoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Company,CountryCode,Province,Street,City,PostalCode")] CompanyLocationPoco companyLocationPoco)
        {
            if (ModelState.IsValid)
            {
                db.Entry(companyLocationPoco).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Company = new SelectList(db.CompanyProfiles, "Id", "CompanyWebsite", companyLocationPoco.Company);
            return View(companyLocationPoco);
        }

        // GET: CompanyLocationPocoes/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyLocationPoco companyLocationPoco = db.CompanyLocations.Find(id);
            if (companyLocationPoco == null)
            {
                return HttpNotFound();
            }
            return View(companyLocationPoco);
        }

        // POST: CompanyLocationPocoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            CompanyLocationPoco companyLocationPoco = db.CompanyLocations.Find(id);
            db.CompanyLocations.Remove(companyLocationPoco);
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
