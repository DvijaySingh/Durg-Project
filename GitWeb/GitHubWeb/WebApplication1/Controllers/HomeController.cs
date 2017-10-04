using DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Filter;

namespace WebApplication1.Controllers
{
    [AuthFilter]
    public class HomeController : Controller
    {
        private IDashBoard _IDashBoard;
        public HomeController(IDashBoard idashboard)
        {
            this._IDashBoard = idashboard;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}