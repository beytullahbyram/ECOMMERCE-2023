using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using ECOMMERCE_2023.Models;
using Newtonsoft.Json;

namespace ECOMMERCE_2023.Controllers
{
    public class CategoriesController : Controller
    {
        private ECommerceEntities db = new ECommerceEntities();
        HttpClient httpClient = new HttpClient();//GET POST PUT DELETE işlemlerini yapmamızı sağlar
        public ActionResult Index()
        {
            List<CATEGORIES> categories = new List<CATEGORIES>();
            httpClient.BaseAddress = new Uri("https://localhost:44317/api/");//istek göndereceğimiz api adresi
            var response=httpClient.GetAsync("Category");//apinin gideği controller veya adres
            response.Wait();//cevap gelene kadar bekleniyor
            var result=response.Result; //clienttan gelen sonucu değişkene attık
            if(result.IsSuccessStatusCode)
            {
                var readstringdata= result.Content.ReadAsStringAsync();//json sonucu string olarak okuyoruz
                readstringdata.Wait();
                categories=JsonConvert.DeserializeObject<List<CATEGORIES>>(readstringdata.Result);//json formatindaki verileri okumak için deserilaze işlemi yaptık yani parçalayıp verilere ulaşmış olduk
            }
            return View(categories);
        }

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

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //View üzerinden post işlemi ile gelen modelden gelmesini istediğimiz veya istemediğimiz propertyleri bind ile belirliyoruz
        //Exlude => view üzerinden gelen modelden istemediğimiz propertylerin o view üzerinden gelmesini istemediğimiz zaman kullanırız
        //Include => view üzerinden gelen kullanmak istediğimiz propertyleri belirtiriz
        public ActionResult Create([Bind(Include = "Category_id,Category_name")] CATEGORIES cATEGORIES)
        {
            if (ModelState.IsValid)//isvalid ile modelin veritabanına doğru bir geçerli olup olmadığını kontrol eder
            {
                httpClient.BaseAddress=new Uri("https://localhost:44317/api/");
                //apiye json olarak bir post isteği göndeririz ve gelen cevabı değişkene alırız
                var response = HttpClientExtensions.PostAsJsonAsync<CATEGORIES>(httpClient,"Category",cATEGORIES);
                response.Wait();
                var result = response.Result;
                if(result.IsSuccessStatusCode)
                    return RedirectToAction("Index");
            }
            return View(cATEGORIES);
        }

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
