using CREA3M.DAO;
using CREA3M.Helpers;
using CREA3M.Models;
using System;
using System.Web.Mvc;

namespace CREA3M.Controllers
{
    public class SalesController : Controller
    {
        // GET: Ventas
        public ActionResult Index()
        {
            if (new validation().validateSession(Session))
            {
                ViewBag.username = Session["username"];
                ViewBag.sucursales = new sucursales().buildList(Session["defaultDB"].ToString());
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

            ResponseList<SaleModel> response = new SalesDAO().getVentas(initDate, endDate, selectedDB);
            return PartialView("Filtered", response);
        }
    }
}