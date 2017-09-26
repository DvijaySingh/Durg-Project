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
using System.IO;

namespace WebApplication1.Controllers
{
    public class BuyersController : Controller
    {
        private ShopDevEntities db = new ShopDevEntities();
        private IBuyer _IBuyer;
        public BuyersController(IBuyer iBuyer)
        {
            this._IBuyer = iBuyer;
        }
        // GET: Buyers
        public ActionResult Index()
        {
            return View(db.Buyers.ToList());
        }

        // GET: Buyers/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BuyerViewModel buyer = _IBuyer.GetBuyerInfo(id);
            if (buyer == null)
            {
                return HttpNotFound();
            }
            return View(buyer);
        }

        // GET: Buyers/Create
        public ActionResult Create()
        {
            _IBuyer.DeleteInitilProducts();
            var objbuyer = InitilCreateBulk();
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryName", "CategoryName");
            ViewBag.ProductTypeID = new SelectList(db.ProductTypes, "TypeName", "TypeName");
            return View(objbuyer);
        }

        // POST: Buyers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( BuyerViewModel buyerobj,HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if(file!=null)
                {
                    using (MemoryStream target = new MemoryStream())
                    {
                        file.InputStream.CopyTo(target);
                        buyerobj.buyer.Bill = target.ToArray();
                    }
                     
                }
                _IBuyer.AddBuyer(buyerobj.buyer);
                return RedirectToAction("Index");
            }
            else
            {
                buyerobj= InitilCreateBulk();
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryName", "CategoryName");
            ViewBag.ProductTypeID = new SelectList(db.ProductTypes, "TypeName", "TypeName");
            return View(buyerobj);
        }

        public ActionResult AddBuyerProduct(BuyerViewModel objbuyer)
        {

            List<BuyerProductModel> lstAddedProducts = _IBuyer.AddBuyerProduct(objbuyer.productInfo);
            objbuyer.buyer = new BuyerModel();
            objbuyer.productInfo = new BuyerProductModel();
            objbuyer.LstProducts = new List<BuyerProductModel>();
            objbuyer.LstProducts.AddRange(lstAddedProducts);
           
            objbuyer.Installments = new BuyerInstallmentModel();
            objbuyer.LstInstallments = new List<BuyerInstallmentModel>();
            return PartialView("~/Views/Buyers/_BuyerProducts.cshtml", objbuyer);
        }


        public ActionResult DeleteProduct(long productID, long buyerID)
        {
            BuyerViewModel objBuyer = new BuyerViewModel();
            List<BuyerProductModel> lstproducts = _IBuyer.DeleteBuyerProduct(productID, buyerID);
            // products
            objBuyer.buyer = new BuyerModel();
            objBuyer.productInfo = new BuyerProductModel();
            objBuyer.LstProducts = new List<BuyerProductModel>();
            objBuyer.LstProducts.AddRange(lstproducts);
            

            // Installments
            objBuyer.Installments = new BuyerInstallmentModel();
            objBuyer.LstInstallments = new List<BuyerInstallmentModel>();
            return PartialView("~/Views/Buyers/_BuyerProducts.cshtml", objBuyer);
        }
        public ActionResult AddBuyerInstallment(BuyerViewModel buyerviewModel)
        {
            List<BuyerInstallmentModel> lstinstallment = _IBuyer.AddBuyerInstallment(buyerviewModel.Installments);
            buyerviewModel.buyer = new BuyerModel();
            buyerviewModel.LstProducts = new List<BuyerProductModel>();
           
            buyerviewModel.Installments = new BuyerInstallmentModel();
            buyerviewModel.LstInstallments = new List<BuyerInstallmentModel>();
            buyerviewModel.LstInstallments.AddRange(lstinstallment);
            return PartialView("~/Views/Buyers/_BuyerInstallments.cshtml", buyerviewModel);
        }
        public ActionResult DeleteInstallment(long InstallmentID, long buyerID)
        {
            BuyerViewModel buyerViewModel = new BuyerViewModel();
            List<BuyerInstallmentModel> lstinstallments = _IBuyer.DeleteBuyerInstallment(InstallmentID, buyerID);
            // products
            buyerViewModel.buyer = new BuyerModel();
            buyerViewModel.productInfo = new BuyerProductModel();
            buyerViewModel.LstProducts = new List<BuyerProductModel>();

            // Installments
            buyerViewModel.Installments = new BuyerInstallmentModel();
            buyerViewModel.LstInstallments = new List<BuyerInstallmentModel>();
            buyerViewModel.LstInstallments.AddRange(lstinstallments);
            return PartialView("~/Views/Buyers/_BuyerInstallments.cshtml.cshtml", buyerViewModel);
        }

       
        // GET: Buyers/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BuyerViewModel buyer = _IBuyer.GetBuyerInfo(id);
            if (buyer == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryName", "CategoryName");
            ViewBag.ProductTypeID = new SelectList(db.ProductTypes, "TypeName", "TypeName");
            return View(buyer);
        }

        // POST: Buyers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( BuyerViewModel buyerviewmodel, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    using (MemoryStream target = new MemoryStream())
                    {
                        file.InputStream.CopyTo(target);
                        buyerviewmodel.buyer.Bill = target.ToArray();
                    }

                }
                _IBuyer.AddBuyer(buyerviewmodel.buyer);
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryName", "CategoryName");
            ViewBag.ProductTypeID = new SelectList(db.ProductTypes, "TypeName", "TypeName");
            return View(buyerviewmodel);
        }

        // GET: Buyers/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Buyer buyer = db.Buyers.Find(id);
            if (buyer == null)
            {
                return HttpNotFound();
            }
            return View(buyer);
        }

        // POST: Buyers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Buyer buyer = db.Buyers.Find(id);
            db.Buyers.Remove(buyer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        private BuyerViewModel InitilCreateBulk()
        {
            BuyerViewModel objbuyer = new BuyerViewModel();
            //objbuyer.buyer = new BuyerModel();
            objbuyer.productInfo = new BuyerProductModel();
            objbuyer.LstProducts = new List<BuyerProductModel>();
            objbuyer.Installments = new BuyerInstallmentModel();
            objbuyer.LstInstallments    = new List<BuyerInstallmentModel>();
            return objbuyer;
        }
        public JsonResult GetAllProducts()
        {
            var products = (from prod in db.Products
                            join category in db.Categories on prod.CategoryID equals category.CategoryID
                         select prod.ProductName  + "("+category.CategoryName+ "#" + prod.Type + ")").Distinct().ToList();
            return Json(products, JsonRequestBehavior.AllowGet);
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
