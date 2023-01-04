using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ECOMMERCE_2023.Models;

namespace ECOMMERCE_2023.Controllers
{
    public class CategoriesController : Controller
    {
        private ECommerceEntities db = new ECommerceEntities();

        // GET: Categories
        public ActionResult Index()
        {
            return View(db.CATEGORIES.ToList());
        }

        // GET: Categories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CATEGORIES cATEGORIES = db.CATEGORIES.Find(id);
            if (cATEGORIES == null)
            {
                return HttpNotFound();
            }
            return View(cATEGORIES);
        }

        // GET: Categories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Category_id,Category_name")] CATEGORIES cATEGORIES)
        {
            if (ModelState.IsValid)
            {
                db.CATEGORIES.Add(cATEGORIES);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cATEGORIES);
        }

        // GET: Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CATEGORIES cATEGORIES = db.CATEGORIES.Find(id);
            if (cATEGORIES == null)
            {
                return HttpNotFound();
            }
            return View(cATEGORIES);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Category_id,Category_name")] CATEGORIES cATEGORIES)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cATEGORIES).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cATEGORIES);
        }

        // GET: Categories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CATEGORIES cATEGORIES = db.CATEGORIES.Find(id);
            if (cATEGORIES == null)
            {
                return HttpNotFound();
            }
            return View(cATEGORIES);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CATEGORIES categories = db.CATEGORIES.Find(id);
            db.CATEGORIES.Remove(categories);
            db.SaveChanges();
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
