using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DAL;
using DAL.Interface;
using Web.Models.ViewModel;
using Web.Models;

namespace WebApplication1.Controllers
{
    public class SellersController : Controller
    {
        private ShopDevEntities db = new ShopDevEntities();
        private ISeller _ISeller;
        public SellersController(ISeller iSeller)
        {
            this._ISeller = iSeller;
        }
        // GET: Sellers
        public ActionResult Index()
        {
            return View(db.Sellers.ToList());
        }

        // GET: Sellers/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SellerViewModel seller = _ISeller.GetSellerInfo(id);
            if (seller == null)
            {
                return HttpNotFound();
            }
            return View(seller);
        }

        // GET: Sellers/Create
        public ActionResult Create()
        {
            _ISeller.DeleteInitilProducts();
            var objbuyer = InitilCreateBulk();
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryName", "CategoryName");
            ViewBag.ProductTypeID = new SelectList(db.ProductTypes, "TypeName", "TypeName");
            return View(objbuyer);
        }

        // POST: Sellers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SellerViewModel seller)
        {
            if (ModelState.IsValid)
            {
                _ISeller.AddSeller(seller.sellerInfor);
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryName", "CategoryName");
            ViewBag.ProductTypeID = new SelectList(db.ProductTypes, "TypeName", "TypeName");
            return View(seller);
        }

        public ActionResult AddSellerProduct(SellerViewModel objSeller)
        {

            List<SellerProductModel> lstAddedProducts = _ISeller.AddSellerProduct(objSeller.productInfo);
            objSeller.sellerInfor = new SellerModel();
            objSeller.productInfo = new SellerProductModel();
            objSeller.Lstproducts = new List<SellerProductModel>();
            objSeller.Lstproducts.AddRange(lstAddedProducts);
            
            objSeller.Installments = new SellerInstallmentModel();
            objSeller.LstInstallments = new List<SellerInstallmentModel>();
            return PartialView("~/Views/Sellers/_SellerProducts.cshtml", objSeller);
        }


        public ActionResult DeleteProduct(long productID, long buyerID)
        {
            SellerViewModel objSeller = new SellerViewModel();
            List<SellerProductModel> lstproducts = _ISeller.DeleteSellerProduct(productID, buyerID);
            // products
            objSeller.sellerInfor = new SellerModel();
            objSeller.productInfo = new SellerProductModel();
            objSeller.Lstproducts = new List<SellerProductModel>();
            objSeller.Lstproducts.AddRange(lstproducts);


            // Installments
            objSeller.Installments = new SellerInstallmentModel();
            objSeller.LstInstallments = new List<SellerInstallmentModel>();
            return PartialView("~/Views/Sellers/_SellerProducts.cshtml", objSeller);
        }
        public ActionResult AddSellerInstallment(SellerViewModel sellerviewModel)
        {
            List<SellerInstallmentModel> lstinstallment = _ISeller.AddSellerInstallment(sellerviewModel.Installments);
            sellerviewModel.sellerInfor = new SellerModel();
            sellerviewModel.Lstproducts = new List<SellerProductModel>();
             
            sellerviewModel.Installments = new SellerInstallmentModel();
            sellerviewModel.LstInstallments = new List<SellerInstallmentModel>();
            sellerviewModel.LstInstallments.AddRange(lstinstallment);
            return PartialView("~/Views/Sellers/_SellerInstallments.cshtml", sellerviewModel);
        }
        public ActionResult DeleteInstallment(long InstallmentID, long buyerID)
        {
            SellerViewModel sellerviewModel = new SellerViewModel();
            List<SellerInstallmentModel> lstinstallments = _ISeller.DeleteSellerInstallment(InstallmentID, buyerID);
            // products
            sellerviewModel.sellerInfor = new SellerModel();
            sellerviewModel.productInfo = new SellerProductModel();
            sellerviewModel.Lstproducts = new List<SellerProductModel>();

            // Installments
            sellerviewModel.Installments = new SellerInstallmentModel();
            sellerviewModel.LstInstallments = new List<SellerInstallmentModel>();
            sellerviewModel.LstInstallments.AddRange(lstinstallments);
            return PartialView("~/Views/Seller/_SellerInstallments.cshtml", sellerviewModel);
        }
        // GET: Sellers/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SellerViewModel seller = _ISeller.GetSellerInfo(id);
            if (seller == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryName", "CategoryName");
            ViewBag.ProductTypeID = new SelectList(db.ProductTypes, "TypeName", "TypeName");
            return View(seller);
        }

        // POST: Sellers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SellerViewModel seller)
        {
            if (ModelState.IsValid)
            {
                _ISeller.AddSeller(seller.sellerInfor);
                return RedirectToAction("Index");
                 
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryName", "CategoryName");
            ViewBag.ProductTypeID = new SelectList(db.ProductTypes, "TypeName", "TypeName");
            return View(seller);
        }

        // GET: Sellers/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Seller seller = db.Sellers.Find(id);
            if (seller == null)
            {
                return HttpNotFound();
            }
            return View(seller);
        }

        // POST: Sellers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Seller seller = db.Sellers.Find(id);
            db.Sellers.Remove(seller);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        private SellerViewModel InitilCreateBulk()
        {
            SellerViewModel objSeller = new SellerViewModel();
            //objbuyer.buyer = new BuyerModel();
            objSeller.productInfo = new SellerProductModel();
            objSeller.Lstproducts = new List<SellerProductModel>();
            objSeller.Installments = new SellerInstallmentModel();
            objSeller.LstInstallments = new List<SellerInstallmentModel>();
            return objSeller;
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
