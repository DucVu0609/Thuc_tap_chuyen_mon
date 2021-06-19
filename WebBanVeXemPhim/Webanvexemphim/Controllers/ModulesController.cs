using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Webanvexemphim.Controllers
{
    public class ModulesController : Controller
    {
        private Webanvexemphim.Models.DbConnection db = new Webanvexemphim.Models.DbConnection();
        // GET: nơi chứa các layout chính của web header/footer
        public ActionResult _header()
        {
            return View("_header");
        }
        public ActionResult _mainmenu()
        {
            var listParentCate = db.menus.Where(m => m.status == 1 && m.parentid == 0).OrderBy(m => m.orders).ToList();
            return View("_mainmenu", listParentCate);
        }
        public ActionResult _footer()
        {
            return View("_footer");
        }

        public ActionResult submainmenu(int id)
        {
            ViewBag.mainmenuitem = db.menus.Find(id);
            var list = db.menus.Where(m => m.status == 1).Where(m => m.parentid == id)
                .OrderBy(m => m.orders);
            if (list.Count() != 0)
            {
                return View("_submainmenu1", list);
            }
            else
            {
                return View("_submainmenu2");
            }

        }

    }
}