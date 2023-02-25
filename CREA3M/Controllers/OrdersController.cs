using CREA3M.DAO;
using CREA3M.Filters;
using CREA3M.Helpers;
using CREA3M.Models;
using System;
using System.Diagnostics;
using System.Web.Mvc;

namespace CREA3M.Controllers
{
    [SessionTimeout]
    public class OrdersController : Controller
    {
        OrdersDAO ordersDAO;
        
        // GET: Orders

        public ActionResult Index()
        {
           
            ViewBag.username = Session["username"];
            string selectedDB = "sucursal" + Session["defaultDB"];
            ResponseList<CatStatusOrden> responseCat = new OrdersDAO().getCatStatusOrden(selectedDB);
            ViewBag.catStatusOrden = responseCat.model;
            return View();
        }

        [HttpPost]
        public ActionResult _orders(Busqueda busqueda)
        {
            string selectedDB = "sucursal" + Session["defaultDB"];
            ResponseList<Order> response = new OrdersDAO().getOrders(selectedDB, busqueda);
            ViewBag.orders = response.model;
            return PartialView();
        }

        [HttpPost]
        public ActionResult _detalleOrders(String idOrden)
        {
            
            ResponseList<DetalleOrder> response = new OrdersDAO().getDetalleOrder(idOrden);
            ViewBag.detalleOrders = response.model;
            return PartialView();
        }

        public ActionResult EditarStatus(int idUsuarioOrdenCompra, int idStatusOrdenCompra, string guia)
        {
            try
            {
                ordersDAO = new OrdersDAO();
                string selectedDB = "sucursal" + Session["defaultDB"];
                return Json(ordersDAO.updateStatusOrders(selectedDB, idUsuarioOrdenCompra, idStatusOrdenCompra, guia), JsonRequestBehavior.AllowGet);
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

        [HttpPost]
        public ActionResult AgregarEntregadoPor(string idOrdenCompra, string entregadoPor , string observaciones)
        {
            try
            {
                
                Debug.WriteLine("entre");
                ordersDAO = new OrdersDAO();
                string selectedDB = "sucursal" + Session["defaultDB"];
                return Json(ordersDAO.EditarEntregadoPor(selectedDB, entregadoPor, observaciones, idOrdenCompra), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}