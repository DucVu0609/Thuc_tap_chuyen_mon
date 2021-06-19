using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Webanvexemphim.Areas.Admin.Controllers
{
    public class TicketController : BaseController
    {
        // GET: Admin/Ticket
        public ActionResult Index()
        {
            return View();
        }
    }
}