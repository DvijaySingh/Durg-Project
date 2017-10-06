using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Models;
using Web.Models.ViewModel;

namespace DAL.Interface
{
    public interface IBuyer
    {
        void DeleteInitilProducts();

        List<BuyerModel> AllBuyers(BuyerModel objModel);
        BuyerViewModel GetBuyerInfo(long? bulkID);
        long AddBuyer(BuyerModel buyer);
        List<BuyerProductModel> AddBuyerProduct(BuyerProductModel ProductModel);
        List<BuyerProductModel> DeleteBuyerProduct(long Id, long buyerID);
        BuyerProduct  GetProduct(ShopDevEntities db, long Id);

        // Installment
        List<BuyerInstallmentModel> AddBuyerInstallment(BuyerInstallmentModel ProductModel);
        List<BuyerInstallmentModel> DeleteBuyerInstallment(long Id, long buyerID);
        BuyerInstallment GetBuyerInstallment(ShopDevEntities db, long Id);

    }
}
