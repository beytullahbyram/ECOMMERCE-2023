using ECOMMERCE_2023.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECOMMERCE_2023.Controllers
{
    public class HomeController : Controller
    {
        ECommerceEntities db=new ECommerceEntities();
        public ActionResult Index()
        {
            ViewBag.Categories = db.CATEGORIES.ToList();
            ViewBag.Products = db.PRODUCT.ToList();
            return View();
        }
        public ActionResult Category(int id)
        {
            ViewBag.Categories = db.CATEGORIES.ToList();
            ViewBag.category = db.CATEGORIES.Find(id);
            return View(db.PRODUCT.Where(x=>x.Category_id==id).ToList());
        }

        public ActionResult Product(int id)
        {
            ViewBag.Categories = db.CATEGORIES.ToList();
            return View(db.PRODUCT.Find(id));
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