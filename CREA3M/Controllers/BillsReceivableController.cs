using CREA3M.DAO;
using CREA3M.Filters;
using CREA3M.Helpers;
using CREA3M.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CREA3M.Controllers
{
    [SessionTimeout]
    public class BillsReceivableController : Controller
    {
        // GET: BillsReceivable
        public ActionResult Index()
        {
           
                ViewBag.username = Session["username"];
                ViewBag.sucursales = sucursales.buildList(Session["defaultDB"].ToString());
                ViewBag.localidades = new ClientsDAO().getCitiesSelect("sucursal" + Session["defaultDB"].ToString());
                ViewBag.usuarios = new UsersDAO().getUsersSelect("sucursal" + Session["defaultDB"].ToString());
                ViewBag.clientes = new List<SelectListItem> { new SelectListItem { Text = "Seleccione una localidad primero", Value = "-1" } };
                ViewBag.admin = Session["admin"];

                return View();
         

           
        }

        public ActionResult Filtered(String initDate, String endDate, String selectedDB, String User, String Client)
        {
            DateTime date = DateTime.Today;

            if ((bool)Session["admin"] != true) selectedDB = null;

            initDate = initDate == null ? date.ToString("yyyy-MM-dd") : initDate;
            endDate = endDate == null ? date.ToString("yyyy-MM") + "-01" : endDate;
            selectedDB = selectedDB == null ? "sucursal" + Session["defaultDB"] : "sucursal" + selectedDB;

            ResponseList<BillsReceivableModel> response = new BillsReceivableDAO().getBills(initDate, endDate, selectedDB, User, Client);
            return PartialView("Filtered", response);
        }

        public ActionResult PaymentHistory(String selectedDB, String idClient)
        {
           
            if ((bool)Session["admin"] != true) selectedDB = null;
            selectedDB = selectedDB == null ? "sucursal" + Session["defaultDB"] : "sucursal" + selectedDB;

            ResponseList<PaymentHistoryModel> response = new BillsReceivableDAO().getHistory(selectedDB, idClient);
            return PartialView("PaymentHistory", response);
        }
    }

}
