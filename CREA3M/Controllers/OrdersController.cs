using CREA3M.DAO;
using CREA3M.Helpers;
using CREA3M.Models;
using System;
using System.Web.Mvc;

namespace CREA3M.Controllers
{
    public class OrdersController : Controller
    {
        OrdersDAO ordersDAO;
        
        // GET: Orders

        public ActionResult Index()
        {
           
            ViewBag.username = Session["username"];
            string selectedDB = "sucursal" + Session["defaultDB"];
            ResponseList<Order> response = new OrdersDAO().getOrders(selectedDB);
            ViewBag.orders = response.model;
            ResponseList<CatStatusOrden> responseCat = new OrdersDAO().getCatStatusOrden(selectedDB);
            ViewBag.catStatusOrden = responseCat.model;
            return View();
        }

        public ActionResult EditarStatus(int idUsuarioOrdenCompra, int idStatusOrdenCompra)
        {
            try
            {
                ordersDAO = new OrdersDAO();
                string selectedDB = "sucursal" + Session["defaultDB"];
                return Json(ordersDAO.updateStatusOrders(selectedDB, idUsuarioOrdenCompra, idStatusOrdenCompra), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult EditarGuia(string guia, string orden)
        {
            try
            {
                ordersDAO = new OrdersDAO();
                string selectedDB = "sucursal" + Session["defaultDB"];
                return Json(ordersDAO.EditarGuia(selectedDB, guia, orden), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}