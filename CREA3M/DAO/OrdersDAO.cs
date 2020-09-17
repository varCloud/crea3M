using CREA3M.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace CREA3M.DAO
{
    public class OrdersDAO
    {
        public ResponseList<Order> getOrders(String database)
        {
            ResponseList<Order> response = new ResponseList<Order>();

            using (IDbConnection db = new SqlConnection(ConfigurationManager.AppSettings[database].ToString()))
            {

                var result = db.QueryMultiple("BC_SP_CREA_CONSULTAR_ORDENES_COMPRA", null, commandType: CommandType.StoredProcedure);
                var r1 = result.ReadFirst();
                if (r1.status == 200)
                {
                    int estatus = r1.status;
                    response.status = estatus.ToString();
                    response.msg = r1.error_message;
                    response.model = result.Read<Order>().ToList();
                }
                else
                {
                    response.status = r1.status;
                    response.msg = r1.error_message;
                }
            
            }

            return response;
        }

        public ResponseList<CatStatusOrden> getCatStatusOrden(String database)
        {
            ResponseList<CatStatusOrden> response = new ResponseList<CatStatusOrden>();

            using (IDbConnection db = new SqlConnection(ConfigurationManager.AppSettings[database].ToString()))
            {

                var result = db.QueryMultiple("BC_SP_CONSULTAR_CAT_STATUS_ORDEN_COMPRA", null, commandType: CommandType.StoredProcedure);
                var r1 = result.ReadFirst();
                if (r1.status == 200)
                {
                    int estatus = r1.status;
                    response.status = estatus.ToString();
                    response.msg = r1.error_message;
                    response.model = result.Read<CatStatusOrden>().ToList();
                }
                else
                {
                    response.status = r1.status;
                    response.msg = r1.error_message;
                }

            }

            return response;
        }

        public ResponseList<Responce> updateStatusOrders(String database, int idUsuarioOrdenCompra, int idStatusOrdenCompra)
        {
            ResponseList<Responce> response = new ResponseList<Responce>();

            using (IDbConnection db = new SqlConnection(ConfigurationManager.AppSettings[database].ToString()))
            {
                DynamicParameters parameter = new DynamicParameters();

                parameter.Add("@idUsuarioOrdenCompra", idUsuarioOrdenCompra);
                parameter.Add("@idStatusOrdenCompra", idStatusOrdenCompra);

                var result = db.QueryMultiple("BC_SP_CREA_ACTUALIZAR_STATUS_ORDEN_COMPRA", parameter, commandType: CommandType.StoredProcedure);
                var r1 = result.ReadFirst();
                if (r1.status == 200)
                {
                    int estatus = r1.status;
                    response.status = estatus.ToString();
                    response.msg = r1.error_message;
                }
                else
                {
                    response.status = r1.status;
                    response.msg = r1.error_message;
                }
            }

            return response;
        }

        public ResponseList<Responce> EditarGuia(String database, string guia, string idUsuarioOrdenCompra)
        {
            ResponseList<Responce> response = new ResponseList<Responce>();

            using (IDbConnection db = new SqlConnection(ConfigurationManager.AppSettings[database].ToString()))
            {
                DynamicParameters parameter = new DynamicParameters();

                
                parameter.Add("@guiaPaqueteria", guia);
                parameter.Add("@idUsuarioOrdenCompra", Convert.ToInt32(idUsuarioOrdenCompra));

                var result = db.QueryMultiple("BC_SP_CREA_ACTUALIZAR_GUIA_ORDEN_COMPRA", parameter, commandType: CommandType.StoredProcedure);
                var r1 = result.ReadFirst();
                if (r1.status == 200)
                {
                    int estatus = r1.status;
                    response.status = estatus.ToString();
                    response.msg = r1.error_message;
                }
                else
                {
                    response.status = r1.status;
                    response.msg = r1.error_message;
                }
            }

            return response;
        }

    }
}