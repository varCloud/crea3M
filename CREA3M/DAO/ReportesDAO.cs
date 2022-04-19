using CREA3M.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CREA3M.DAO
{
    public class ReportesDAO
    {
        string database = "ecommerce";
        private IDbConnection db = null;

        public List<SelectListItem> ObtenerMeses(int anio)
        {
            List<SelectListItem> lstMeses = new List<SelectListItem>();
            try
            {
                using (db = new SqlConnection(ConfigurationManager.AppSettings[this.database].ToString()))
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@anio", anio == 0 ? (Object)null : anio);
                    var result = db.QueryMultiple("BC_SP_CONSULTA_MESES", parameters, commandType: CommandType.StoredProcedure);
                    var r1 = result.ReadFirst();

                    if (r1.status == 200)
                    {
                        lstMeses = result.Read<SelectListItem>().ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstMeses;
        }

        public List<SelectListItem> ObtenerAnios()
        {
            List<SelectListItem> lstAnios = new List<SelectListItem>();
            try
            {
                using (db = new SqlConnection(ConfigurationManager.AppSettings[this.database].ToString()))
                {

                    var result = db.QueryMultiple("BC_SP_CONSULTA_ANIOS", null, commandType: CommandType.StoredProcedure);
                    var r1 = result.ReadFirst();

                    if (r1.status == 200)
                    {
                        lstAnios = result.Read<SelectListItem>().ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstAnios;
        }

        public List<SelectListItem> ObtenerTiposReportes()
        {
            List<SelectListItem> lstAnios = new List<SelectListItem>();
            try
            {
                lstAnios.Add(new SelectListItem() { Text = "Producto que más se vende", Value = "1" });
                lstAnios.Add(new SelectListItem() { Text = "Producto que menos se vende", Value = "2" });
                lstAnios.Add(new SelectListItem() { Text = "Ventas por Estado", Value = "3" });
                lstAnios.Add(new SelectListItem() { Text = "Promedio de venta por cliente", Value = "4" });

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstAnios;
        }

        public List<SelectListItem> ObtenerTiposReportesUsuarios()
        {
            List<SelectListItem> lstAnios = new List<SelectListItem>();
            try
            {
                lstAnios.Add(new SelectListItem() { Text = "Registrados", Value = "5" , Selected = true });
                lstAnios.Add(new SelectListItem() { Text = "Suscripciones", Value = "6" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstAnios;
        }

        public List<ReporteProducto> ObtenerTiposReportes(FiltroReporte filtro)
        {
            List<ReporteProducto> listResultado = new List<ReporteProducto>();
            try
            {
                using (db = new SqlConnection(ConfigurationManager.AppSettings[this.database].ToString()))
                {
                    DynamicParameters parameter = new DynamicParameters();

                    parameter.Add("@mes", filtro.mes);
                    parameter.Add("@ano", filtro.ano);
                    parameter.Add("@idTipoReporte", filtro.tipoReporte);
                    var result = db.QueryMultiple("BC_SP_OBTENER_REPORTE_VENTAS", parameter, commandType: CommandType.StoredProcedure);
                    var r1 = result.ReadFirst();
                    if (r1.estatus == 200)
                    {
                        listResultado = result.Read<ReporteProducto>().ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listResultado;
        }

        public List<UsuarioEcommerce> ObtenerUsuarioReportes(FiltroReporte filtro)
        {
            List<UsuarioEcommerce> listResultado = new List<UsuarioEcommerce>();
            try
            {
                using (db = new SqlConnection(ConfigurationManager.AppSettings[this.database].ToString()))
                {
                    DynamicParameters parameter = new DynamicParameters();

                    parameter.Add("@fechaInicio", filtro.fechaInicio);
                    parameter.Add("@fechaFin", filtro.fechaFin);
                    parameter.Add("@idTipoReporte", filtro.tipoReporte);
                    var result = db.QueryMultiple("BC_SP_OBTENER_USUARIOS", parameter, commandType: CommandType.StoredProcedure);
                    var r1 = result.ReadFirst();
                    if (r1.estatus == 200)
                    {
                        listResultado = result.Read<UsuarioEcommerce>().ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listResultado;
        }
    }
}