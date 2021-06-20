using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Webanvexemphim.Controllers
{
    public class PostCController : Controller
    {
        private Webanvexemphim.Models.DbConnection db = new Webanvexemphim.Models.DbConnection();
        // GET: PostC
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult postDetaail(string slug)
        {
            var SingleProduct = db.posts.Where(m => m.status == 1 && m.slug == slug).First();
            //ViewBag.category = db.Categorys.Find(SingleProduct.catid);
            return View("postDetaail", SingleProduct);
        }
        public ActionResult post_cate(string slug)
        {
            var singleMovies = db.topics.Where(m=>m.slug == slug).FirstOrDefault();
            ViewBag.nameCate = singleMovies.name.ToString();
            var listMovie = db.posts.Where(m => m.status == 1 && m.topid == singleMovies.ID).ToList();
            return View("post_cate", listMovie);
        }
        public ActionResult _postSugger()
        {
            var singleMovies = db.topics.Where(m => m.status == 1 && m.slug == "thong-tin-phim").FirstOrDefault();
            var listMovie = db.posts.Where(m => m.status == 1 && m.topid == singleMovies.ID).ToList();
            
            return View("_postSugger", listMovie);
        }

    }
}