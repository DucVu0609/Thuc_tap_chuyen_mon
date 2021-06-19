using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Webanvexemphim.Common;
using Webanvexemphim.Models;

namespace Webanvexemphim.Areas.Admin.Controllers
{
    public class OrdersController : BaseController
    {
        private DbConnection db = new DbConnection();

        // GET: Bdmin/Orders
        public ActionResult Index()
        {
            var list = db.Orders.Where(m => m.status != 0).OrderByDescending(m => m.ID).ToList();
            return View(list);
        }

        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var sigleOrder = db.Orders.Where(m => m.ID == id).First();
            return View("Orderdetail", sigleOrder);
        }
        //status
        public ActionResult Status(int id)
        {
            order morder = db.Orders.Find(id);
            morder.status = (morder.status == 1) ? 2 : 1;
            db.Entry(morder).State = EntityState.Modified;
            db.SaveChanges();
            Message.set_flash("Thay đổi trang thái thành công", "success");
            return RedirectToAction("Index");
        }
        //trash
        public ActionResult trash()
        {
            var list = db.Orders.Where(m => m.status == 0).ToList();
            return View("Trash", list);
        }
        public ActionResult Deltrash(int id)
        {
            order morder = db.Orders.Find(id);
            morder.status = 0;
            db.Entry(morder).State = EntityState.Modified;
            db.SaveChanges();
            Message.set_flash("Xóa thành công", "success");
            return RedirectToAction("Index");
        }
        public ActionResult productDetailCheckOut(int orderId)
        {
            var list = db.Ordersdetails.Where(m => m.orderid == orderId).ToList();
            return View("_productDetailCheckOut", list);

        }

        public ActionResult subnameProduct(int id)
        {
            var list = db.movies.Find(id);
            return View("_subproductOrdersuccess", list);

        }
        public ActionResult Retrash(int id)
        {
            order morder = db.Orders.Find(id);
            morder.status = 2;
            db.Entry(morder).State = EntityState.Modified;
            db.SaveChanges();
            Message.set_flash("Khôi phục thành công", "success");
            return RedirectToAction("trash");
        }
        public ActionResult deleteTrash(int id)
        {
            order morder = db.Orders.Find(id);
            db.Orders.Remove(morder);
            db.SaveChanges();
            Message.set_flash("Đã xóa vĩnh viễn 1 Đơn hàng", "success");
            return RedirectToAction("trash");
        }
    }
}
