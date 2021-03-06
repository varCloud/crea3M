﻿using CREA3M.Helpers;
using CREA3M.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CREA3M.DAO
{
    public class SalesDAO
    {
        public ResponseList<SaleModel> getVentas(String initDate, String endDate, String database, String User, String Client)
        {
            ResponseList<SaleModel> response = new ResponseList<SaleModel>();

            List<List<SaleModel>> ResultSets = new List<List<SaleModel>>();
            List<string> Databases = new List<string>();

            if (database.Equals("sucursalALL"))
                Databases = sucursales._SUCURSALES;
            else
                Databases.Add(database);

            List<Task> Queries = new List<Task>();

            foreach (string db in Databases)
            {
                Queries.Add(
                    Task.Factory.StartNew(() =>
                    {
                        ResultSets.Add(makeQuery(initDate, endDate, db, User, Client));
                    })
                );
            }

            Task.WaitAll(Queries.ToArray());

            response.msg = "success";
            response.status = "success";
            response.alertType = "success";
            response.model = new List<SaleModel>();

            foreach(List<SaleModel> list in ResultSets)
            {
                if(list != null)
                {
                    response.model.AddRange(list);
                }
                else
                {
                    response.model = null;
                    response.msg = "Error durante la ejecucion";
                    response.status = "failure";
                    response.alertType = "error";
                    break;
                }
            }

            return response;
        }

        public List<SaleModel> makeQuery(string initDate, string endDate, string database, string User, string Client)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.AppSettings[database].ToString()))
            {
                DynamicParameters parameter = new DynamicParameters();

                parameter.Add("@FechaInicial", initDate);
                parameter.Add("@FechaFinal", endDate);
                parameter.Add("@idEmpresa", 2);
                parameter.Add("@idTipoDocumento", 5);
                parameter.Add("@idComisionista", 0);
                parameter.Add("@FolioInicial", 0);
                parameter.Add("@FolioFinal", 99999999);
                parameter.Add("@Facturado", -1);
                parameter.Add("@idCondicionesPago", 0);
                parameter.Add("@idProcesoTempladora", 0);
                parameter.Add("@idGiroComercial", 0);
                parameter.Add("@idUsuario", User);

                Func<SaleModel, bool> filter = null;

                User = User == null ? "-1" : User;
                Client = Client == null ? "0" : Client;

                if (!Client.Equals("0"))
                    filter = elem => elem.ClienteNombre.Equals(Client);
                else
                    filter = elem => true;

                try
                {
                    List<SaleModel> Sales = db.Query<SaleModel>("SPVentasNoCFDiPorFecha", parameter, commandType: CommandType.StoredProcedure).Where(filter).ToList();
                    List<UtilidadModel> Utilities = db.Query<UtilidadModel>($"select F.idFactura,MAX(F.SubTotalAntesDescuento) - MAX(F.ImporteDescuento) + MAX(F.ImporteIVA) + isnull(MAX(F.ImporteIEPS), 0) - isnull(MAX(F.ImporteRetIVA),0) - isnull(MAX(F.ImporteISR),0)-isnull(MAX(F.ImporteIMCD),0)+isnull(MAX(F.ImporteISSH),0) - SUM(COALESCE(FD.PrecioCompra, 0) * FD.Cantidad) Utilidad from FacturasDetalle FD JOIN Facturas F ON FD.idFactura = F.idFactura JOIN Productos P ON FD.idProducto = P.idProducto AND F.Fecha BETWEEN '{initDate}' AND '{endDate}' GROUP BY F.idFactura ORDER BY F.idFactura ASC").ToList();

                    Dictionary<long, double> UtilitiesDic = new Dictionary<long, double>();
                    Utilities.ForEach(item => UtilitiesDic.Add(item.idFactura, item.Utilidad));

                    Sales.ForEach(item => {
                        double utilidad = 0;
                        UtilitiesDic.TryGetValue(item.idFactura, out utilidad);                       
                        item.Utilidad = utilidad == 0 ? item.Total: utilidad;
                    });

                    return Sales;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
    }
}