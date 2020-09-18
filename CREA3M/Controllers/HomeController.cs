using CREA3M.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CREA3M.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if(validation.validateSession(Session))
            {
                ViewBag.username = Session["username"];
                return View();
            }
            else
            {
                return Redirect("/Login/Index?nosession=1");
            }
        }
    }
}