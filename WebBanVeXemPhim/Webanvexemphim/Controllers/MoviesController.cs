using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Webanvexemphim.Controllers
{
    public class MoviesController : Controller
    {
        private Webanvexemphim.Models.DbConnection db = new Webanvexemphim.Models.DbConnection();
        // GET: Movies all movie
        public ActionResult Index()
        {
            return View();
        }
        //movies detail
        public ActionResult MoviesDetail(string slug)
        {
            var singleMovies = db.movies.Where(m => m.status == 1 && m.slug==slug ).FirstOrDefault();
            return View("MoviesDetail",singleMovies);
        }
        public ActionResult Movie_Category(string slug)
        {
            var singleMovies = db.categories.Where(m => m.status == 1 && m.slug == slug).FirstOrDefault();
            ViewBag.nameCate = singleMovies.name;
            var listMovie = db.movies.Where(m => m.status == 1 && m.catid== singleMovies.ID).ToList();
            return View("Movie_Category", listMovie);
        }
       
        // layout main
        public ActionResult _movieSlider()
        {
            return View("_movieSlider");
        }
        public ActionResult _movieComingSoom()
        {
            return View("_movieComingSoom");
        }
        // single List Movie
        public ActionResult _SingleMovie()
        {
            return View("_SingleMovie");
        }

        // pages main
        public ActionResult movieDetail()
        {
            return View("_SingleMovie");
        }

        //
        public ActionResult _MovieActive()
        {
            var list = db.movies.Where(m => m.status == 1).ToList();
            return View("_MovieActive", list);
        }
        public ActionResult _MovieOfDay()
        {
            var list = db.movies.Where(m => m.status == 1).ToList();
            return View("_MovieOfDay", list);
        }

    }
}
