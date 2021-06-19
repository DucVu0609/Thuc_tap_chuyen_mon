using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webanvexemphim.Models;
namespace Webanvexemphim.Controllers
{
    public class CheckoutController : Controller
    {
        private const string SessionCart = "SessionCart";
        private Webanvexemphim.Models.DbConnection db = new Webanvexemphim.Models.DbConnection();
        // GET: Checkout
        [HttpPost]
        public ActionResult  Thanh_toan(order order)
        {
            var cart = Session[SessionCart];

            var list = new List<Cart_item>();
            if (cart != null)
            {
                list = (List<Cart_item>)cart;
            }
            else
            {
                ViewBag.error = "Vui lòng thêm sản phẩm vào giỏ hàng";
                return View("index");
            }
            Random rand = new Random((int)DateTime.Now.Ticks);
            int RandomNumber;
            RandomNumber = rand.Next(100, 100000);
            if (ModelState.IsValid)
            {
                order.code = RandomNumber.ToString();
                order.userid = 1;
                order.created_ate = DateTime.Now;
                order.status = 2;
                order.userid = 1;
                order.exportdate = DateTime.Now;
                db.Orders.Add(order);
                db.SaveChanges();
                int lastid = order.ID;
                var orderLast = db.Orders.Find(lastid);
                ordersdetail orderdetail = new ordersdetail();
                foreach (var item in list)
                {
                    float price = 0;
                    price = (float)item.product.price * (int)item.quantity;                
                    orderdetail.orderid = order.ID;
                    orderdetail.productid = item.product.ID;
                    orderdetail.priceSale = (int)item.product.price;
                    orderdetail.day = item.day;
                    orderdetail.time = item.time;
                    orderdetail.price = item.product.price;
                    orderdetail.quantity = item.quantity;
                    orderdetail.amount = price;
                    db.Ordersdetails.Add(orderdetail);
                    db.SaveChanges();
                }
                ViewBag.cart = (List<Cart_item>)cart;
                Session["SessionCart"] = null;
                var listProductOrder = db.Ordersdetails.Where(m => m.orderid == order.ID);
                return View("oderComplete", orderLast);
            }
            ViewBag.error = "Đặt hàng thất bại";
            return View("index");

        }
        public ActionResult subnameProduct(int id)
        {
            var list = db.movies.Find(id);
            return View("_subproductOrdersuccess", list);

        }
        public ActionResult productDetailCheckOut(int orderId)
        {
            var list = db.Ordersdetails.Where(m => m.orderid == orderId).ToList();
            return View("_productDetailCheckOut", list);

        }
    }
}