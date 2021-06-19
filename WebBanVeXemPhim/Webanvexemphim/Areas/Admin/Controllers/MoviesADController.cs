using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Webanvexemphim.Models;

namespace Webanvexemphim.Areas.Admin.Controllers
{
    public class MoviesADController : BaseController
    {
        private Webanvexemphim.Models.DbConnection db = new Webanvexemphim.Models.DbConnection();
        // GET: Admin/MoviesAD
        public ActionResult Index()
        {
            var list = db.movies.Where(m => m.status > 0).OrderByDescending(m => m.ID).ToList();
            return View(list);
        }
        // GET: Admin/Post/Create
        public ActionResult Create()
        {
            ViewBag.listTopic = db.categories.Where(m => m.status == 1).ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(movies movies)
        {
            if (ModelState.IsValid)
            {
                HttpPostedFileBase file;
                var namecateDb = db.categories.Where(m => m.ID == movies.catid).First();
                string slug = Mystring.ToSlug(movies.name.ToString());
                string namecate = Mystring.ToStringNospace(namecateDb.name);
                file = Request.Files["img"];
                string filename = file.FileName.ToString();
                string ExtensionFile = Mystring.GetFileExtension(filename);
                string namefilenew = namecate + "/" + slug + "." + ExtensionFile;
                var path = Path.Combine(Server.MapPath("~/public/images/"), namefilenew);
                var folder = Server.MapPath("~/public/images/" + namecate);
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }
                file.SaveAs(path);
                movies.img = namefilenew;
                movies.slug = slug;
                db.movies.Add(movies);
                db.SaveChanges();
                Message.set_flash("Thêm thành công", "success");
                return RedirectToAction("Index");
            }
            ViewBag.listTopic = db.categories.Where(m => m.status != 0).ToList();
            Message.set_flash("Thêm Thất Bại", "danger");
            return View(movies);
        }
        // GET: Admin/Post/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            movies movies = db.movies.Find(id);
            if (movies == null)
            {
                return HttpNotFound();
            }
            ViewBag.listTopic = db.categories.Where(m => m.status != 0).ToList();
            return View(movies);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(movies movies)
        {
            if (ModelState.IsValid)
            {
                HttpPostedFileBase file;
                string slug = Mystring.ToSlug(movies.name.ToString());
                file = Request.Files["img"];
                string filename = file.FileName.ToString();
                if (filename.Equals("") == false)
                {
                    var namecateDb = db.categories.Where(m => m.ID == movies.catid).First();
                    string namecate = Mystring.ToStringNospace(namecateDb.name);
                    string ExtensionFile = Mystring.GetFileExtension(filename);
                    string namefilenew = namecate + "/" + slug + "." + ExtensionFile;
                    var path = Path.Combine(Server.MapPath("~/public/images/"), namefilenew);
                    var folder = Server.MapPath("~/public/images/" + namecate);
                    if (!Directory.Exists(folder))
                    {
                        Directory.CreateDirectory(folder);
                    }
                    file.SaveAs(path);
                    movies.img = namefilenew;
                }
                movies.slug = slug;
              
                db.Entry(movies).State = EntityState.Modified;
                db.SaveChanges();
                Message.set_flash("Sửa thành công", "success");
                return RedirectToAction("Index");
            }
            ViewBag.listTopic = db.categories.Where(m => m.status != 0).ToList();
            Message.set_flash("Sửa Thất Bại", "danger");
            return View(movies);
        }
        public ActionResult Status(int id)
        {
            movies movies = db.movies.Find(id);
            movies.status = (movies.status == 1) ? 2 : 1;        
            db.Entry(movies).State = EntityState.Modified;
            db.SaveChanges();
            Message.set_flash("Thay đổi trang thái thành công", "success");
            return RedirectToAction("Index");
        }
        public ActionResult Deltrash(int id)
        {
            movies movies = db.movies.Find(id);
            movies.status = 0;
            db.Entry(movies).State = EntityState.Modified;
            db.SaveChanges();
            Message.set_flash("Xóa thành công", "success");
            return RedirectToAction("Index");
        }
        
    }
}