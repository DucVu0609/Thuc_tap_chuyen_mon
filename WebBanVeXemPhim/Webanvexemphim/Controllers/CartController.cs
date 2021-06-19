using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webanvexemphim.Models;

namespace Webanvexemphim.Controllers
{
    public class CartController : Controller
    {
        
            // khởi tạo session:
            private const string SessionCart = "SessionCart";
        // GET: Cart
            Webanvexemphim.Models.DbConnection db = new Webanvexemphim.Models.DbConnection();
            public ActionResult Index()
            {
                var cart = Session[SessionCart];
                var list = new List<Cart_item>();
                if (cart != null)
                {
                    list = (List<Cart_item>)cart;
                }
                return View(list);
            }
            public ActionResult card_header()
            {
                var cart = Session[SessionCart];
                var list = new List<Cart_item>();
                if (cart != null)
                {

                    list = (List<Cart_item>)cart;
                    int quantytyyy = 0;
                    foreach (var item1 in list)
                    {
                        quantytyyy += item1.quantity;
                    }
                    ViewBag.quantity = quantytyyy;
                }
                return View(list);
            }
            public RedirectToRouteResult updateitem(long P_SanPhamID, int P_quantity)
            {
                var cart = Session[SessionCart];
                var list = (List<Cart_item>)cart;
                Cart_item itemSua = list.FirstOrDefault(m => m.product.ID == P_SanPhamID);
                if (itemSua != null)
                {
                    itemSua.quantity = P_quantity;
                }
                return RedirectToAction("Index");
            }
            public RedirectToRouteResult deleteitem(long productID)
            {
                var cart = Session[SessionCart];
                var list = (List<Cart_item>)cart;

                Cart_item itemXoa = list.FirstOrDefault(m => m.product.ID == productID);
                if (itemXoa != null)
                {
                    list.Remove(itemXoa);
                }
            Message.set_flash("Cập nhật thành công", "success");
            return RedirectToAction("Index");
            }
            public RedirectToRouteResult Additem(long productID, int quantity , String time)
            {
                var item = new Cart_item();
                movies product = db.movies.Find(productID);
                var cart = Session[SessionCart];
                if (cart != null)
                {
                    var list = (List<Cart_item>)cart;
                    if (list.Exists(m => m.product.ID == productID && m.time ==time ))
                    {
                        int quantity1 = 0;
                        foreach (var item1 in list)
                        {
                            if (item1.product.ID == productID && item1.time == time )
                            {
                                item1.quantity += quantity;
                                quantity1 = item1.quantity;
                            }
                        }
                        int priceTotol = 0;

                        int price = 0;
                        foreach (var item1 in list)
                        {
                            int temp = (int)item1.product.price * (int)item1.quantity;
                            priceTotol += temp;

                            price = (int)item1.product.price;
                        }
                    Message.set_flash("Thêm thành công", "success");
                    return RedirectToAction("Index");

                    }
                    else
                    {
                        item.product = product;
                        item.quantity = quantity;
                        item.time = time;
                        item.day = DateTime.Now;
                        list.Add(item);
                        item.countCart = list.Count();
                        item.meThod = "cartExist";
                        int priceTotol = 0;
                        foreach (var item1 in list)
                        {
                            int temp = (int)item1.product.price * (int)item1.quantity;
                            priceTotol += temp;
                        }
                        item.priceTotal = priceTotol;
                    Message.set_flash("Thêm thành công", "success");
                    return RedirectToAction("Index");
                    }
                }
                else
                {
                    item.product = product;
                    item.quantity = quantity;
                    item.time = time;
                    item.day = DateTime.Now;
                    item.meThod = "cartEmpty";
                    item.countCart = 1;
                    item.priceTotal = (int)product.price;
                    var list = new List<Cart_item>();
                    list.Add(item);
                    Session[SessionCart] = list;

                }
            Message.set_flash("Thêm thành công", "success");
            return RedirectToAction("Index");
            }
        }
    }