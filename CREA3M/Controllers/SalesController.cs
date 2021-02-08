using CREA3M.DAO;
using CREA3M.Filters;
using CREA3M.Helpers;
using CREA3M.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CREA3M.Controllers
{
    [SessionTimeout]
    public class SalesController : Controller
    {


        // GET: Ventas
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

            ResponseList<SaleModel> response = new SalesDAO().getVentas(initDate, endDate, selectedDB, User, Client);
            return PartialView("Filtered", response);
        }

        public ActionResult BranchOfficeChange(String Database, String City, String User, String idClient)
        {

            if ((bool)Session["admin"] != true) Database = null;
            Database = Database == null ? "sucursal" + Session["defaultDB"] : "sucursal" + Database;

            if (Database.Equals("sucursalALL"))
            {
                ViewBag.localidades = new List<SelectListItem> { new SelectListItem { Text = "Seleccione una sucursal primero", Value = "-1" } };
                ViewBag.usuarios = new List<SelectListItem> { new SelectListItem { Text = "Seleccione una sucursal primero", Value = "0" } };
                ViewBag.clientes = new List<SelectListItem> { new SelectListItem { Text = "Seleccione una sucursal primero", Value = "0" } };
            }
            else
            {

                ViewBag.localidades = new ClientsDAO().getCitiesSelect(Database, City);
                ViewBag.usuarios = new UsersDAO().getUsersSelect(Database, User);

                if (City == null)
                {
                    ViewBag.clientes = new List<SelectListItem> { new SelectListItem { Text = "Seleccione una localidad", Value = "0" } };
                }
                else
                {
                    ViewBag.clientes = new ClientsDAO().getClientsSelect(Database, City, idClient);

                }

            }

            return PartialView("BranchOfficeChange");
        }
    }
}