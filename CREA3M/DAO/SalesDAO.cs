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
        public ResponseList<SaleModel> getVentas(String initDate, String endDate)
        {
            ResponseList<SaleModel> response = new ResponseList<SaleModel>();

            using (IDbConnection db = new SqlConnection(ConfigurationManager.AppSettings["conexionString"].ToString()))
            {
                DynamicParameters parameter = new DynamicParameters();

                parameter.Add("@FechaInicial", initDate);
                parameter.Add("@FechaFinal", endDate);
                parameter.Add("@idEmpresa", 2);

                try
                {
                    List<SaleModel> result = (List<SaleModel>)db.Query<SaleModel>("SPVentasGlobalesAgrupadasPorCliente", parameter, commandType: CommandType.StoredProcedure);

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