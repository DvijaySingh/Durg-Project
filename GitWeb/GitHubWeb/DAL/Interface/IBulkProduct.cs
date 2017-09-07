﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Models;

namespace DAL.Interface
{
   public interface IBulkProduct
    {
        // customer Product
        List<BulkBuyProductsModel> AddProduct(BulkBuyProductsModel  ProductModel);
        List<BulkBuyProductsModel> DeleteProduct(long Id, long bulkBuyID);
        BulkBuyProduct GetProduct(ShopDevEntities db, long Id);
        // vendors
        List<VendorModel> AddVendor(VendorModel ProductModel);
        List<VendorModel> DeleteVendor(long Id,long bulkBuyID);
        VendorDetail  GetVendor(ShopDevEntities db, long Id);

        // Installment
        List<Installments> AddInstallment(Installments ProductModel);
        List<Installments> DeleteDeleteInstallment(long Id, long bulkBuyID);
        BulkBuyInstallment GetInstallment(ShopDevEntities db, long Id);
    }
}
