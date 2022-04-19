using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CREA3M.Controllers
{
    public class PlantillasController : Controller
    {
        // GET: Plantilla
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CorreoPedido()
        {
            return View();
        }
    }
}