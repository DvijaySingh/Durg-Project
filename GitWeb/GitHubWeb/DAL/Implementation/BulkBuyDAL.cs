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
    public class BulkBuyDAL : IBulkProduct
    {

        public void AddBulkBuy(BulkBuyModel BulkInfo)
        {
            using (ShopDevEntities db = new ShopDevEntities())
            {
                try
                {
                    BulkBuy bulkbuy = null;
                    if (BulkInfo.BulkBuyID == 0)
                    {
                        bulkbuy = new BulkBuy();
                        BulkInfo.CopyProperties(bulkbuy);
                        db.BulkBuys.Add(bulkbuy);
                    }
                    else
                    {
                        bulkbuy = db.BulkBuys.Where(m => m.BulkBuyID == BulkInfo.BulkBuyID).FirstOrDefault();
                        BulkInfo.CopyProperties(bulkbuy);
                    }
                    db.SaveChanges();

                    List<BulkBuyProduct> lstNewproducts = db.BulkBuyProducts.Where(m => m.BulkBuyID == 0).ToList();
                    lstNewproducts.ForEach(m => m.BulkBuyID = bulkbuy.BulkBuyID);
                    List<VendorDetail> lstvendors = db.VendorDetails.Where(m => m.BulkByID == 0).ToList();
                    lstvendors.ForEach(m => m.BulkByID = bulkbuy.BulkBuyID);

                    List<BulkBuyInstallment> lstinstallments = db.BulkBuyInstallments.Where(m => m.BulkBuyID == 0).ToList();
                    lstinstallments.ForEach(m => m.BulkBuyID = bulkbuy.BulkBuyID);
                    db.SaveChanges();
                }
                catch
                {

                }
            }
        }

        public List<BulkBuyProductsModel> AddProduct(BulkBuyProductsModel productModel)
        {
            List<BulkBuyProductsModel> lstcsproducts = new List<BulkBuyProductsModel>();
            using (ShopDevEntities db = new ShopDevEntities())
            {
                try
                {


                    productModel.BulkBuyID = productModel.BulkBuyID == null ? 0 : productModel.BulkBuyID;
                    BulkBuyProduct bulkproduct = null;
                    if (productModel.BulkBuyProductID > 0)
                    {
                        bulkproduct = db.BulkBuyProducts.Where(m => m.BulkBuyProductID == productModel.BulkBuyProductID).FirstOrDefault();
                    }
                    else
                    {
                        bulkproduct = new BulkBuyProduct();
                    }
                    productModel.CopyProperties(bulkproduct);
                    if (productModel.BulkBuyProductID == 0)
                    {
                        db.BulkBuyProducts.Add(bulkproduct);
                    }
                    db.SaveChanges();
                    var lstproducts = db.BulkBuyProducts.Where(m => m.BulkBuyID == productModel.BulkBuyID).ToList();
                    foreach (var cusprod in lstproducts)
                    {
                        BulkBuyProductsModel objcsproduct = new BulkBuyProductsModel();
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


        public BulkBuyProduct GetProduct(ShopDevEntities db, long Id)
        {
            BulkBuyProduct objProduct = null;
            try
            {
                objProduct = db.BulkBuyProducts.Where(m => m.BulkBuyProductID == Id).FirstOrDefault();
            }
            catch (Exception)
            {

            }
            return objProduct;
        }

        public List<BulkBuyProductsModel> DeleteProduct(long Id, long bulkBuyID)
        {
            List<BulkBuyProductsModel> lstbulkproducts = new List<BulkBuyProductsModel>();
            using (ShopDevEntities db = new ShopDevEntities())
            {
                try
                {
                    BulkBuyProduct product = GetProduct(db, Id);

                    db.BulkBuyProducts.Remove(product);
                    db.SaveChanges();
                    var lstproducts = db.BulkBuyProducts.Where(m => m.BulkBuyID == bulkBuyID).ToList();
                    foreach (var cusprod in lstproducts)
                    {
                        BulkBuyProductsModel objcsproducts = new BulkBuyProductsModel();
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

        // vendor
        public List<VendorDetailsModel> AddVendor(VendorDetailsModel vendorModel)
        {
            List<VendorDetailsModel> lstvendors = new List<VendorDetailsModel>();
            using (ShopDevEntities db = new ShopDevEntities())
            {
                try
                {


                    vendorModel.BulkByID = vendorModel.BulkByID == null ? 0 : vendorModel.BulkByID;
                    VendorDetail vendordetails = null;
                    if (vendorModel.BuyVendorID > 0)
                    {
                        vendordetails = db.VendorDetails.Where(m => m.BuyVendorID == vendorModel.BuyVendorID).FirstOrDefault();
                    }
                    else
                    {
                        vendordetails = new VendorDetail();
                    }
                    vendorModel.CopyProperties(vendordetails);
                    if (vendorModel.BuyVendorID == 0)
                    {
                        db.VendorDetails.Add(vendordetails);
                    }
                    db.SaveChanges();
                    var lstAllvendors = db.VendorDetails.Where(m => m.BulkByID == vendorModel.BulkByID).ToList();
                    foreach (var cusprod in lstAllvendors)
                    {
                        VendorDetailsModel objcsproduct = new VendorDetailsModel();
                        cusprod.CopyProperties(objcsproduct);
                        lstvendors.Add(objcsproduct);
                    }
                    return lstvendors;
                }
                catch (Exception)
                {
                    return lstvendors;
                }
            }
        }
        public VendorDetail GetVendor(ShopDevEntities db, long Id)
        {
            VendorDetail objvendor = null;
            try
            {
                objvendor = db.VendorDetails.Where(m => m.BuyVendorID == Id).FirstOrDefault();
            }
            catch (Exception)
            {

            }
            return objvendor;
        }
        public List<VendorDetailsModel> DeleteVendor(long Id, long bulkBuyID)
        {
            List<VendorDetailsModel> lstvendors = new List<VendorDetailsModel>();
            using (ShopDevEntities db = new ShopDevEntities())
            {
                try
                {
                    VendorDetail vendor = GetVendor(db, Id);

                    db.VendorDetails.Remove(vendor);
                    db.SaveChanges();
                    var lstproducts = db.VendorDetails.Where(m => m.BulkByID == bulkBuyID).ToList();
                    foreach (var cusprod in lstproducts)
                    {
                        VendorDetailsModel objvendor = new VendorDetailsModel();
                        cusprod.CopyProperties(objvendor);
                        lstvendors.Add(objvendor);
                    }

                }
                catch (Exception)
                {

                }
                return lstvendors;
            }
        }

        // installments
        public List<Installments> AddInstallment(Installments installment)
        {
            List<Installments> lstAllinstallments = new List<Installments>();
            using (ShopDevEntities db = new ShopDevEntities())
            {
                try
                {
                    installment.BulkBuyID = installment.BulkBuyID == null ? 0 : installment.BulkBuyID;
                    BulkBuyInstallment bulkinstDetail = null;
                    if (installment.InstallmentID > 0)
                    {
                        bulkinstDetail = db.BulkBuyInstallments.Where(m => m.InstallmentID == installment.InstallmentID).FirstOrDefault();
                    }
                    else
                    {
                        bulkinstDetail = new BulkBuyInstallment();
                    }
                    installment.CopyProperties(bulkinstDetail);
                    if (installment.InstallmentID == 0)
                    {
                        db.BulkBuyInstallments.Add(bulkinstDetail);
                    }
                    db.SaveChanges();
                    var lstinstallments = db.BulkBuyInstallments.Where(m => m.BulkBuyID == installment.BulkBuyID).ToList();
                    foreach (var cusprod in lstinstallments)
                    {
                        Installments objcsproduct = new Installments();
                        cusprod.CopyProperties(objcsproduct);
                        lstAllinstallments.Add(objcsproduct);
                    }
                }
                catch { }
                return lstAllinstallments;
            }
        }
        public List<Installments> DeleteDeleteInstallment(long Id, long bulkBuyID)
        {
            List<Installments> lstinstallments = new List<Installments>();
            using (ShopDevEntities db = new ShopDevEntities())
            {
                try
                {
                    BulkBuyInstallment installment = GetInstallment(db, Id);
                    db.BulkBuyInstallments.Remove(installment);
                    db.SaveChanges();
                    var lstproducts = db.BulkBuyInstallments.Where(m => m.BulkBuyID == bulkBuyID).ToList();
                    foreach (var cusprod in lstproducts)
                    {
                        Installments objInstallments = new Installments();
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
        public BulkBuyInstallment GetInstallment(ShopDevEntities db, long Id)
        {
            BulkBuyInstallment objinstllment = null;
            try
            {
                objinstllment = db.BulkBuyInstallments.Where(m => m.InstallmentID == Id).FirstOrDefault();
            }
            catch (Exception)
            {

            }
            return objinstllment;
        }

        public void DeleteInitilProducts()
        {
            using (ShopDevEntities db = new ShopDevEntities())
            {
                try
                {
                    var AllInitialVendors = db.VendorDetails.Where(m => m.BulkByID == 0).ToList();
                    db.VendorDetails.RemoveRange(AllInitialVendors);
                    var AllInitialproducts = db.BulkBuyProducts.Where(m => m.BulkBuyID == 0).ToList();
                    db.BulkBuyProducts.RemoveRange(AllInitialproducts);
                    var AllInitialInstallments = db.BulkBuyInstallments.Where(m => m.BulkBuyID == 0).ToList();
                    db.BulkBuyInstallments.RemoveRange(AllInitialInstallments);
                    db.SaveChanges();
                }
                catch { }
            }
        }

        public BulkBuyViewModel GetBulk(long? bulkID)
        {
            BulkBuyViewModel bulkmodel = new BulkBuyViewModel();
            using (ShopDevEntities db = new ShopDevEntities())
            {
                try
                {
                    var bulkBuycustomerInfo = db.BulkBuys.Where(m => m.BulkBuyID == bulkID).FirstOrDefault();
                    var lstproducts = db.BulkBuyProducts.Where(m => m.BulkBuyID == bulkID).ToList();
                    var AllInstallments = db.BulkBuyInstallments.Where(m => m.BulkBuyID == bulkID).ToList();
                    var Allvendors = db.VendorDetails.Where(m => m.BulkByID == bulkID).ToList();

                    BulkBuyModel bbModel = new BulkBuyModel();
                    bulkBuycustomerInfo.CopyProperties(bbModel);

                    List<BulkBuyProductsModel> lstcsproducts = new List<BulkBuyProductsModel>();
                    foreach (var cusprod in lstproducts)
                    {
                        BulkBuyProductsModel objcsproduct = new BulkBuyProductsModel();
                        cusprod.CopyProperties(objcsproduct);
                        lstcsproducts.Add(objcsproduct);
                    }
                    List<VendorDetailsModel> lstvendors = new List<VendorDetailsModel>();
                    foreach (var cusprod in Allvendors)
                    {
                        VendorDetailsModel objcsproduct = new VendorDetailsModel();
                        cusprod.CopyProperties(objcsproduct);
                        lstvendors.Add(objcsproduct);
                    }

                    List<Installments> lstInstallments = new List<Installments>();
                    foreach (var cusprod in AllInstallments)
                    {
                        Installments objcsproduct = new Installments();
                        cusprod.CopyProperties(objcsproduct);
                        lstInstallments.Add(objcsproduct);
                    }

                    bulkmodel.bulkBuyModel = bbModel;
                    bulkmodel.Products = new BulkBuyProductsModel();
                    bulkmodel.lstbulkBuyProducts = new List<BulkBuyProductsModel>();
                    bulkmodel.lstbulkBuyProducts.AddRange(lstcsproducts);

                    bulkmodel.Vendors = new VendorDetailsModel();
                    bulkmodel.lstVendors = new List<VendorDetailsModel>();
                    bulkmodel.lstVendors.AddRange(lstvendors);

                    bulkmodel.installments = new Installments();
                    bulkmodel.lstinstallments = new List<Installments>();
                    bulkmodel.lstinstallments.AddRange(lstInstallments);

                   

                }
                catch (Exception) { }
            }
            return bulkmodel;
        }
    }
}
