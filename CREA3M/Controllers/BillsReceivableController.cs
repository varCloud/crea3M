using CREA3M.DAO;
using CREA3M.Helpers;
using CREA3M.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CREA3M.Controllers
{
    public class BillsReceivableController : Controller
    {
        // GET: BillsReceivable
        public ActionResult Index()
        {
            if (new validation().validateSession(Session))
            {
                ViewBag.username = Session["username"];
                ViewBag.sucursales = sucursales.buildList(Session["defaultDB"].ToString());
                return View();
            }

            return Redirect("/Login/Index?nosession=1");
        }

        public ActionResult Filtered(String initDate, String endDate, String selectedDB)
        {
            if (!new validation().validateSession(Session))
            {
                return Redirect("/Login/Index?nosession=1");
            }

            DateTime date = DateTime.Today;
            initDate = initDate == null ? date.ToString("yyyy-MM-dd") : initDate;
            endDate = endDate == null ? date.ToString("yyyy-MM") + "-01" : endDate;
            selectedDB = selectedDB == null ? "sucursal" + Session["defaultDB"] : "sucursal" + selectedDB;

            ResponseList<BillsReceivableModel> response = new BillsReceivableDAO().getBills(initDate, endDate, selectedDB);
            return PartialView("Filtered", response);
        }
    }
}