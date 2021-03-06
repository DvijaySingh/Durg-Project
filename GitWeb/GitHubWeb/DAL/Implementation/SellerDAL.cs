﻿using DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Models;
using Web.Models.ViewModel;

namespace DAL.Implementation
{
    public class SellerDAL : ISeller
    {
        public void AddSeller(SellerModel seller)
        {
            using (ShopDevEntities db = new ShopDevEntities())
            {
                try
                {
                    Seller sellerdb = null;
                    if (seller.SellerID == 0)
                    {
                        sellerdb = new Seller();
                        seller.CopyProperties(sellerdb);
                        db.Sellers.Add(sellerdb);
                    }
                    else
                    {
                        sellerdb = db.Sellers.Where(m => m.SellerID == seller.SellerID).FirstOrDefault();
                        seller.CopyProperties(sellerdb);
                    }
                    db.SaveChanges();

                    List<SellerProduct> lstNewproducts = db.SellerProducts.Where(m => m.SellerID == 0).ToList();
                    lstNewproducts.ForEach(m => m.SellerID = sellerdb.SellerID);
                    foreach(var product in lstNewproducts)
                    {
                        
                        var oldproduct = db.Products.Where(m => m.ProductName == product.ProductName && m.Type == product.Type && m.CategoryID == product.CategoryID).FirstOrDefault();
                        if(oldproduct!=null)
                        {
                            oldproduct.Unit += product.Unit;
                        }
                        else
                        {
                            Product prod = new Product();
                            prod.ProductName = product.ProductName;
                            prod.ProdDesc = product.Description;
                            prod.Type = product.Type;
                            prod.CategoryID = product.CategoryID;
                            prod.Unit = product.Unit;
                            prod.IsActive = true;
                            db.Products.Add(prod);
                        }
                    }

                    List<SellerInstallment> lstinstallments = db.SellerInstallments.Where(m => m.SellerID == 0).ToList();
                    lstinstallments.ForEach(m => m.SellerID = sellerdb.SellerID);
                    db.SaveChanges();
                }
                catch
                {

                }
            }
        }

        public SellerViewModel GetSellerInfo(long? sellerID)
        {
            SellerViewModel sellermodel = new SellerViewModel();
            using (ShopDevEntities db = new ShopDevEntities())
            {
                try
                {
                    var sellerInfo = db.Sellers.Where(m => m.SellerID == sellerID).FirstOrDefault();
                    var lstproducts = db.SellerProducts.Where(m => m.SellerID == sellerID).ToList();
                    var AllInstallments = db.SellerInstallments.Where(m => m.SellerID == sellerID).ToList();

                    SellerModel bbModel = new SellerModel();
                    sellerInfo.CopyProperties(bbModel);

                    List<SellerProductModel> lstcsproducts = new List<SellerProductModel>();
                    foreach (var cusprod in lstproducts)
                    {
                        SellerProductModel objcsproduct = new SellerProductModel();
                        cusprod.CopyProperties(objcsproduct);
                        lstcsproducts.Add(objcsproduct);
                    }

                    List<SellerInstallmentModel> lstInstallments = new List<SellerInstallmentModel>();
                    foreach (var cusprod in AllInstallments)
                    {
                        SellerInstallmentModel objcsproduct = new SellerInstallmentModel();
                        cusprod.CopyProperties(objcsproduct);
                        lstInstallments.Add(objcsproduct);
                    }
                    sellermodel.sellerInfor = bbModel;
                    sellermodel.productInfo = new SellerProductModel();
                    sellermodel.Lstproducts = new List<SellerProductModel>();
                    sellermodel.Lstproducts.AddRange(lstcsproducts);
                    sellermodel.Installments = new SellerInstallmentModel();
                    sellermodel.LstInstallments = new List<SellerInstallmentModel>();
                    sellermodel.LstInstallments.AddRange(lstInstallments);
                }
                catch (Exception) { }
            }
            return sellermodel;
        }
        public void DeleteInitilProducts()
        {
            using (ShopDevEntities db = new ShopDevEntities())
            {
                try
                {
                    var AllInitialproducts = db.SellerProducts.Where(m => m.SellerID == 0).ToList();
                    db.SellerProducts.RemoveRange(AllInitialproducts);
                    var AllInitialInstallments = db.SellerInstallments.Where(m => m.SellerID == 0).ToList();
                    db.SellerInstallments.RemoveRange(AllInitialInstallments);
                    db.SaveChanges();
                }
                catch { }
            }
        }

        #region Seller Products
        public List<SellerProductModel> AddSellerProduct(SellerProductModel productModel)
        {
            List<SellerProductModel> lstcsproducts = new List<SellerProductModel>();
            using (ShopDevEntities db = new ShopDevEntities())
            {
                try
                {
                    productModel.SellerID = productModel.SellerID == null ? 0 : productModel.SellerID;
                    SellerProduct buyerproduct = null;
                    int? oldunit = 0;
                    if (productModel.SellerProductID > 0)
                    {
                        buyerproduct = db.SellerProducts.Where(m => m.SellerProductID == productModel.SellerProductID).FirstOrDefault();
                        oldunit = buyerproduct.Unit==null?0: buyerproduct.Unit;
                        var diffUnit = oldunit - productModel.Unit;
                        var product = db.Products.Where(m => m.CategoryID == productModel.CategoryID && m.ProductName == productModel.ProductName && m.CategoryID == productModel.CategoryID).FirstOrDefault();
                        if (product != null)
                        {
                            product.Unit += diffUnit;
                        }
                    }
                    else
                    {
                        buyerproduct = new SellerProduct();
                    }
                    productModel.CopyProperties(buyerproduct);
                    if (productModel.SellerProductID == 0)
                    {
                        db.SellerProducts.Add(buyerproduct);


                        if (productModel.SellerID > 0)
                        {
                            var oldproduct = db.Products.Where(m => m.ProductName == productModel.ProductName && m.Type == productModel.Type && m.CategoryID == productModel.CategoryID).FirstOrDefault();
                            if (oldproduct != null)
                            {
                                oldproduct.Unit += productModel.Unit;
                            }
                            else
                            {
                                Product prod = new Product();
                                prod.ProductName = productModel.ProductName;
                                prod.ProdDesc = productModel.Description;
                                prod.Type = productModel.Type;
                                prod.CategoryID = productModel.CategoryID;
                                prod.Unit = productModel.Unit;
                                prod.IsActive = true;
                                db.Products.Add(prod);
                            }
                        }
                    }
                    db.SaveChanges();
                    var lstproducts = db.SellerProducts.Where(m => m.SellerID == productModel.SellerID).ToList();
                    foreach (var cusprod in lstproducts)
                    {
                        SellerProductModel objcsproduct = new SellerProductModel();
                        cusprod.CopyProperties(objcsproduct);
                        lstcsproducts.Add(objcsproduct);
                    }
                    return lstcsproducts;
                }
                catch (Exception)
                {
                    return lstcsproducts;
                }
            }
        }
        public List<SellerProductModel> DeleteSellerProduct(long Id, long sellerID)
        {
            List<SellerProductModel> lstbulkproducts = new List<SellerProductModel>();
            using (ShopDevEntities db = new ShopDevEntities())
            {
                try
                {
                    SellerProduct product = GetProduct(db, Id);

                    db.SellerProducts.Remove(product);
                    db.SaveChanges();
                    var lstproducts = db.SellerProducts.Where(m => m.SellerID == sellerID).ToList();
                    foreach (var cusprod in lstproducts)
                    {
                        SellerProductModel objcsproducts = new SellerProductModel();
                        cusprod.CopyProperties(objcsproducts);
                        lstbulkproducts.Add(objcsproducts);
                    }

                }
                catch (Exception)
                {

                }
                return lstbulkproducts;
            }
        }
        public SellerProduct GetProduct(ShopDevEntities db, long Id)
        {
            SellerProduct objProduct = null;
            try
            {
                objProduct = db.SellerProducts.Where(m => m.SellerProductID == Id).FirstOrDefault();
            }
            catch (Exception)
            {

            }
            return objProduct;
        }

        #endregion

        #region SellerInstallment
        public List<SellerInstallmentModel> AddSellerInstallment(SellerInstallmentModel installment)
        {
            List<SellerInstallmentModel> lstAllinstallments = new List<SellerInstallmentModel>();
            using (ShopDevEntities db = new ShopDevEntities())
            {
                try
                {
                    installment.SellerID = installment.SellerID == null ? 0 : installment.SellerID;
                    SellerInstallment sellerinstDetail = null;
                    if (installment.SellerInstallmentID > 0)
                    {
                        sellerinstDetail = db.SellerInstallments.Where(m => m.SellerInstallmentID == installment.SellerInstallmentID).FirstOrDefault();
                    }
                    else
                    {
                        sellerinstDetail = new SellerInstallment();
                    }
                    installment.CopyProperties(sellerinstDetail);
                    if (sellerinstDetail.SellerInstallmentID == 0)
                    {
                        db.SellerInstallments.Add(sellerinstDetail);
                    }
                    db.SaveChanges();
                    var lstinstallments = db.SellerInstallments.Where(m => m.SellerID == installment.SellerID).ToList();
                    foreach (var cusprod in lstinstallments)
                    {
                        SellerInstallmentModel objcsproduct = new SellerInstallmentModel();
                        cusprod.CopyProperties(objcsproduct);
                        lstAllinstallments.Add(objcsproduct);
                    }
                }
                catch { }
                return lstAllinstallments;
            }
        }
        public List<SellerInstallmentModel> DeleteSellerInstallment(long Id, long sellerID)
        {
            List<SellerInstallmentModel> lstinstallments = new List<SellerInstallmentModel>();
            using (ShopDevEntities db = new ShopDevEntities())
            {
                try
                {
                    SellerInstallment installment = GetSellerInstallment(db, Id);
                    db.SellerInstallments.Remove(installment);
                    db.SaveChanges();
                    var lstproducts = db.SellerInstallments.Where(m => m.SellerID == sellerID).ToList();
                    foreach (var cusprod in lstproducts)
                    {
                        SellerInstallmentModel objInstallments = new SellerInstallmentModel();
                        cusprod.CopyProperties(objInstallments);
                        lstinstallments.Add(objInstallments);
                    }
                }
                catch (Exception)
                {

                }
                return lstinstallments;
            }
        }
        public SellerInstallment GetSellerInstallment(ShopDevEntities db, long Id)
        {
            SellerInstallment objinstllment = null;
            try
            {
                objinstllment = db.SellerInstallments.Where(m => m.SellerInstallmentID == Id).FirstOrDefault();
            }
            catch (Exception)
            {

            }
            return objinstllment;
        }



        #endregion
        public SellerSearchViewModel SearchSeller(string name)
        {
            SellerSearchViewModel searchRes = new SellerSearchViewModel();
            List<SellerModel> lstsellers = new List<SellerModel>();
            using (ShopDevEntities db = new ShopDevEntities())
            {
                try
                {

                    var res = from seller in db.Sellers
                              where seller.SellerName.Contains(name) || string.IsNullOrEmpty(name)
                              orderby seller.BuyDate descending
                              select seller;
                    foreach (var seller in res)
                    {
                        SellerModel sellerModel = new SellerModel();
                        seller.CopyProperties(sellerModel);
                        lstsellers.Add(sellerModel);
                    }

                }
                catch (Exception ex)
                {

                }
                searchRes.lstseller = lstsellers;
                searchRes.seller = new SellerModel();
                return searchRes;
            }
        }

    }
}
