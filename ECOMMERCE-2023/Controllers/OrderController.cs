using ECOMMERCE_2023.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security;
namespace ECOMMERCE_2023.Controllers
{
    public class OrderController : Controller
    {
        ECommerceEntities db=new ECommerceEntities();
        public ActionResult Index()
        {
            string user_id = User.Identity.GetUserId();
            return View(db.ORDER.Where(x=>x.User_id==user_id).ToList());
        }
        [ValidateAntiForgeryToken]
        public ActionResult OrderComplete()
        {
            //    ClientID: Bankadan alınan mağaza kodu
            //    Amount:Sepetteki ürünlerin toplam tutar
            //    Oid:SiparişID
            //    OnayUrl:Ödeme başarılı olduğunda gelen verilerin gösterileceği url
            //    HataUrl:Ödeme sırasında hata olduysa gelen hatanın gösterileceği url
            //    RDN:Hash karşılaştırılıması için kullanılan bilgi
            //        StoreKEy:Güvenlik anahtarı.Bankanın sanal pos sayfasından alınır.
            //        TransactionType:"Auth"
            //        Instalment:""
            //        HashStr:HashSet oluşturulurken bankanın istediği bilgiler birleştirilir.
            //        Hash:Farklı değerler oluşturulup birleştirilir.

            string userID = User.Identity.GetUserId();
            List<ORDER_CART> basketProduct = db.ORDER_CART.Where(x => x.User_id == userID).ToList();

            string ClientId = "1003001";//Bankanın verdiği magaza kodu
            string TotalAmount = basketProduct.Sum(x => x.Total_amount).ToString();

            string sipId = string.Format("{0:yyyyMMddHHmmss}", DateTime.Now);
           
            string onayURL = "https://localhost:44377/Order/Complete";

            string hataURL = "https://localhost:44377/Order/Error";

            string RDN = "asdf";
            string StoreKey = "123456";

            string TransActionType = "Auth";
            string Instalment = "";

            string HashStr = ClientId + sipId + TotalAmount + onayURL + hataURL + TransActionType + Instalment + RDN + StoreKey;//Bankanın istediği bilgiler

            System.Security.Cryptography.SHA1 sha = new System.Security.Cryptography.SHA1CryptoServiceProvider();

            byte[] HashBytes = System.Text.Encoding.GetEncoding("ISO-8859-9").GetBytes(HashStr);
            byte[] InputBytes = sha.ComputeHash(HashBytes);
            string Hash = Convert.ToBase64String(InputBytes);
            ViewBag.ClientId = ClientId;
            ViewBag.Oid = sipId;
            ViewBag.okUrl = onayURL;
            ViewBag.failUrl = hataURL;
            ViewBag.TransActionType = TransActionType;
            ViewBag.RDN = RDN;
            ViewBag.Hash = Hash;
            ViewBag.Amount = TotalAmount;
            ViewBag.StoreType = "3d_pay_hosting"; // Ödeme modelimiz
            ViewBag.Description = "";
            ViewBag.XID = "";
            ViewBag.Lang = "tr";
            ViewBag.EMail = "email@gmail.com";
            ViewBag.UserID = "usernameid"; // bu id yi bankanın sanala pos ekranında biz oluşturuyoruz.
            ViewBag.PostURL = "https://entegrasyon.asseco-see.com.tr/fim/est3Dgate&quot";//Bankanın kendi sitesi

            return View();
        }

        public ActionResult Complete()
        {
            //sipariş sepeti
            //sipariş tamamlama nesnesi
            ORDER orderCompleteFinish=new ORDER()
            {
                Name =Request.Form.Get("Name"),
                Surname = Request.Form.Get("Surname"),
                Identification_number= Request.Form.Get("Identification_number"),
                Telephone = Request.Form.Get("Telephone"),
                Address = Request.Form.Get("Address"),
                Date=DateTime.Now,
                User_id=User.Identity.GetUserId(),
            };

            string useridd=User.Identity.GetUserId();
            List<ORDER_CART> orders = db.ORDER_CART.Where(x=>x.User_id == useridd).ToList();

            foreach (ORDER_CART item in orders)
            {
                //sipariş detay nesnesi
                ORDER_DETAILS od=new ORDER_DETAILS()
                {
                    //sepetteki urunlerı sipariş detay nesnenin içine attık
                    Custom=item.Custom,
                    Product_id=item.Product_id, 
                    Total=item.Total_amount
                };
                //sipariş detayları sipariş tamamlayacağımız nesneye attık
                orderCompleteFinish.ORDER_DETAILS.Add(od);
                //ve ardından temizledik
                db.ORDER_CART.Remove(item);
            }
            return View();  
        }
        public ActionResult Error()
        {
            ViewBag.Error = Request.Form;//formdaki tüm bilgileri alır ve hata sayfasında hangi işlem hatalı ise müşteriye onu gösteririz
            return View();  
        }

    }
}