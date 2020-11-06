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
        public ResponseList<Order> getOrders(String database, Busqueda busqueda)
        {
            ResponseList<Order> response = new ResponseList<Order>();

            using (IDbConnection db = new SqlConnection(ConfigurationManager.AppSettings[database].ToString()))
            {
                DynamicParameters parameter = new DynamicParameters();
                
                parameter.Add("@fechaInicio", busqueda.startDate.ToString("yyyy-MM-dd"));
                parameter.Add("@fechaFin", busqueda.endDate.ToString("yyyy-MM-dd"));
                parameter.Add("@idStatus", Convert.ToInt32(busqueda.tipoBusqueda));


                var result = db.QueryMultiple("BC_SP_CREA_CONSULTAR_ORDENES_COMPRA", parameter, commandType: CommandType.StoredProcedure);
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
                    int estatus = r1.status;
                    response.status = estatus.ToString();
                    response.msg = r1.error_message;
                    response.model = result.Read<Order>().ToList();
                }
            
            }

            return response;
        }

        public ResponseList<DetalleOrder> getDetalleOrder(String database, String idOrden)
        {
            ResponseList<DetalleOrder> response = new ResponseList<DetalleOrder>();

            using (IDbConnection db = new SqlConnection(ConfigurationManager.AppSettings[database].ToString()))
            {
                DynamicParameters parameter = new DynamicParameters();

                parameter.Add("@idUsuarioOrdenCompra", Convert.ToInt32(idOrden));


                var result = db.QueryMultiple("BC_SP_CREA_OBTENER_PRODUCTOS_ORDEN_COMPRA", parameter, commandType: CommandType.StoredProcedure);
                var r1 = result.ReadFirst();
                if (r1.status == 200)
                {
                    int estatus = r1.status;
                    response.status = estatus.ToString();
                    response.msg = r1.error_message;
                    response.model = result.Read<DetalleOrder>().ToList();
                }
                else
                {
                    int estatus = r1.status;
                    response.status = estatus.ToString();
                    response.msg = r1.error_message;
                    response.model = result.Read<DetalleOrder>().ToList();
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
                    int estatus = r1.status;
                    response.status = estatus.ToString();
                    response.msg = r1.error_message;
                }

            }

            return response;
        }

        public ResponseList<Responce> updateStatusOrders(String database, int idUsuarioOrdenCompra, int idStatusOrdenCompra, string guia)
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

                    if (idStatusOrdenCompra == 4)
                    {
                        Utils.NotificacionPedidoEnviado(r1.nombre, r1.email, guia, idUsuarioOrdenCompra);
                    }
                }
                else
                {
                    int estatus = r1.status;
                    response.status = estatus.ToString();
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
                    int estatus = r1.status;
                    response.status = estatus.ToString();
                    response.msg = r1.error_message;
                }
            }

            return response;
        }

    }
}