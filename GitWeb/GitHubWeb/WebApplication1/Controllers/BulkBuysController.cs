﻿using System;
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
using WebApplication1.Filter;

namespace WebApplication1.Controllers
{
    [AuthFilter]
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
            BulkBuySearchViewModel vModel = new BulkBuySearchViewModel();
            vModel.BulkBuy = new BulkBuyModel();
            vModel.AllBulks = new List<BulkBuyModel>();
            return View(vModel);
            // return View(db.Buyers.ToList());
        }
        public ActionResult Search(BulkBuySearchViewModel objModel)
        {
            BulkBuySearchViewModel vModel = new BulkBuySearchViewModel();
            vModel.BulkBuy = new BulkBuyModel();
            vModel.AllBulks = new List<BulkBuyModel>();
            List<BulkBuyModel> objRes = _IBulkProduct.AllBulk(objModel.BulkBuy);
            vModel.AllBulks.AddRange(objRes);
            return PartialView("~/Views/BulkBuys/_bulkBuySearch.cshtml", vModel);
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
            _IBulkProduct.DeleteInitilProducts();
            var objbulkBuy = InitilCreateBulk();
            ViewBag.ProductTypeID = new SelectList(db.ProductTypes, "TypeName", "TypeName");
            return View(objbulkBuy);
        }

        // POST: BulkBuys/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BulkBuyViewModel bulkBuy)
        {
            if (ModelState.IsValid)
            {
                _IBulkProduct.AddBulkBuy(bulkBuy.bulkBuyModel);
                var objbulkBuy = InitilCreateBulk();
                //return View(objbulkBuy);
                TempData["Message"] = "Bulk Save Successfully !";
                return RedirectToAction("Index");
            }
            ViewBag.ProductTypeID = new SelectList(db.ProductTypes, "TypeName", "TypeName");
            return View(bulkBuy);
        }

        public ActionResult AddProduct(BulkBuyViewModel objBulkBuyViewModel)
        {
             
            List<BulkBuyProductsModel> lstAddedProducts = _IBulkProduct.AddProduct(objBulkBuyViewModel.Products);
            objBulkBuyViewModel.bulkBuyModel = new BulkBuyModel();
            objBulkBuyViewModel.lstbulkBuyProducts = new List<BulkBuyProductsModel>();
            objBulkBuyViewModel.lstbulkBuyProducts.AddRange(lstAddedProducts);
            objBulkBuyViewModel.lstVendors = new List<VendorDetailsModel>();
            objBulkBuyViewModel.Vendors = new VendorDetailsModel();
            objBulkBuyViewModel.installments = new Installments();
            objBulkBuyViewModel.lstinstallments = new List<Installments>();
            TempData["Message"] = "Product added Successfully !";
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
            objBulkBuyViewModel.lstVendors = new List<VendorDetailsModel>();
            objBulkBuyViewModel.Vendors = new VendorDetailsModel();

            // Installments
            objBulkBuyViewModel.installments = new Installments();
            objBulkBuyViewModel.lstinstallments = new List<Installments>();
            TempData["Message"] = "Product Deleted Successfully !";
            return PartialView("~/Views/BulkBuys/_BulkProducts.cshtml", objBulkBuyViewModel);
        }
        public ActionResult GetProductDetails(long BulkBuyProductID)
        {
            BulkBuyViewModel objBulkBuyViewModel = new BulkBuyViewModel();
 
            // products
            objBulkBuyViewModel.bulkBuyModel = new BulkBuyModel();
            objBulkBuyViewModel.Products = _IBulkProduct.GetProductDetails(BulkBuyProductID);
            objBulkBuyViewModel.lstbulkBuyProducts = new List<BulkBuyProductsModel>();
            
            // vendors
            objBulkBuyViewModel.lstVendors = new List<VendorDetailsModel>();
            objBulkBuyViewModel.Vendors = new VendorDetailsModel();

            // Installments
            objBulkBuyViewModel.installments = new Installments();
            objBulkBuyViewModel.lstinstallments = new List<Installments>();
            ViewBag.ProductTypeID = new SelectList(db.ProductTypes, "TypeName", "TypeName");
            return PartialView("~/Views/BulkBuys/_AddProduct.cshtml", objBulkBuyViewModel);
        }
        public ActionResult AddVendor(BulkBuyViewModel objBulkBuyViewModel)
        {
            List<VendorDetailsModel> lstvendors = _IBulkProduct.AddVendor(objBulkBuyViewModel.Vendors);
            objBulkBuyViewModel.bulkBuyModel = new BulkBuyModel();
            objBulkBuyViewModel.lstbulkBuyProducts = new List<BulkBuyProductsModel>();
            objBulkBuyViewModel.lstVendors = new List<VendorDetailsModel>();
            objBulkBuyViewModel.lstVendors.AddRange(lstvendors);
            objBulkBuyViewModel.Vendors = new VendorDetailsModel();
            objBulkBuyViewModel.installments = new Installments();
            objBulkBuyViewModel.lstinstallments = new List<Installments>();
            TempData["Message"] = "Vendor added Successfully !";
            return PartialView("~/Views/BulkBuys/_bulkVendors.cshtml", objBulkBuyViewModel);
        }
        public ActionResult DeleteVendor(long VendorID,long bulkBuyID)
        {
            BulkBuyViewModel objBulkBuyViewModel = new BulkBuyViewModel();
            List<VendorDetailsModel> lstvendors = _IBulkProduct.DeleteVendor(VendorID, bulkBuyID);
            // products
            objBulkBuyViewModel.bulkBuyModel = new BulkBuyModel();
            objBulkBuyViewModel.lstbulkBuyProducts = new List<BulkBuyProductsModel>();

            // vendors
            objBulkBuyViewModel.lstVendors = new List<VendorDetailsModel>();
            objBulkBuyViewModel.lstVendors.AddRange(lstvendors);
            objBulkBuyViewModel.Vendors = new VendorDetailsModel();

            // Installments
            objBulkBuyViewModel.installments = new Installments();
            objBulkBuyViewModel.lstinstallments = new List<Installments>();
            TempData["Message"] = "Vendor deleted Successfully !";
            return PartialView("~/Views/BulkBuys/_bulkVendors.cshtml", objBulkBuyViewModel);
        }
        public ActionResult GetVendorDetails(long VendorID)
        {
            BulkBuyViewModel objBulkBuyViewModel = new BulkBuyViewModel();
          
            // products
            objBulkBuyViewModel.bulkBuyModel = new BulkBuyModel();
            objBulkBuyViewModel.lstbulkBuyProducts = new List<BulkBuyProductsModel>();

            // vendors
            objBulkBuyViewModel.lstVendors = new List<VendorDetailsModel>();
           
            objBulkBuyViewModel.Vendors = _IBulkProduct.GetVendorDetails(VendorID);

            // Installments
            objBulkBuyViewModel.installments = new Installments();
            objBulkBuyViewModel.lstinstallments = new List<Installments>();

            return PartialView("~/Views/BulkBuys/_AddVendor.cshtml", objBulkBuyViewModel);
        }

        public ActionResult AddInstallment(BulkBuyViewModel objBulkBuyViewModel)
        {
            List<Installments> lstinstallment= _IBulkProduct.AddInstallment(objBulkBuyViewModel.installments);
            objBulkBuyViewModel.bulkBuyModel = new BulkBuyModel();
            objBulkBuyViewModel.lstbulkBuyProducts = new List<BulkBuyProductsModel>();
            objBulkBuyViewModel.lstVendors = new List<VendorDetailsModel>();
           
            objBulkBuyViewModel.Vendors = new VendorDetailsModel();
            objBulkBuyViewModel.installments = new Installments();
            objBulkBuyViewModel.lstinstallments = new List<Installments>();
            objBulkBuyViewModel.lstinstallments.AddRange(lstinstallment);
            TempData["Message"] = "Installment Save Successfully !";
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
            objBulkBuyViewModel.lstVendors = new List<VendorDetailsModel>();
            objBulkBuyViewModel.Vendors = new VendorDetailsModel();

            // Installments
            objBulkBuyViewModel.installments = new Installments();
            objBulkBuyViewModel.lstinstallments = new List<Installments>();
            objBulkBuyViewModel.lstinstallments.AddRange(lstinstallments);
            TempData["Message"] = "Installment delete Successfully !";
            return PartialView("~/Views/BulkBuys/_Installments.cshtml", objBulkBuyViewModel);
        }
        public ActionResult GetInstallMent(long InstallmentID)
        {
            BulkBuyViewModel objBulkBuyViewModel = new BulkBuyViewModel();
           
            // products
            objBulkBuyViewModel.bulkBuyModel = new BulkBuyModel();
            objBulkBuyViewModel.lstbulkBuyProducts = new List<BulkBuyProductsModel>();

            // vendors
            objBulkBuyViewModel.lstVendors = new List<VendorDetailsModel>();
            objBulkBuyViewModel.Vendors = new VendorDetailsModel();

            // Installments
            //Installments installment = 
            objBulkBuyViewModel.installments = _IBulkProduct.GetInstallmentDetails(InstallmentID); ;
            objBulkBuyViewModel.lstinstallments = new List<Installments>();
            return PartialView("~/Views/BulkBuys/_BulkAddVendorInstallment.cshtml", objBulkBuyViewModel);
        }
        // GET: BulkBuys/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BulkBuyViewModel bulkBuy = _IBulkProduct.GetBulk(id);
            if (bulkBuy == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductTypeID = new SelectList(db.ProductTypes, "TypeName", "TypeName");
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
                TempData["Message"] = "Bulk updated Successfully !";
                return RedirectToAction("Index");
            }
            ViewBag.ProductTypeID = new SelectList(db.ProductTypes, "TypeName", "TypeName");
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
            TempData["Message"] = "Bulk deleted Successfully !";
            return RedirectToAction("Index");
        }
        private BulkBuyViewModel InitilCreateBulk()
        {
            BulkBuyViewModel objbulkBuy = new BulkBuyViewModel();
            objbulkBuy.Vendors = new VendorDetailsModel();
            objbulkBuy.Products = new BulkBuyProductsModel();
            objbulkBuy.lstbulkBuyProducts = new List<BulkBuyProductsModel>();
            objbulkBuy.lstVendors = new List<VendorDetailsModel>();
            objbulkBuy.installments = new Installments();
            objbulkBuy.lstinstallments = new List<Installments>();
            return objbulkBuy;
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
