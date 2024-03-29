﻿using CREA3M.DAO;
using CREA3M.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CREA3M.Controllers
{
    public class ReportesController : Controller
    {
        // GET: Reportes
        public ActionResult Reportes()
        {
            ViewBag.listMeses = new ReportesDAO().ObtenerMeses(0);
            ViewBag.listAnios = new ReportesDAO().ObtenerAnios();
            ViewBag.listReportes = new ReportesDAO().ObtenerTiposReportes();
            return View();
        }

        public ActionResult _ObtenerTipoReporte(FiltroReporte filtro) {
            try
            {
                ViewBag.tipoReporte = filtro.tipoReporte;
                ViewBag.resultadoReporte = new ReportesDAO().ObtenerTiposReportes(filtro);
                return PartialView("_ObtenerTipoReporte");
            }
            catch (Exception ex)
            {

                return Json(new ResponseGeneral<string>() { estatus = -1, mensaje = "[" + ex.Message + "]", modelo = ex.StackTrace }, JsonRequestBehavior.AllowGet);
            }
            
        }
    }
}