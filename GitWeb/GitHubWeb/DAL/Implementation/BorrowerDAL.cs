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
            List<BorrowerInstallmentModel> lstAllinstallments = new List<BorrowerInstallmentModel>();
            using (ShopDevEntities db = new ShopDevEntities())
            {
                try
                {
                    installment.BorrowerID = installment.BorrowerID == null ? 0 : installment.BorrowerID;
                    BorrowerInstallment borrowerinstDetail = null;
                    if (installment.InstallmentID > 0)
                    {
                        borrowerinstDetail = db.BorrowerInstallments.Where(m => m.InstallmentID == installment.InstallmentID).FirstOrDefault();
                    }
                    else
                    {
                        borrowerinstDetail = new BorrowerInstallment();
                    }
                    installment.CopyProperties(borrowerinstDetail);
                    if (borrowerinstDetail.InstallmentID == 0)
                    {
                        db.BorrowerInstallments.Add(borrowerinstDetail);
                    }
                    db.SaveChanges();
                    var lstinstallments = db.BorrowerInstallments.Where(m => m.BorrowerID == borrowerinstDetail.BorrowerID).ToList();
                    foreach (var cusprod in lstinstallments)
                    {
                        BorrowerInstallmentModel objcsproduct = new BorrowerInstallmentModel();
                        cusprod.CopyProperties(objcsproduct);
                        lstAllinstallments.Add(objcsproduct);
                    }
                }
                catch { }
                return lstAllinstallments;
            }
        }

        public List<BorrowerInstallmentModel> DeleteBorrowerInstallment(long Id, long buyerID)
        {
            List<BorrowerInstallmentModel> lstinstallments = new List<BorrowerInstallmentModel>();
            using (ShopDevEntities db = new ShopDevEntities())
            {
                try
                {
                    BorrowerInstallment installment = GetBorrowerInstallment(db, Id);
                    db.BorrowerInstallments.Remove(installment);
                    db.SaveChanges();
                    var lstproducts = db.BorrowerInstallments.Where(m => m.BorrowerID == buyerID).ToList();
                    foreach (var cusprod in lstproducts)
                    {
                        BorrowerInstallmentModel objInstallments = new BorrowerInstallmentModel();
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

       
        public BorrowerInstallment GetBorrowerInstallment(ShopDevEntities db, long Id)
        {
            BorrowerInstallment objinstllment = null;
            try
            {
                objinstllment = db.BorrowerInstallments.Where(m => m.BorrowerID == Id).FirstOrDefault();
            }
            catch (Exception)
            {

            }
            return objinstllment;
        }

       
    }
}
