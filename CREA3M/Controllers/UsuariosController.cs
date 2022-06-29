
using CREA3M.DAO;
using CREA3M.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CREA3M.Controllers
{
    public class UsuariosController : Controller
    {
        // GET: Usuarios
        public ActionResult Usuarios()
        {
            ViewBag.listReportes = new ReportesDAO().ObtenerTiposReportesUsuarios();
            return View();
        }

        public ActionResult _obtenerUsuarios(FiltroReporte filtro)
        {
            try
            {
                ViewBag.tipoReporte = filtro.tipoReporte;
                ViewBag.resultadoReporte = new ReportesDAO().ObtenerUsuarioReportes(filtro);
                return PartialView("_obtenerUsuarios");

            }
            catch (Exception ex)
            {
                return Json(new ResponseGeneral<string>() { estatus = -1, mensaje = "[" + ex.Message + "]", modelo = ex.StackTrace }, JsonRequestBehavior.AllowGet);
            }

        }
    }
}