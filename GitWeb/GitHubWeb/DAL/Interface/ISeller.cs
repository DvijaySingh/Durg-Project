using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Models;
using Web.Models.ViewModel;

namespace DAL.Interface
{
    public interface ISeller
    {
        void DeleteInitilProducts();
        SellerViewModel GetSellerInfo(long? sellerID);
        void AddSeller(SellerModel seller);
        List<SellerProductModel> AddSellerProduct(SellerProductModel ProductModel);
        List<SellerProductModel> DeleteSellerProduct(long Id, long buyerID);
        SellerProduct GetProduct(ShopDevEntities db, long Id);

        // Installment
        List<SellerInstallmentModel> AddSellerInstallment(SellerInstallmentModel installment);
        List<SellerInstallmentModel> DeleteSellerInstallment(long Id, long buyerID);
        SellerInstallment GetSellerInstallment(ShopDevEntities db, long Id);

        SellerSearchViewModel SearchSeller(string name);
    }
}
