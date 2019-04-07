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
    public class SecurityRolePocoesController : Controller
    {
        private CareerCloudContext db = new CareerCloudContext();

        // GET: SecurityRolePocoes
        public ActionResult Index()
        {
            return View(db.SecurityRoles.ToList());
        }

        // GET: SecurityRolePocoes/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SecurityRolePoco securityRolePoco = db.SecurityRoles.Find(id);
            if (securityRolePoco == null)
            {
                return HttpNotFound();
            }
            return View(securityRolePoco);
        }

        // GET: SecurityRolePocoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SecurityRolePocoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Role,IsInactive")] SecurityRolePoco securityRolePoco)
        {
            if (ModelState.IsValid)
            {
                securityRolePoco.Id = Guid.NewGuid();
                db.SecurityRoles.Add(securityRolePoco);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(securityRolePoco);
        }

        // GET: SecurityRolePocoes/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SecurityRolePoco securityRolePoco = db.SecurityRoles.Find(id);
            if (securityRolePoco == null)
            {
                return HttpNotFound();
            }
            return View(securityRolePoco);
        }

        // POST: SecurityRolePocoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Role,IsInactive")] SecurityRolePoco securityRolePoco)
        {
            if (ModelState.IsValid)
            {
                db.Entry(securityRolePoco).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(securityRolePoco);
        }

        // GET: SecurityRolePocoes/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SecurityRolePoco securityRolePoco = db.SecurityRoles.Find(id);
            if (securityRolePoco == null)
            {
                return HttpNotFound();
            }
            return View(securityRolePoco);
        }

        // POST: SecurityRolePocoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            SecurityRolePoco securityRolePoco = db.SecurityRoles.Find(id);
            db.SecurityRoles.Remove(securityRolePoco);
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
