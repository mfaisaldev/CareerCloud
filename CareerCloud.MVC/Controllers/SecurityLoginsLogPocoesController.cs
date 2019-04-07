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
    public class SecurityLoginsLogPocoesController : Controller
    {
        private CareerCloudContext db = new CareerCloudContext();

        // GET: SecurityLoginsLogPocoes
        public ActionResult Index()
        {
            var securityLoginsLogs = db.SecurityLoginsLogs.Include(s => s.SecurityLogins);
            return View(securityLoginsLogs.ToList());
        }

        // GET: SecurityLoginsLogPocoes/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SecurityLoginsLogPoco securityLoginsLogPoco = db.SecurityLoginsLogs.Find(id);
            if (securityLoginsLogPoco == null)
            {
                return HttpNotFound();
            }
            return View(securityLoginsLogPoco);
        }

        // GET: SecurityLoginsLogPocoes/Create
        public ActionResult Create()
        {
            ViewBag.Login = new SelectList(db.SecurityLogins, "Id", "Login");
            return View();
        }

        // POST: SecurityLoginsLogPocoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Login,SourceIP,LogonDate,IsSuccesful")] SecurityLoginsLogPoco securityLoginsLogPoco)
        {
            if (ModelState.IsValid)
            {
                securityLoginsLogPoco.Id = Guid.NewGuid();
                db.SecurityLoginsLogs.Add(securityLoginsLogPoco);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Login = new SelectList(db.SecurityLogins, "Id", "Login", securityLoginsLogPoco.Login);
            return View(securityLoginsLogPoco);
        }

        // GET: SecurityLoginsLogPocoes/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SecurityLoginsLogPoco securityLoginsLogPoco = db.SecurityLoginsLogs.Find(id);
            if (securityLoginsLogPoco == null)
            {
                return HttpNotFound();
            }
            ViewBag.Login = new SelectList(db.SecurityLogins, "Id", "Login", securityLoginsLogPoco.Login);
            return View(securityLoginsLogPoco);
        }

        // POST: SecurityLoginsLogPocoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Login,SourceIP,LogonDate,IsSuccesful")] SecurityLoginsLogPoco securityLoginsLogPoco)
        {
            if (ModelState.IsValid)
            {
                db.Entry(securityLoginsLogPoco).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Login = new SelectList(db.SecurityLogins, "Id", "Login", securityLoginsLogPoco.Login);
            return View(securityLoginsLogPoco);
        }

        // GET: SecurityLoginsLogPocoes/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SecurityLoginsLogPoco securityLoginsLogPoco = db.SecurityLoginsLogs.Find(id);
            if (securityLoginsLogPoco == null)
            {
                return HttpNotFound();
            }
            return View(securityLoginsLogPoco);
        }

        // POST: SecurityLoginsLogPocoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            SecurityLoginsLogPoco securityLoginsLogPoco = db.SecurityLoginsLogs.Find(id);
            db.SecurityLoginsLogs.Remove(securityLoginsLogPoco);
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
