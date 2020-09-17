using CREA3M.Helpers;
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
        public ResponseList<SaleModel> getVentas(String initDate, String endDate, String database)
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
                        ResultSets.Add(makeQuery(initDate, endDate, db));
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

        public List<SaleModel> makeQuery(string initDate, string endDate, string database)
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
                parameter.Add("@idUsuario", 0);

                try
                {
                    return (List<SaleModel>)db.Query<SaleModel>("SPVentasNoCFDiPorFecha", parameter, commandType: CommandType.StoredProcedure);
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
    }
}