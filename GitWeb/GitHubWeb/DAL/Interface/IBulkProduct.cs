using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Models;
using Web.Models.ViewModel;

namespace DAL.Interface
{
   public interface IBulkProduct
    {
        // customer Product
        void DeleteInitilProducts();
        BulkBuyViewModel GetBulk(long? bulkID);
        void AddBulkBuy(BulkBuyModel ProductModel);
        List<BulkBuyProductsModel> AddProduct(BulkBuyProductsModel  ProductModel);
        List<BulkBuyProductsModel> DeleteProduct(long Id, long bulkBuyID);
         BulkBuyProductsModel GetProductDetails(long Id);
        BulkBuyProduct GetProduct(ShopDevEntities db, long Id);
        // vendors
        List<VendorDetailsModel> AddVendor(VendorDetailsModel ProductModel);
        List<VendorDetailsModel> DeleteVendor(long Id,long bulkBuyID);
        VendorDetailsModel GetVendorDetails(long Id);
        VendorDetail  GetVendor(ShopDevEntities db, long Id);

        // Installment
        List<Installments> AddInstallment(Installments ProductModel);
        List<Installments> DeleteDeleteInstallment(long Id, long bulkBuyID);
        Installments GetInstallmentDetails(long Id);
        BulkBuyInstallment GetInstallment(ShopDevEntities db, long Id);
    }
}
