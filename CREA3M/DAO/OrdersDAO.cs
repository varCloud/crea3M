using CREA3M.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace CREA3M.DAO
{
    public class OrdersDAO
    {
        public ResponseList<Order> getOrders(String database)
        {
            ResponseList<Order> response = new ResponseList<Order>();

            using (IDbConnection db = new SqlConnection(ConfigurationManager.AppSettings[database].ToString()))
            {

                try
                {
                    List<Order> result = (List<Order>)db.Query<Order>("BC_SP_CREA_CONSULTAR_ORDENES_COMPRA", null , commandType: CommandType.StoredProcedure);

                    response.msg = "success";
                    response.status = "success";
                    response.alertType = "success";
                    response.model = result == null ? new List<Order>() : result;

                }
                catch (Exception ex)
                {
                    response.msg = "Error durante la ejecucion";
                    response.status = "failure";
                    response.alertType = "error";
                    Console.WriteLine(ex.ToString());
                    response.model = new List<Order>();
                }
            }

            return response;
        }

        public Responce updateStatusOrders(String database, int idUsuarioOrdenCompra, int idStatusOrdenCompra)
        {
            Responce response = new Responce();

            using (IDbConnection db = new SqlConnection(ConfigurationManager.AppSettings[database].ToString()))
            {
                DynamicParameters parameter = new DynamicParameters();

                parameter.Add("@idUsuarioOrdenCompra", idUsuarioOrdenCompra);
                parameter.Add("@idStatusOrdenCompra", idStatusOrdenCompra);
              
                try
                {
                    response = (Responce)db.Query("BC_SP_CREA_ACTUALIZAR_STATUS_ORDEN_COMPRA", parameter, commandType: CommandType.StoredProcedure);

                }
                catch (Exception ex)
                {
                    
                }
            }

            return response;
        }

    }
}