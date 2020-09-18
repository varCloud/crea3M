using CREA3M.DAO;
using CREA3M.Helpers;
using CREA3M.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CREA3M.Controllers
{
    public class SalesController : Controller
    {


        // GET: Ventas
        public ActionResult Index()
        {
            if (validation.validateSession(Session))
            {

                ViewBag.username = Session["username"];
                ViewBag.sucursales = sucursales.buildList(Session["defaultDB"].ToString());
                ViewBag.localidades = new ClientsDAO().getCitiesSelect("sucursal" + Session["defaultDB"].ToString());
                ViewBag.usuarios = new UsersDAO().getUsersSelect("sucursal" + Session["defaultDB"].ToString());
                ViewBag.clientes = new List<SelectListItem> { new SelectListItem { Text = "Seleccione una localidad primero", Value = "-1" } };

                return View();
            }

            return Redirect("/Login/Index?nosession=1");
        }

        public ActionResult Filtered(String initDate, String endDate, String selectedDB, String User, String Client)
        {
            if (!validation.validateSession(Session))
            {
                return Redirect("/Login/Index?nosession=1");
            }

            DateTime date = DateTime.Today;
            initDate = initDate == null ? date.ToString("yyyy-MM-dd") : initDate;
            endDate = endDate == null ? date.ToString("yyyy-MM") + "-01" : endDate;
            selectedDB = selectedDB == null ? "sucursal" + Session["defaultDB"] : "sucursal" + selectedDB;

            ResponseList<SaleModel> response = new SalesDAO().getVentas(initDate, endDate, selectedDB, User, Client);
            return PartialView("Filtered", response);
        }

        public ActionResult BranchOfficeChange(String Database, String City, String User)
        {
            if (!validation.validateSession(Session))
            {
                return Redirect("/Login/Index?nosession=1");
            }

            Database = Database == null ? "sucursal" + Session["defaultDB"] : "sucursal" + Database;

            if (Database.Equals("sucursalALL"))
            {
                ViewBag.localidades = new List<SelectListItem> { new SelectListItem { Text = "Seleccione una sucursal primero", Value = "-1" } };
                ViewBag.usuarios = new List<SelectListItem> { new SelectListItem { Text = "Seleccione una sucursal primero", Value = "-1" } };
                ViewBag.clientes = new List<SelectListItem> { new SelectListItem { Text = "Seleccione una sucursal primero", Value = "-1" } };
            }
            else
            {

                ViewBag.localidades = new ClientsDAO().getCitiesSelect(Database, City);
                ViewBag.usuarios = new UsersDAO().getUsersSelect(Database, User);

                if (City == null)
                {
                    ViewBag.clientes = new List<SelectListItem> { new SelectListItem { Text = "Seleccione una localidad", Value = "-1" } };
                }
                else
                {
                    ViewBag.clientes = new ClientsDAO().getClientsSelect(Database, City);

                }

            }

            return PartialView("BranchOfficeChange");
        }
    }
}