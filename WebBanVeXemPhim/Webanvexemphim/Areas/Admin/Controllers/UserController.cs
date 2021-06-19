using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Webanvexemphim.Areas.Admin.Controllers;
using Webanvexemphim;
using Webanvexemphim.Common;
using Webanvexemphim.Models;

namespace Webanvexemphim.Areas.Admin.Controllers
{
   
    public class UserController : BaseController
    {
        private DbConnection db = new DbConnection();

        // GET: Admin/User
        public ActionResult Index()
        {
            var list = db.users.Where(m => m.status != 0).OrderByDescending(m => m.ID).ToList();
            return View(list);
        }

        // GET: Admin/User/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user user = db.users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Admin/User/Create
        public ActionResult Create()
        {
            //ViewBag.role = db.role.ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( user user, FormCollection data)
        {
            if (ModelState.IsValid)
            {
                string password1 = data["password1"];
                string password2 = data["password2"];
                string username = user.username;
                var Luser = db.users.Where(m => m.status == 1 && m.username == username);
                if (password1!=password2) {ViewBag.error = "PassWord không khớp";}
                if (Luser.Count()>0) { ViewBag.error1 = "Tên Đăng nhâp đã tồn tại";}
                else
                {
                    string pass = Mystring.ToMD5(password1);
                    user.img = "ádasd";
                    user.password = pass;
                    user.address = "";
                    user.created_at = DateTime.Now;
                    user.updated_at = DateTime.Now;
                    user.created_by = int.Parse(Session["Admin_id"].ToString());
                    user.updated_by = int.Parse(Session["Admin_id"].ToString());
                    db.users.Add(user);
                    db.SaveChanges();
                    Message.set_flash("Tạo user  thành công", "success");
                    return RedirectToAction("Index");
                }
            }
            return View(user);
        }

        // GET: Admin/User/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user user = db.users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            //ViewBag.role = db.Roles.ToList();
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( user user)
        {
            if (ModelState.IsValid)
            {
                    user.img = "ádasd";               
                    user.updated_at = DateTime.Now;
                    user.updated_by = int.Parse(Session["Admin_id"].ToString());
                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();
                Message.set_flash("Cập nhật thành công", "success");
                return RedirectToAction("Index");
            }
            return View(user);
        }

        //status
        public ActionResult Status(int id)
        {
            user user = db.users.Find(id);
            user.status = (user.status == 1) ? 2 : 1;
            user.updated_at = DateTime.Now;
            user.updated_by = int.Parse(Session["Admin_id"].ToString());
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
            Message.set_flash("Thay đổi trang thái thành công", "success");
            return RedirectToAction("Index");
        }
        //trash
        public ActionResult trash()
        {
            var list = db.users.Where(m => m.status == 0).ToList();
            return View("Trash", list);
        }
        public ActionResult Deltrash(int id)
        {
            user user = db.users.Find(id);
            user.status = 0;
            user.updated_at = DateTime.Now;
            user.updated_by = int.Parse(Session["Admin_id"].ToString());
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
            Message.set_flash("Xóa thành công", "success");
            return RedirectToAction("Index");
        }

        public ActionResult Retrash(int id)
        {
            user user = db.users.Find(id);
            user.status = 2;
            user.updated_at = DateTime.Now;
            user.updated_by = int.Parse(Session["Admin_id"].ToString());
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
            Message.set_flash("khôi phục thành công", "success");
            return RedirectToAction("trash");
        }
        public ActionResult deleteTrash(int id)
        {
            user user = db.users.Find(id);
            db.users.Remove(user);
            db.SaveChanges();
            Message.set_flash("Đã xóa vĩnh viễn 1 User", "success");
            return RedirectToAction("trash");
        }

    }
}
