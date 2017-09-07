using DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Models;

namespace DAL.Implementation
{
    public class BulkBuyDAL : IBulkProduct
    {


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
        public List<VendorModel> AddVendor(VendorModel vendorModel)
        {
            List<VendorModel> lstvendors = new List<VendorModel>();
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
                        VendorModel objcsproduct = new VendorModel();
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
        public List<VendorModel> DeleteVendor(long Id,long bulkBuyID)
        {
            List<VendorModel> lstvendors = new List<VendorModel>();
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
                        VendorModel objvendor = new VendorModel();
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
        public List<Installments> AddInstallment(Installments ProductModel)
        {
            throw new NotImplementedException();
        }
        public List<Installments> DeleteDeleteInstallment(long Id,long bulkBuyID)
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

    }
}
