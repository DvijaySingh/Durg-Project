using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DAL;

namespace WebApplication1.Controllers
{
    public class BulkBuysController : Controller
    {
        private ShopDevEntities db = new ShopDevEntities();

        // GET: BulkBuys
        public ActionResult Index()
        {
            return View(db.BulkBuys.ToList());
        }

        // GET: BulkBuys/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BulkBuy bulkBuy = db.BulkBuys.Find(id);
            if (bulkBuy == null)
            {
                return HttpNotFound();
            }
            return View(bulkBuy);
        }

        // GET: BulkBuys/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BulkBuys/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BulkBuyID,CustomerName,Address,TakenAmount,Rate,GWeight,SWeight,Status")] BulkBuy bulkBuy)
        {
            if (ModelState.IsValid)
            {
                db.BulkBuys.Add(bulkBuy);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bulkBuy);
        }

        // GET: BulkBuys/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BulkBuy bulkBuy = db.BulkBuys.Find(id);
            if (bulkBuy == null)
            {
                return HttpNotFound();
            }
            return View(bulkBuy);
        }

        // POST: BulkBuys/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BulkBuyID,CustomerName,Address,TakenAmount,Rate,GWeight,SWeight,Status")] BulkBuy bulkBuy)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bulkBuy).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bulkBuy);
        }

        // GET: BulkBuys/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BulkBuy bulkBuy = db.BulkBuys.Find(id);
            if (bulkBuy == null)
            {
                return HttpNotFound();
            }
            return View(bulkBuy);
        }

        // POST: BulkBuys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            BulkBuy bulkBuy = db.BulkBuys.Find(id);
            db.BulkBuys.Remove(bulkBuy);
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
