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
    public class BillsReceivableDAO
    {
        public ResponseList<BillsReceivableModel> getBills(String initDate, String endDate, String database, String User, String Client)
        {
            ResponseList<BillsReceivableModel> response = new ResponseList<BillsReceivableModel>();

            List<List<BillsReceivableModel>> ResultSets = new List<List<BillsReceivableModel>>();
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
            response.model = new List<BillsReceivableModel>();

            foreach (List<BillsReceivableModel> list in ResultSets)
            {
                if (list != null)
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

        public List<BillsReceivableModel> makeQuery(string initDate, string endDate, string database, string User, string Client)
        {
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

                Func<BillsReceivableModel, bool> filter = null;

                User = User == null ? "-1" : User;
                Client = Client == null ? "-1" : Client;

                if (!User.Equals("-1") && !Client.Equals("-1"))
                    filter = elem => elem.Nombre.Equals(Client) && elem.idUsuario.Equals(User);
                else if (!User.Equals("-1"))
                    filter = elem => elem.idUsuario == Int32.Parse(User);
                else if (!Client.Equals("-1"))
                    filter = elem => elem.idCliente == Int32.Parse(Client);
                else
                    filter = elem => true;

                try
                {
                    List<BillsReceivableModel> Bills = db.Query<BillsReceivableModel>("SPFacturasPorCobrarGeneral", parameter, commandType: CommandType.StoredProcedure).Where(filter).ToList();

                    Bills.ForEach(item => {
                        try
                        {
                            BillDateModel days = db.QuerySingle<BillDateModel>($"select MAX(datediff(day, F.Fecha, C.Fecha)) Days, datediff(day, MAX(F.Fecha), getdate()) DaysSincePurchase, count(C.idFactura) count from Cobros C join Facturas F ON C.idFactura = F.idFactura AND F.idFactura = {item.idFactura}");

                            if (days.count == 0) throw new Exception();
                            if (days.Days <= 60 && item.Saldo == 0) {
                                item.Liquidacion = $"Pagada en {days.Days} Día(s)";
                                item.TipoDistintivo = "bg-success text-white";
                            }
                            else if(item.Saldo == 0 && days.Days > 60)
                            {
                                item.Liquidacion = $"Liquidada con mora en: <br>{days.Days} Día(s).";
                                item.TipoDistintivo = "bg-info text-white";
                            }
                            else if(item.Saldo > 0 && days.DaysSincePurchase <=60)
                            {
                                item.Liquidacion = $"Sin Liquidar dentro de tiempo. {60-days.DaysSincePurchase} Día(s) restantes.";
                                item.TipoDistintivo = "bg-warning text-dark";
                            }
                            else if (item.Saldo > 0 && days.DaysSincePurchase > 60)
                            {
                                item.Liquidacion = $"Sin Liquidar con mora. {days.DaysSincePurchase-60} Día(s) de mora.";
                                item.TipoDistintivo = "bg-danger text-white";
                            }

                        }
                        catch (Exception EX)
                        {
                            item.Liquidacion = "Sin pagos registrados.";
                            item.TipoDistintivo = "bg-dark text-white";
                        }
                    });

                    return Bills;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public ResponseList<PaymentHistoryModel> getHistory(String database, String Client)
        {
            ResponseList<PaymentHistoryModel> response = new ResponseList<PaymentHistoryModel>();

            List<List<PaymentHistoryModel>> ResultSets = new List<List<PaymentHistoryModel>>();
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
                        ResultSets.Add(makeQuery(db, Client));
                    })
                );
            }

            Task.WaitAll(Queries.ToArray());

            response.msg = "success";
            response.status = "success";
            response.alertType = "success";
            response.model = new List<PaymentHistoryModel>();

            foreach (List<PaymentHistoryModel> list in ResultSets)
            {
                if (list != null)
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

        public List<PaymentHistoryModel> makeQuery(string database, string Client)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.AppSettings[database].ToString()))
            {
                DynamicParameters parameter = new DynamicParameters();

                parameter.Add("@FechaInicial", "19000101");
                parameter.Add("@FechaFinal", "30000101");
                parameter.Add("@idEmpresa", 2);
                parameter.Add("@idFormaPago", "0");
                parameter.Add("@numDoc", "");
                parameter.Add("@Filtro", "0");
                parameter.Add("@idComisionista", 0);
                parameter.Add("@idTipoDocumento", 0);
                parameter.Add("@idCuentaBanco", 0);
                parameter.Add("@idCondicionesPago", 4);
                parameter.Add("@idCliente", Client);
                parameter.Add("@idUsuario", 0);


                try
                {
                    return db.Query<PaymentHistoryModel>("SPCobros", parameter, commandType: CommandType.StoredProcedure).ToList();
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

    }
}