using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Models;
using Web.Models.ViewModel;

namespace DAL.Interface
{
   public interface IBorrower
    {
        void DeleteInitilProducts();
        BorrowerViewModel GetBorrowerInfo(long? sellerID);
        void AddBorrower(BorrowerModel borrower);
       
        // Installment
        List<BorrowerInstallmentModel> AddBorrowerInstallment(BorrowerInstallmentModel installment);
        List<BorrowerInstallmentModel> DeleteBorrowerInstallment(long Id, long buyerID);
        BorrowerInstallment GetBorrowerInstallment(ShopDevEntities db, long Id);
    }
}
