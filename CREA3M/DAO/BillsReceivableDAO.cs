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
    public class BillsReceivableDAO
    {
        public ResponseList<BillsReceivableModel> getBills(String initDate, String endDate, String database)
        {
            ResponseList<BillsReceivableModel> response = new ResponseList<BillsReceivableModel>();

            using (IDbConnection db = new SqlConnection(ConfigurationManager.AppSettings[database].ToString()))
            {
                DynamicParameters parameter = new DynamicParameters();

                parameter.Add("@FechaInicial", initDate);
                parameter.Add("@FechaFinal", endDate);
                parameter.Add("@idEmpresa", 2);
                parameter.Add("@idCliente", 0);
                parameter.Add("@idTipoDocumento", 0);
                parameter.Add("@idComisionista", 0);
                parameter.Add("@idFacturasListar", "");
                parameter.Add("@idFormaPago", "0");
                parameter.Add("@idMetodoPago", "0");
                parameter.Add("@idCondicionesPago", 0);
                parameter.Add("@idUsuario", 0);
                parameter.Add("@Localidad", "%%");

                try
                {
                    List<BillsReceivableModel> result = (List<BillsReceivableModel>)db.Query<BillsReceivableModel>("SPFacturasPorCobrarGeneral", parameter, commandType: CommandType.StoredProcedure);

                    response.msg = "success";
                    response.status = "success";
                    response.alertType = "success";
                    response.model = result == null ? new List<BillsReceivableModel>() : result;

                }
                catch (Exception ex)
                {
                    response.msg = "Error durante la ejecucion";
                    response.status = "failure";
                    response.alertType = "error";
                    Console.WriteLine(ex.ToString());
                    response.model = new List<BillsReceivableModel>();
                }
            }

            return response;
        }
    }
}