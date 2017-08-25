using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DAL;
using Web.Models;
using DAL.Interface;

namespace WebApplication1.Controllers
{
    public class CustomerProductsController : Controller
    {
        private ShopDevEntities db = new ShopDevEntities();
        private ICustomerProduct _ICustomerProduct;
        public CustomerProductsController(ICustomerProduct customerProduct)
        {
            this._ICustomerProduct = customerProduct;
        }
        // GET: CustomerProducts
        public ActionResult Index()
        {
            return View(db.CustomerProducts.ToList());
        }

        [HttpPost]
        public ActionResult AddProduct(OrderModel objOrderModel)
        {
            CustomerProductModel objCustomer = objOrderModel.cusProduct;
            List<CustomerProductModel> lstAddedProducts = _ICustomerProduct.AddCustomerProduct(objOrderModel.cusProduct);
            objOrderModel.cusProduct = new CustomerProductModel();
            objOrderModel.lstcusProduct = new List<CustomerProductModel>();
            objOrderModel.lstcusProduct.AddRange(lstAddedProducts);
            return PartialView("~/Views/Orders/_CustomerProduct.cshtml", objOrderModel);
        }
        // GET: CustomerProducts/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerProduct customerProduct = db.CustomerProducts.Find(id);
            if (customerProduct == null)
            {
                return HttpNotFound();
            }
            return View(customerProduct);
        }

        // GET: CustomerProducts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustomerProducts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductID,ProductName,OrderID,AppxWeight,ActualWeight,Description,IsActive,CreatedDate,CreatedBy,UpdatedDate,Updatedby")] CustomerProduct customerProduct)
        {
            if (ModelState.IsValid)
            {
                db.CustomerProducts.Add(customerProduct);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customerProduct);
        }

        // GET: CustomerProducts/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerProduct customerProduct = db.CustomerProducts.Find(id);
            if (customerProduct == null)
            {
                return HttpNotFound();
            }
            return View(customerProduct);
        }

        // POST: CustomerProducts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductID,ProductName,OrderID,AppxWeight,ActualWeight,Description,IsActive,CreatedDate,CreatedBy,UpdatedDate,Updatedby")] CustomerProduct customerProduct)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customerProduct).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customerProduct);
        }

        // GET: CustomerProducts/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerProduct customerProduct = db.CustomerProducts.Find(id);
            if (customerProduct == null)
            {
                return HttpNotFound();
            }
            return View(customerProduct);
        }

        // POST: CustomerProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            CustomerProduct customerProduct = db.CustomerProducts.Find(id);
            db.CustomerProducts.Remove(customerProduct);
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
