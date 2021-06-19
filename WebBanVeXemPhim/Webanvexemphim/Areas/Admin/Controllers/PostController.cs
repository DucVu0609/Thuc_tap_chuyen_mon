using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Webanvexemphim;
using Webanvexemphim.Common;
using Webanvexemphim.Models;

namespace Webanvexemphim.Areas.Admin.Controllers
{
    public class PostController : BaseController
    {
        private DbConnection db = new DbConnection();

        // GET: Admin/Post
        public ActionResult Index()
        {
            var list = db.posts.Where(m => m.status > 0).OrderByDescending(m=>m.ID).ToList();
            return View(list);
        }

        // GET: Admin/Post/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            post post = db.posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // GET: Admin/Post/Create
        public ActionResult Create()
        {
            ViewBag.listTopic = db.topics.Where(m => m.status == 1 ).ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(post post)
        {
            if (ModelState.IsValid)
            {
                HttpPostedFileBase file;
                var namecateDb = db.topics.Where(m => m.ID == post.topid).First();
                string slug = Mystring.ToSlug(post.title.ToString());
                string namecate = Mystring.ToStringNospace(namecateDb.name);
                file = Request.Files["img"];
                string filename = file.FileName.ToString();
                string ExtensionFile = Mystring.GetFileExtension(filename);
                string namefilenew = namecate + "/" + slug + "." + ExtensionFile;
                var path = Path.Combine(Server.MapPath("~/public/images/Post/"), namefilenew);
                var folder = Server.MapPath("~/public/images/Post/" + namecate);
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }
                file.SaveAs(path);
                post.img = namefilenew;
                post.slug = slug;
                post.type = "Post";
                post.created_at = DateTime.Now;
                post.updated_at = DateTime.Now;
                post.created_by = int.Parse(Session["Admin_id"].ToString());
                post.updated_by = int.Parse(Session["Admin_id"].ToString());
                db.posts.Add(post);
                db.SaveChanges();
                Message.set_flash("Thêm thành công", "success");
                return RedirectToAction("Index");
            }
            ViewBag.listTopic = db.topics.Where(m => m.status != 0).ToList();
            Message.set_flash("Thêm Thất Bại", "danger");
            return View(post);
        }

        // GET: Admin/Post/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            post post = db.posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            ViewBag.listTopic = db.topics.Where(m => m.status != 0).ToList();
            return View(post);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit( post post)
        {
            if (ModelState.IsValid)
            {
                HttpPostedFileBase file;
                string slug = Mystring.ToSlug(post.title.ToString());
                file = Request.Files["img"];
                string filename = file.FileName.ToString();
                if (filename.Equals("") == false)
                {
                    var namecateDb = db.topics.Where(m => m.ID == post.topid).First();
                    string namecate = Mystring.ToStringNospace(namecateDb.name);
                    string ExtensionFile = Mystring.GetFileExtension(filename);
                    string namefilenew = namecate + "/" + slug + "." + ExtensionFile;
                    var path = Path.Combine(Server.MapPath("~/public/images/post"), namefilenew);
                    var folder = Server.MapPath("~/public/images/post/" + namecate);
                    if (!Directory.Exists(folder))
                    {
                        Directory.CreateDirectory(folder);
                    }
                    file.SaveAs(path);
                    post.img = namefilenew;
                }
                post.slug = slug;
                post.updated_at = DateTime.Now;
                post.updated_by = int.Parse(Session["Admin_id"].ToString());
              
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                Message.set_flash("Sửa thành công", "success");
              
                return RedirectToAction("Index");
            }
            ViewBag.listTopic = db.topics.Where(m => m.status != 0).ToList();
            Message.set_flash("Sửa Thất Bại", "danger");
            return View(post);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            post post = db.posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }
        public ActionResult Status(int id)
        {
            post post = db.posts.Find(id);
            post.status = (post.status == 1) ? 2 : 1;
            post.updated_at = DateTime.Now;
            post.updated_by = int.Parse(Session["Admin_id"].ToString());
            db.Entry(post).State = EntityState.Modified;
            db.SaveChanges();
            Message.set_flash("Thay đổi trang thái thành công", "success");
            return RedirectToAction("Index");
        }
        public ActionResult trash()
        {
            var list = db.posts.Where(m => m.status == 0).ToList();
            return View("Trash", list);
        }
        public ActionResult Deltrash(int id)
        {
            post post = db.posts.Find(id);
            post.status = 0;
            post.updated_at = DateTime.Now;
            post.updated_by = int.Parse(Session["Admin_id"].ToString());
            db.Entry(post).State = EntityState.Modified;
            db.SaveChanges();
            Message.set_flash("Xóa thành công", "success");
            return RedirectToAction("Index");
        }
        public ActionResult Retrash(int id)
        {
            post post = db.posts.Find(id);
            post.status = 2;
            post.updated_at = DateTime.Now;
            post.updated_by = int.Parse(Session["Admin_id"].ToString());
            db.Entry(post).State = EntityState.Modified;
            db.SaveChanges();
            Message.set_flash("khôi phục thành công", "success");
            return RedirectToAction("trash");
        }
        public ActionResult deleteTrash(int id)
        {
            post post = db.posts.Find(id);
            db.posts.Remove(post);
            db.SaveChanges();
            Message.set_flash("Đã xóa vĩnh viễn 1 sản phẩm", "success");
            return RedirectToAction("trash");
        }
    }
}
