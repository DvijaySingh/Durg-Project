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
    public class BorrowerDAL : IBorrower
    {
        public void DeleteInitilProducts()
        {
            using (ShopDevEntities db = new ShopDevEntities())
            {
                try
                {
                     
                    var AllInitialInstallments = db.BorrowerInstallments.Where(m => m.BorrowerID == 0).ToList();
                    db.BorrowerInstallments.RemoveRange(AllInitialInstallments);
                    db.SaveChanges();
                }
                catch { }
            }
        }

        public void AddBorrower(BorrowerModel borrower)
        {
            using (ShopDevEntities db = new ShopDevEntities())
            {
                try
                {
                    Borrower borrowerdb = null;
                    if (borrower.BorrowID == 0)
                    {
                        borrowerdb = new Borrower();
                        borrower.CopyProperties(borrowerdb);
                        db.Borrowers.Add(borrowerdb);
                    }
                    else
                    {
                        borrowerdb = db.Borrowers.Where(m => m.BorrowID == borrower.BorrowID).FirstOrDefault();
                        borrower.CopyProperties(borrowerdb);
                    }
                    db.SaveChanges();

                    List<BorrowerInstallment> lstinstallments = db.BorrowerInstallments.Where(m => m.BorrowerID == 0).ToList();
                    lstinstallments.ForEach(m => m.BorrowerID = borrowerdb.BorrowID);
                    db.SaveChanges();
                }
                catch
                {

                }
            }
        }
        public BorrowerViewModel GetBorrowerInfo(long? borrowerID)
        {
            BorrowerViewModel borrowermodel = new BorrowerViewModel();
            using (ShopDevEntities db = new ShopDevEntities())
            {
                try
                {
                    var sellerInfo = db.Borrowers.Where(m => m.BorrowID == borrowerID).FirstOrDefault();
                   
                    var AllInstallments = db.BorrowerInstallments.Where(m => m.BorrowerID == borrowerID).ToList();

                    BorrowerModel bbModel = new BorrowerModel();
                    sellerInfo.CopyProperties(bbModel);

                    
                    List<BorrowerInstallmentModel> lstInstallments = new List<BorrowerInstallmentModel>();
                    foreach (var cusprod in AllInstallments)
                    {
                        BorrowerInstallmentModel objcsproduct = new BorrowerInstallmentModel();
                        cusprod.CopyProperties(objcsproduct);
                        lstInstallments.Add(objcsproduct);
                    }
                    borrowermodel.Borrower = bbModel;
                    borrowermodel.Installments = new BorrowerInstallmentModel();
                    borrowermodel.Lstinstallments = new List<BorrowerInstallmentModel>();
                    borrowermodel.Lstinstallments.AddRange(lstInstallments);
                }
                catch (Exception) { }
            }
            return borrowermodel;
        }
        public List<BorrowerInstallmentModel> AddBorrowerInstallment(BorrowerInstallmentModel installment)
        {
            throw new NotImplementedException();
        }

        public List<BorrowerInstallmentModel> DeleteBorrowerInstallment(long Id, long buyerID)
        {
            throw new NotImplementedException();
        }

       
        public BorrowerInstallment GetBorrowerInstallment(ShopDevEntities db, long Id)
        {
            throw new NotImplementedException();
        }

       
    }
}
