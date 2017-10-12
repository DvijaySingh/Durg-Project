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
                        var buyMonth = buyer.BuyDate.Value.Month;
                        var buyYear = buyer.BuyDate.Value.Year;
                        var monthDiff = ((todayYear - buyYear) * 12) + CurrentMonth - buyMonth;
                        buyer.Interest = buyer.OutstandingAmount * buyer.InterestRate * monthDiff / 100;
                    }
                    // Borrower
                    var AllBorrower = db.Borrowers.Where(m => m.Amont > 0).ToList();
                    foreach (var borrow in AllBorrower)
                    {
                        var buyMonth = borrow.Date.Value.Month;
                        var buyYear = borrow.Date.Value.Year;
                        var monthDiff = ((todayYear - buyYear) * 12) + CurrentMonth - buyMonth;
                        borrow.Interest = borrow.Amont * borrow.InterestRate * monthDiff / 100;
                    }
                    // Bulks
                    var AllBulks = db.BulkBuys.Where(m => m.OustandingAmont > 0).ToList();
                    foreach (var bulk in AllBulks)
                    {
                        var buyMonth = bulk.StartDate.Value.Month;
                        var buyYear = bulk.StartDate.Value.Year;
                        var monthDiff = ((todayYear - buyYear) * 12) + CurrentMonth - buyMonth;
                        decimal? interst = 0;
                        if (bulk.TakenAmount < bulk.OustandingAmont)
                        {
                            interst = bulk.TakenAmount * bulk.InterestRate * monthDiff / 100;
                        }
                        else
                        {
                            interst = bulk.OustandingAmont * bulk.InterestRate * monthDiff / 100;
                        }

                        bulk.Interest = interst;
                        bulk.OustandingAmont = bulk.TakenAmount + interst;
                    }
                    db.SaveChanges();
                }
                catch { }
            }
        }
    }
}
