using ECOMMERCE_2023.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ECOMMERCE_2023.Controllers
{
    [Authorize]
    public class BasketController : Controller
    {
        ECommerceEntities db = new ECommerceEntities();
        public ActionResult Index()
        {
            string user_id = User.Identity.GetUserId();
            return View(db.ORDER_CART.Where(x=>x.User_id==user_id).ToList());
        }

        public ActionResult AddToBaset(int Product_id,int custom)
        {
            string user_id = User.Identity.GetUserId();
            ORDER_CART product_cart= db.ORDER_CART.FirstOrDefault(x=>x.Product_id == Product_id && x.User_id ==user_id);

            PRODUCT products = db.PRODUCT.Find(Product_id);

            if(product_cart == null)
            {
                ORDER_CART new_product = new ORDER_CART()
                {
                    User_id = user_id,
                    Product_id = Product_id,
                    Custom=custom,
                    Total_amount = products.Product_price * custom
                };
                db.ORDER_CART.Add(new_product);
            }
            else
            {
                product_cart.Custom = product_cart.Custom + custom;
                product_cart.Total_amount = product_cart.Custom * products.Product_price;
            }
            db.SaveChanges();

            return RedirectToAction("Index");
        }
        
        public ActionResult BasketUpdate(int? Order_cart_id,int Custom)
        {
            if(Order_cart_id == null)
            {
                return  new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ORDER_CART order_cart=db.ORDER_CART.Find(Order_cart_id);
            if(order_cart == null)
            {
                return  HttpNotFound();
            }
            PRODUCT product=db.PRODUCT.Find(order_cart.Product_id);
            order_cart.Custom=Custom;
            order_cart.Total_amount=order_cart.Custom * product.Product_price;
            return RedirectToAction("Index");
        }

        public ActionResult BasketDelete(int id)
        {
            ORDER_CART order_cart=db.ORDER_CART.Find(id);
            db.ORDER_CART.Remove(order_cart);   
            db.SaveChanges();
            return RedirectToAction("Index");

        }


    }
}