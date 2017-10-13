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
using Web.Models;
using Web.Models.ViewModel;
using WebApplication1.Filter;

namespace WebApplication1.Controllers
{
    [AuthFilter]
    public class BorrowersController : Controller
    {
        private ShopDevEntities db = new ShopDevEntities();
        private IBorrower _IBorrower;
        public BorrowersController(IBorrower iBorrower)
        {
            this._IBorrower = iBorrower;
        }
        // GET: Borrowers
        //public ActionResult Index()
        //{
        //    return View(db.Borrowers.ToList());
        //}
        public ActionResult Index()
        {
            BorrowerSearchViewModel vModel = new BorrowerSearchViewModel();
            vModel.Borrower = new BorrowerModel();
            vModel.AllBorrowers = new List<BorrowerModel>();
            return View(vModel);
            // return View(db.Buyers.ToList());
        }
        public ActionResult Search(BorrowerSearchViewModel objModel)
        {
            BorrowerSearchViewModel vModel = new BorrowerSearchViewModel();
            vModel.Borrower = new BorrowerModel();
            vModel.AllBorrowers = new List<BorrowerModel>();
            List<BorrowerModel> objRes = _IBorrower.AllBorrowers(objModel.Borrower);
            vModel.AllBorrowers.AddRange(objRes);
            return PartialView("~/Views/Borrowers/_BorrowerSearch.cshtml", vModel);
        }
        // GET: Borrowers/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BorrowerViewModel borrower = _IBorrower.GetBorrowerInfo(id);
            if (borrower == null)
            {
                return HttpNotFound();
            }
            return View(borrower);
        }

        // GET: Borrowers/Create
        public ActionResult Create()
        {
            _IBorrower.DeleteInitilProducts();
            var objbuyer = InitilCreateBulk();
            return View(objbuyer);
        }

        // POST: Borrowers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BorrowerViewModel borrowerviwmodel)
        {
            if (ModelState.IsValid)
            {
                _IBorrower.AddBorrower(borrowerviwmodel.Borrower);
               TempData["Message"] = "Borrower Save Successfully !";
                return RedirectToAction("Index");
            }
            return View(borrowerviwmodel);
        }
        public ActionResult AddInstallment(BorrowerViewModel borrowerviewModel)
        {
            List<BorrowerInstallmentModel> lstinstallment = _IBorrower.AddBorrowerInstallment(borrowerviewModel.Installments);
            borrowerviewModel.Borrower = new BorrowerModel();
            borrowerviewModel.Installments = new BorrowerInstallmentModel();
            borrowerviewModel.Lstinstallments = new List<BorrowerInstallmentModel>();
            borrowerviewModel.Lstinstallments.AddRange(lstinstallment);
            TempData["Message"] = "Installment Save Successfully !";
            return PartialView("~/Views/Borrowers/_BorrowerInstallment.cshtml", borrowerviewModel);
        }
        public ActionResult DeleteInstallment(long InstallmentID, long BorrowerID)
        {
            BorrowerViewModel borrowerviewModel = new BorrowerViewModel();
            List<BorrowerInstallmentModel> lstinstallments = _IBorrower.DeleteBorrowerInstallment(InstallmentID, BorrowerID);
            // products
            borrowerviewModel.Borrower = new BorrowerModel();

            // Installments
            borrowerviewModel.Installments = new BorrowerInstallmentModel();
            borrowerviewModel.Lstinstallments = new List<BorrowerInstallmentModel>();
            borrowerviewModel.Lstinstallments.AddRange(lstinstallments);
            TempData["Message"] = "Installment delete Successfully !";
            return PartialView("~/Views/Borrowers/_BorrowerInstallment.cshtml", borrowerviewModel);
        }
        // GET: Borrowers/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BorrowerViewModel borrower = _IBorrower.GetBorrowerInfo(id);
            if (borrower == null)
            {
                return HttpNotFound();
            }
            return View(borrower);
        }

        // POST: Borrowers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(  BorrowerViewModel objborrower)
        {
            if (ModelState.IsValid)
            {
                _IBorrower.AddBorrower(objborrower.Borrower);
                TempData["Message"] = "Borrower update Successfully !";
                return RedirectToAction("Index");
            }
            return View(objborrower);
        }

        // GET: Borrowers/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Borrower borrower = db.Borrowers.Find(id);
            if (borrower == null)
            {
                return HttpNotFound();
            }
            return View(borrower);
        }

        // POST: Borrowers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Borrower borrower = db.Borrowers.Find(id);
            db.Borrowers.Remove(borrower);
            db.SaveChanges();
            TempData["Message"] = "Borrower delete Successfully !";
            return RedirectToAction("Index");
        }
        private BorrowerViewModel InitilCreateBulk()
        {
            BorrowerViewModel objborrower = new BorrowerViewModel();
            //objbuyer.buyer = new BuyerModel();
            objborrower.Borrower = new BorrowerModel();
            objborrower.Installments = new BorrowerInstallmentModel();
            objborrower.Lstinstallments = new List<BorrowerInstallmentModel>();
            return objborrower;
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
