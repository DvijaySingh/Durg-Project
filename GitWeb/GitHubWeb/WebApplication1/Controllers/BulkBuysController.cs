using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DAL;
using Web.Models.ViewModel;
using Web.Models;
using DAL.Interface;

namespace WebApplication1.Controllers
{
    public class BulkBuysController : Controller
    {
        private ShopDevEntities db = new ShopDevEntities();
        private IBulkProduct _IBulkProduct;
        public BulkBuysController(IBulkProduct bulkProduct)
        {
            this._IBulkProduct = bulkProduct;
        }
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
            BulkBuyViewModel objbulkBuy = new BulkBuyViewModel();
            objbulkBuy.Vendors = new VendorModel();
            objbulkBuy.Products = new BulkBuyProductsModel();
            objbulkBuy.lstbulkBuyProducts = new List<BulkBuyProductsModel>();
            objbulkBuy.lstVendors = new List<VendorModel>();

            return View(objbulkBuy);
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

        public ActionResult AddProduct(BulkBuyViewModel objBulkBuyViewModel)
        {
             
            List<BulkBuyProductsModel> lstAddedProducts = _IBulkProduct.AddProduct(objBulkBuyViewModel.Products);
            objBulkBuyViewModel.bulkBuyModel = new BulkBuyModel();
            objBulkBuyViewModel.lstbulkBuyProducts = new List<BulkBuyProductsModel>();
            objBulkBuyViewModel.lstbulkBuyProducts.AddRange(lstAddedProducts);
            objBulkBuyViewModel.lstVendors = new List<VendorModel>();
            objBulkBuyViewModel.Vendors = new VendorModel();
            objBulkBuyViewModel.installments = new Installments();
            objBulkBuyViewModel.lstinstallments = new List<Installments>();
            return PartialView("~/Views/BulkBuys/_BulkProducts.cshtml", objBulkBuyViewModel);
        }


        public ActionResult DeleteProduct(long BulkBuyProductID, long bulkBuyID)
        {
            BulkBuyViewModel objBulkBuyViewModel = new BulkBuyViewModel();
            List<BulkBuyProductsModel> lstproducts = _IBulkProduct.DeleteProduct(BulkBuyProductID, bulkBuyID);
            // products
            objBulkBuyViewModel.bulkBuyModel = new BulkBuyModel();
            objBulkBuyViewModel.lstbulkBuyProducts = new List<BulkBuyProductsModel>();
            objBulkBuyViewModel.lstbulkBuyProducts.AddRange(lstproducts);
            // vendors
            objBulkBuyViewModel.lstVendors = new List<VendorModel>();
            objBulkBuyViewModel.Vendors = new VendorModel();

            // Installments
            objBulkBuyViewModel.installments = new Installments();
            objBulkBuyViewModel.lstinstallments = new List<Installments>();
            return PartialView("~/Views/BulkBuys/_BulkProducts.cshtml", objBulkBuyViewModel);
        }
        public ActionResult AddVendor(BulkBuyViewModel objBulkBuyViewModel)
        {
            List<VendorModel> lstvendors = _IBulkProduct.AddVendor(objBulkBuyViewModel.Vendors);
            objBulkBuyViewModel.bulkBuyModel = new BulkBuyModel();
            objBulkBuyViewModel.lstbulkBuyProducts = new List<BulkBuyProductsModel>();
            objBulkBuyViewModel.lstVendors = new List<VendorModel>();
            objBulkBuyViewModel.lstVendors.AddRange(lstvendors);
            objBulkBuyViewModel.Vendors = new VendorModel();
            objBulkBuyViewModel.installments = new Installments();
            objBulkBuyViewModel.lstinstallments = new List<Installments>();
            return PartialView("~/Views/BulkBuys/_BulkProducts.cshtml", objBulkBuyViewModel);
        }
        public ActionResult DeleteVendor(long VendorID,long bulkBuyID)
        {
            BulkBuyViewModel objBulkBuyViewModel = new BulkBuyViewModel();
            List<VendorModel> lstvendors = _IBulkProduct.DeleteVendor(VendorID, bulkBuyID);
            // products
            objBulkBuyViewModel.bulkBuyModel = new BulkBuyModel();
            objBulkBuyViewModel.lstbulkBuyProducts = new List<BulkBuyProductsModel>();

            // vendors
            objBulkBuyViewModel.lstVendors = new List<VendorModel>();
            objBulkBuyViewModel.lstVendors.AddRange(lstvendors);
            objBulkBuyViewModel.Vendors = new VendorModel();

            // Installments
            objBulkBuyViewModel.installments = new Installments();
            objBulkBuyViewModel.lstinstallments = new List<Installments>();
            return PartialView("~/Views/BulkBuys/_bulkVendors.cshtml", objBulkBuyViewModel);
        }

        public ActionResult AddInstallment(BulkBuyViewModel objBulkBuyViewModel)
        {
            List<Installments> lstinstallment= _IBulkProduct.AddInstallment(objBulkBuyViewModel.installments);
            objBulkBuyViewModel.bulkBuyModel = new BulkBuyModel();
            objBulkBuyViewModel.lstbulkBuyProducts = new List<BulkBuyProductsModel>();
            objBulkBuyViewModel.lstVendors = new List<VendorModel>();
           
            objBulkBuyViewModel.Vendors = new VendorModel();
            objBulkBuyViewModel.installments = new Installments();
            objBulkBuyViewModel.lstinstallments = new List<Installments>();
            objBulkBuyViewModel.lstinstallments.AddRange(lstinstallment);
            return PartialView("~/Views/BulkBuys/_Installments.cshtml", objBulkBuyViewModel);
        }
        public ActionResult DeleteInstallment(long InstallmentID, long bulkBuyID)
        {
            BulkBuyViewModel objBulkBuyViewModel = new BulkBuyViewModel();
            List<Installments> lstinstallments = _IBulkProduct.DeleteDeleteInstallment(InstallmentID, bulkBuyID);
            // products
            objBulkBuyViewModel.bulkBuyModel = new BulkBuyModel();
            objBulkBuyViewModel.lstbulkBuyProducts = new List<BulkBuyProductsModel>();

            // vendors
            objBulkBuyViewModel.lstVendors = new List<VendorModel>();
            objBulkBuyViewModel.Vendors = new VendorModel();

            // Installments
            objBulkBuyViewModel.installments = new Installments();
            objBulkBuyViewModel.lstinstallments = new List<Installments>();
            objBulkBuyViewModel.lstinstallments.AddRange(lstinstallments);
            return PartialView("~/Views/BulkBuys/_Installments.cshtml.cshtml", objBulkBuyViewModel);
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
