﻿using System;
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
    public class OrdersController : Controller
    {
        private ShopDevEntities db = new ShopDevEntities();
        private IOrder _IOrder;
        public OrdersController(IOrder iorder)
        {
            this._IOrder = iorder;
        }
        // GET: Orders
        public ActionResult Index()
        {
            return View(db.Orders.ToList());
        }
        public ActionResult Add()
        {
            OrderModel objOrderModel = new OrderModel();
            objOrderModel.lstcusProduct = new List<CustomerProductModel>();
            return View(objOrderModel);
        }
        
        public JsonResult AllProducts()
        {
            return Json(db.Products.Select(m=> m.ProductName  + "(" + m.ProductID + ")").ToList(), JsonRequestBehavior.AllowGet);
        }
        // GET: Orders/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            OrderModel objOrderModel = new OrderModel();
            objOrderModel.cusProduct = new CustomerProductModel();
            objOrderModel.lstcusProduct = new List<CustomerProductModel>();
            //var data = new CustomerProductModel { ActualWeight = 10, AppxWeight = 20, ProductName = "Test", IsActive = true, Description = "Test" };
            //objOrderModel.lstcusProduct.Add(data);
            return View(objOrderModel);
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( OrderModel order)
        {
            if (ModelState.IsValid)
            {
                //db.Orders.Add(order);
                //db.SaveChanges();
                //return RedirectToAction("Index");
            }

            return View(order);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderId,CustomerName,CustomerAddress,BookingAmount,OrderDate,DeliveryDate,IsDelevered")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        
        [HttpPost]
        public ActionResult AddOrderProduct(CustomerProductModel objOrderProduct)
        {
            OrderModel objOrderModel = new OrderModel();
            if ((OrderModel)TempData["cusproduct"] != null)
            {
                objOrderModel = (OrderModel)TempData["cusproduct"];
            }
            List<CustomerProductModel> objCustomerProductModel = new List<CustomerProductModel>();
            objCustomerProductModel.Add(objOrderProduct);
            objOrderModel.lstcusProduct = objCustomerProductModel;
            TempData["cusproduct"] = objOrderModel;

           return Json(objOrderModel,JsonRequestBehavior.AllowGet);
            //return PartialView("~/Views/Shared/_AddCustomerProduct.cshtml", objOrderModel);
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
