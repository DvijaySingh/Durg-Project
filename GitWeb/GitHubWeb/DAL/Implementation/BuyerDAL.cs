using DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Models;
using Web.Models.ViewModel;

namespace DAL.Implementation
{
    public class BuyerDAL : IBuyer
    {

        public void AddBuyer(BuyerModel buyer)
        {
            using (ShopDevEntities db = new ShopDevEntities())
            {
                try
                {
                    Buyer buyerdb = null;
                    if (buyer.BuyerID == 0)
                    {
                        buyerdb = new Buyer();
                        buyer.CopyProperties(buyerdb);
                        db.Buyers.Add(buyerdb);
                    }
                    else
                    {
                        buyerdb = db.Buyers.Where(m => m.BuyerID == buyer.BuyerID).FirstOrDefault();
                        buyer.CopyProperties(buyerdb);
                    }
                    db.SaveChanges();

                    List<BuyerProduct> lstNewproducts = db.BuyerProducts.Where(m => m.BuyerID == 0).ToList();
                    lstNewproducts.ForEach(m => m.BuyerID = buyerdb.BuyerID);
                    foreach (var product in lstNewproducts)
                    {
                        var oldproduct = db.Products.Where(m => m.ProductName == product.ProductName && m.Type == product.Type && m.CategoryID == product.CategoryID).FirstOrDefault();
                        if (oldproduct != null)
                        {
                            oldproduct.Unit -= product.Units;
                        }
                    }

                    List<BuyerInstallment> lstinstallments = db.BuyerInstallments.Where(m => m.BuyerID == 0).ToList();
                    lstinstallments.ForEach(m => m.BuyerID = buyerdb.BuyerID);
                    db.SaveChanges();
                }
                catch
                {

                }
            }
        }

        #region Buyer Products

        public List<BuyerProductModel> AddBuyerProduct(BuyerProductModel productModel)
        {
            List<BuyerProductModel> lstcsproducts = new List<BuyerProductModel>();
            using (ShopDevEntities db = new ShopDevEntities())
            {
                try
                {
                    productModel.BuyerID = productModel.BuyerID == null ? 0 : productModel.BuyerID;
                    BuyerProduct buyerproduct = null;
                    int? oldUnits = 0;
                    if (productModel.BuyerProductlD > 0)
                    {
                        buyerproduct = db.BuyerProducts.Where(m => m.BuyerProductlD == productModel.BuyerProductlD).FirstOrDefault();
                        oldUnits = buyerproduct.Units == null ? 0 : buyerproduct.Units;
                        var diffUnit = oldUnits - productModel.Units;
                        var product = db.Products.Where(m => m.CategoryID == productModel.CategoryID && m.ProductName == productModel.ProductName && m.CategoryID == productModel.CategoryID).FirstOrDefault();
                        if(product!=null)
                        {
                            product.Unit += diffUnit;
                        }
                    }
                    else
                    {
                        buyerproduct = new BuyerProduct();
                    }
                 

                    productModel.CopyProperties(buyerproduct);
                    if (productModel.BuyerProductlD == 0)
                    {
                        db.BuyerProducts.Add(buyerproduct);

                        if (productModel.BuyerID > 0)
                        {
                            var oldproduct = db.Products.Where(m => m.ProductName == productModel.ProductName && m.Type == productModel.Type && m.CategoryID == productModel.CategoryID).FirstOrDefault();
                            if (oldproduct != null)
                            {
                                oldproduct.Unit -= productModel.Units;
                            }

                        }
                    }
                    db.SaveChanges();
                    var lstproducts = db.BuyerProducts.Where(m => m.BuyerID == productModel.BuyerID).ToList();
                    foreach (var cusprod in lstproducts)
                    {
                        BuyerProductModel objcsproduct = new BuyerProductModel();
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
        public List<BuyerProductModel> DeleteBuyerProduct(long Id, long buyerID)
        {
            List<BuyerProductModel> lstbulkproducts = new List<BuyerProductModel>();
            using (ShopDevEntities db = new ShopDevEntities())
            {
                try
                {
                    BuyerProduct product = GetProduct(db, Id);

                    db.BuyerProducts.Remove(product);
                    db.SaveChanges();
                    var lstproducts = db.BuyerProducts.Where(m => m.BuyerID == buyerID).ToList();
                    foreach (var cusprod in lstproducts)
                    {
                        BuyerProductModel objcsproducts = new BuyerProductModel();
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
        public BuyerProduct GetProduct(ShopDevEntities db, long Id)
        {
            BuyerProduct objProduct = null;
            try
            {
                objProduct = db.BuyerProducts.Where(m => m.BuyerProductlD == Id).FirstOrDefault();
            }
            catch (Exception)
            {

            }
            return objProduct;
        }
        #endregion

        #region Buyer Installment
        public List<BuyerInstallmentModel> AddBuyerInstallment(BuyerInstallmentModel installment)
        {
            List<BuyerInstallmentModel> lstAllinstallments = new List<BuyerInstallmentModel>();
            using (ShopDevEntities db = new ShopDevEntities())
            {
                try
                {
                    installment.BuyerID = installment.BuyerID == null ? 0 : installment.BuyerID;
                    BuyerInstallment buyerinstDetail = null;
                    if (installment.BuyerInstallmentID > 0)
                    {
                        buyerinstDetail = db.BuyerInstallments.Where(m => m.BuyerInstallmentID == installment.BuyerInstallmentID).FirstOrDefault();
                    }
                    else
                    {
                        buyerinstDetail = new BuyerInstallment();
                    }
                    installment.CopyProperties(buyerinstDetail);
                    if (buyerinstDetail.BuyerInstallmentID == 0)
                    {
                        db.BuyerInstallments.Add(buyerinstDetail);
                    }
                    db.SaveChanges();
                    var lstinstallments = db.BuyerInstallments.Where(m => m.BuyerID == buyerinstDetail.BuyerID).ToList();
                    foreach (var cusprod in lstinstallments)
                    {
                        BuyerInstallmentModel objcsproduct = new BuyerInstallmentModel();
                        cusprod.CopyProperties(objcsproduct);
                        lstAllinstallments.Add(objcsproduct);
                    }
                }
                catch { }
                return lstAllinstallments;
            }
        }

        public List<BuyerInstallmentModel> DeleteBuyerInstallment(long Id, long buyerID)
        {
            List<BuyerInstallmentModel> lstinstallments = new List<BuyerInstallmentModel>();
            using (ShopDevEntities db = new ShopDevEntities())
            {
                try
                {
                    BuyerInstallment installment = GetBuyerInstallment(db, Id);
                    db.BuyerInstallments.Remove(installment);
                    db.SaveChanges();
                    var lstproducts = db.BuyerInstallments.Where(m => m.BuyerID == buyerID).ToList();
                    foreach (var cusprod in lstproducts)
                    {
                        BuyerInstallmentModel objInstallments = new BuyerInstallmentModel();
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

        public BuyerInstallment GetBuyerInstallment(ShopDevEntities db, long Id)
        {
            BuyerInstallment objinstllment = null;
            try
            {
                objinstllment = db.BuyerInstallments.Where(m => m.BuyerInstallmentID == Id).FirstOrDefault();
            }
            catch (Exception)
            {

            }
            return objinstllment;
        }

        #endregion

        public void DeleteInitilProducts()
        {
            using (ShopDevEntities db = new ShopDevEntities())
            {
                try
                {
                    var AllInitialproducts = db.BuyerProducts.Where(m => m.BuyerID == 0).ToList();
                    db.BuyerProducts.RemoveRange(AllInitialproducts);
                    var AllInitialInstallments = db.BuyerInstallments.Where(m => m.BuyerID == 0).ToList();
                    db.BuyerInstallments.RemoveRange(AllInitialInstallments);
                    db.SaveChanges();
                }
                catch { }
            }
        }

        public BuyerViewModel GetBuyerInfo(long? buyerId)
        {
            BuyerViewModel buyermodel = new BuyerViewModel();
            using (ShopDevEntities db = new ShopDevEntities())
            {
                try
                {
                    var buyercustomerInfo = db.Buyers.Where(m => m.BuyerID == buyerId).FirstOrDefault();
                    var lstproducts = db.BuyerProducts.Where(m => m.BuyerID == buyerId).ToList();
                    var AllInstallments = db.BuyerInstallments.Where(m => m.BuyerID == buyerId).ToList();

                    BuyerModel bbModel = new BuyerModel();
                    buyercustomerInfo.CopyProperties(bbModel);

                    List<BuyerProductModel> lstcsproducts = new List<BuyerProductModel>();
                    foreach (var cusprod in lstproducts)
                    {
                        BuyerProductModel objcsproduct = new BuyerProductModel();
                        cusprod.CopyProperties(objcsproduct);
                        lstcsproducts.Add(objcsproduct);
                    }

                    List<BuyerInstallmentModel> lstInstallments = new List<BuyerInstallmentModel>();
                    foreach (var cusprod in AllInstallments)
                    {
                        BuyerInstallmentModel objcsproduct = new BuyerInstallmentModel();
                        cusprod.CopyProperties(objcsproduct);
                        lstInstallments.Add(objcsproduct);
                    }
                    buyermodel.buyer = bbModel;
                    buyermodel.productInfo = new BuyerProductModel();
                    buyermodel.LstProducts = new List<BuyerProductModel>();
                    buyermodel.LstProducts.AddRange(lstcsproducts);
                    buyermodel.Installments = new BuyerInstallmentModel();
                    buyermodel.LstInstallments = new List<BuyerInstallmentModel>();
                    buyermodel.LstInstallments.AddRange(lstInstallments);
                }
                catch (Exception) { }
            }
            return buyermodel;
        }
    }
}

