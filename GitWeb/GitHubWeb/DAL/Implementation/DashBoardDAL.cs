using DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Implementation
{
    public class DashBoardDAL : IDashBoard
    {
        public void Get()
        {
        }
        public void CalculateInterest()
        {
            using (ShopDevEntities db = new ShopDevEntities())
            {
                var todayYear = DateTime.Now.Year;
                var CurrentMonth = DateTime.Now.Month;
                try
                {
                    // buyer
                    var Allbuyer = db.Buyers.Where(m => m.OutstandingAmount > 0).ToList();
                    foreach (var buyer in Allbuyer)
                    {
                        int buyMonth = 0;
                        int buyYear = 0;
                        if (buyer.LastInstallmentDate != null)
                        {
                            buyMonth = buyer.LastInstallmentDate.Value.Month;
                            buyYear = buyer.LastInstallmentDate.Value.Year;
                        }
                        else
                        {
                            buyMonth = buyer.BuyDate.Value.Month;
                            buyYear = buyer.BuyDate.Value.Year;
                        }
                        var monthDiff = ((todayYear - buyYear) * 12) + CurrentMonth - buyMonth;
                        var interest = buyer.InterstableAmount * buyer.InterestRate * monthDiff / 100;
                        buyer.Interest = interest;
                        buyer.OutstandingAmount += interest;
                        buyer.InterstableAmount += interest;
                    }
                    // Borrower
                    var AllBorrower = db.Borrowers.Where(m => m.Amont > 0).ToList();
                    foreach (var borrow in AllBorrower)
                    {
                        int borrowMonth = 0;
                        int borrowYear = 0;
                        if (borrow.LastInstallmentDate != null)
                        {
                            borrowMonth = borrow.LastInstallmentDate.Value.Month;
                            borrowYear = borrow.LastInstallmentDate.Value.Year;
                        }
                        else
                        {
                            borrowMonth = borrow.Date.Value.Month;
                            borrowYear = borrow.Date.Value.Year;
                        }
                        var monthDiff = ((todayYear - borrowYear) * 12) + CurrentMonth - borrowMonth;
                        var interest= borrow.InterstableAmount * borrow.InterestRate * monthDiff / 100;
                        borrow.Interest = interest;
                        borrow.Outstanding += interest;
                        borrow.InterstableAmount += interest;
                    }
                    // Bulks
                    var AllBulks = db.BulkBuys.Where(m => m.OustandingAmont > 0).ToList();
                    foreach (var bulk in AllBulks)
                    {
                        var buyMonth = bulk.StartDate.Value.Month;
                        var buyYear = bulk.StartDate.Value.Year;
                        var monthDiff = ((todayYear - buyYear) * 12) + CurrentMonth - buyMonth;
                        decimal? interst = 0;
                         
                            interst = bulk.InterstableAmount * bulk.InterestRate * monthDiff / 100;
                         

                        bulk.Interest = interst;

                        bulk.OustandingAmont +=  interst;
                        bulk.InterstableAmount += interst;
                    }
                    db.SaveChanges();
                }
                catch { }
            }
        }
    }
}
