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
                    if (borrower.CustCode == null || borrower.CustCode==0)
                    {
                        Customer customer = new Customer
                        {
                            CustmerName = borrower.Name,
                            Address = borrower.Address
                        };
                        db.Customers.Add(customer);
                        db.SaveChanges();
                        borrower.CustCode = customer.CustCode;
                    }
                    var todayYear = DateTime.Now.Year;
                    var CurrentMonth = DateTime.Now.Month;
                    // calculation for interst
                    if (borrower.InterestRate != null && borrower.InterestRate != 0)
                    {
                        var buyMonth = borrower.Date.Value.Month;
                        var buyYear = borrower.Date.Value.Year;
                        var monthDiff = ((todayYear - buyYear) * 12) + CurrentMonth - buyMonth;
                        decimal? interst = 0;
                        if (borrower.InterstableAmount > 0)
                        {
                            interst = borrower.InterstableAmount * borrower.InterestRate * monthDiff / 100;
                        }
                        else
                        {
                            borrower.InterstableAmount = borrower.Amont;
                            interst = borrower.Amont * borrower.InterestRate * monthDiff / 100;
                        }
                        borrower.Interest = interst;
                        borrower.Outstanding = borrower.Amont + interst;
                    }
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

                    List<BorrowerInstallment> lstinstallments = db.BorrowerInstallments.Where(m => m.BorrowerID == borrowerdb.BorrowID).ToList();
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
                        var borrower = db.Borrowers.Where(m => m.BorrowID == installment.BorrowerID).FirstOrDefault();
                        if(borrower.Amont>0)
                        {
                            if (borrower.Interest != null && borrower.Interest != 0)
                            {
                                if (installment.Amount > borrower.Interest)
                                {
                                    var interest = borrower.Interest;
                                    borrower.Outstanding -= installment.Amount - interest;
                                    borrower.Interest = 0;
                                    var interstableAmount = borrower.InterstableAmount;
                                    borrower.InterstableAmount= interstableAmount==null? installment.Amount - interest: interstableAmount-(installment.Amount - interest);
                                    borrowerinstDetail.Description = "Amount cut for Interset" + Convert.ToString(interest) + " and adjust for amunt is " + Convert.ToString(installment.Amount - interest);
                                }
                                else
                                {
                                    borrower.Interest -= installment.Amount;
                                    borrowerinstDetail.Description = "Amount cut for Interset" + Convert.ToString(installment.Amount) + " and adjust for amunt is 0";
                                }
                            }
                            else
                            {
                                borrower.InterstableAmount -= installment.Amount;
                                borrower.Outstanding -= installment.Amount;
                                borrowerinstDetail.Description = "Amount cut for Interset 0 and adjust for amunt is " + Convert.ToString(installment.Amount) ;
                            }
                            borrower.LastInstallmentDate = DateTime.Now;
                            db.SaveChanges();
                        }
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
                    var borrower = db.Borrowers.Where(m => m.BorrowID == installment.BorrowerID).FirstOrDefault();
                   
                    var outstandingAmount = borrower.Outstanding;
                    borrower.Outstanding = outstandingAmount == null ? installment.Amount : outstandingAmount + installment.Amount;
                    var interstableAmount = borrower.InterstableAmount;
                    borrower.InterstableAmount = interstableAmount == null ? installment.Amount : interstableAmount + installment.Amount;
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
                objinstllment = db.BorrowerInstallments.Where(m => m.InstallmentID == Id).FirstOrDefault();
            }
            catch (Exception)
            {

            }
            return objinstllment;
        }

        public List<BorrowerModel> AllBorrowers(BorrowerModel objborrower)
        {
            List<BorrowerModel> Allcust = new List<BorrowerModel>();
            using (ShopDevEntities db = new ShopDevEntities())
            {
                try
                {
                    var res = from buyer in db.Borrowers
                              where (buyer.Name.Contains(objborrower.Name) ||
                              buyer.CustCode == objborrower.CustCode)
                                || (string.IsNullOrEmpty(objborrower.Name) && objborrower.CustCode == null)

                              select buyer;
                    foreach (var seller in res)
                    {
                        BorrowerModel sellerModel = new BorrowerModel();
                        seller.CopyProperties(sellerModel);
                        Allcust.Add(sellerModel);
                    }
                }
                catch (Exception ex)
                {

                }
                return Allcust;
            }
        }

    }
}
