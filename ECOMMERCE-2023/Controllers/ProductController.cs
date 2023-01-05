using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ECOMMERCE_2023.Models;

namespace ECOMMERCE_2023.Controllers
{
    public class ProductController : Controller
    {
        private ECommerceEntities db = new ECommerceEntities();

        // GET: Product
        public ActionResult Index()
        {
            var pRODUCT = db.PRODUCT.Include(p => p.CATEGORIES);
            return View(pRODUCT.ToList());
        }

        // GET: Product/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PRODUCT pRODUCT = db.PRODUCT.Find(id);
            if (pRODUCT == null)
            {
                return HttpNotFound();
            }
            return View(pRODUCT);
        }
        #region Create
        public ActionResult Create()
        {
            ViewBag.Category_id = new SelectList(db.CATEGORIES, "Category_id", "Category_name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PRODUCT pRODUCT,HttpPostedFileBase product_image)
        {
            if (ModelState.IsValid)
            {
                db.PRODUCT.Add(pRODUCT);
                db.SaveChanges();
                if(product_image != null)
                {
                    string filePath=Path.Combine(Server.MapPath("~/image"),pRODUCT.Product_id + ".jpg" );
                    product_image.SaveAs(filePath);
                }
                return RedirectToAction("Index");
            }

            ViewBag.Category_id = new SelectList(db.CATEGORIES, "Category_id", "Category_name", pRODUCT.Category_id);
            return View(pRODUCT);
        }
        #endregion



        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PRODUCT pRODUCT = db.PRODUCT.Find(id);
            if (pRODUCT == null)
            {
                return HttpNotFound();
            }
            ViewBag.Category_id = new SelectList(db.CATEGORIES, "Category_id", "Category_name", pRODUCT.Category_id);
            return View(pRODUCT);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PRODUCT pRODUCT,HttpPostedFileBase product_image)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pRODUCT).State = EntityState.Modified;
                db.SaveChanges();
                if(product_image != null)
                {
                    string path=Path.Combine(Server.MapPath("~/image/"),pRODUCT.Product_id+".jpg");
                    product_image.SaveAs(path); 
                }
                return RedirectToAction("Index");
            }
            //kategoriler tablosunda category_id ile parametre ile gelen category_id eşleştirme işlemi yapılır, arka planda category id tutulur görünürde category_name gözükür,
            ViewBag.Category_id = new SelectList(db.CATEGORIES, "Category_id", "Category_name", pRODUCT.Category_id);
            return View(pRODUCT);
        }


        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PRODUCT pRODUCT = db.PRODUCT.Find(id);
            if (pRODUCT == null)
            {
                return HttpNotFound();
            }
            return View(pRODUCT);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PRODUCT pRODUCT = db.PRODUCT.Find(id);
            db.PRODUCT.Remove(pRODUCT);
            db.SaveChanges();
            string filePath=Path.Combine(Server.MapPath("~/image"),id + ".jpg" );
            FileInfo fileInfo = new FileInfo(filePath);
            if (fileInfo.Exists)
            {
                fileInfo.Delete();
            }
            return RedirectToAction("Index");
        }







        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
