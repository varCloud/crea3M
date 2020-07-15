using CREA3M.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CREA3M.DAO
{
    public class SalesDAO
    {
        public ResponseList<SaleModel> getVentas(String initDate, String endDate, String database)
        {
            ResponseList<SaleModel> response = new ResponseList<SaleModel>();

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
                    List<SaleModel> result = (List<SaleModel>)db.Query<SaleModel>("SPVentasNoCFDiPorFecha", parameter, commandType: CommandType.StoredProcedure);

                    response.msg = "success";
                    response.status = "success";
                    response.alertType = "success";
                    response.model = result == null ? new List<SaleModel>() : result;

                }
                catch (Exception ex)
                {
                    response.msg = "Error durante la ejecucion";
                    response.status = "failure";
                    response.alertType = "error";
                    Console.WriteLine(ex.ToString());
                    response.model = new List<SaleModel>();
                }
            }

            return response;
        }
    }
}