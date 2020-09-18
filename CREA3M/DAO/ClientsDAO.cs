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
    public class ClientsDAO
    {

        public List<ClientShort> getClients(string database, string city)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.AppSettings[database].ToString()))
            {
                try
                {
                    return (List<ClientShort>)db.Query<ClientShort>(
                        sql: $"SELECT idCliente, Nombre, RFC FROM Cliente WHERE Localidad LIKE '{city}' ORDER BY Nombre",
                        commandType: CommandType.Text
                    );
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public List<SelectListItem> getClientsSelect(string database, string filter)
        {

            List<ClientShort> Clients = getClients(database, filter);
            List<SelectListItem> ClientsSelect = new List<SelectListItem>();

            ClientsSelect.Add(new SelectListItem { Text = "Seleccione una localidad o vendedor primero", Value = "-1", Selected = true });

            Clients.ForEach(Client => ClientsSelect.Add(new SelectListItem { Text = Client.Nombre, Value = Client.Nombre, Selected = false }));
            return ClientsSelect;
        }       

        public List<City> getCities( string database)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.AppSettings[database].ToString()))
            {
                try
                {
                    return (List<City>)db.Query<City>(
                        sql: "SELECT DISTINCT Localidad FROM Cliente ORDER BY Localidad",
                        commandType: CommandType.Text
                    );
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public List<SelectListItem> getCitiesSelect(string database, string city = null)
        {
            List<City> Cities = getCities(database);
            List<SelectListItem> CitiesSelect = new List<SelectListItem>();

            city = city == null ? String.Empty : city;

            CitiesSelect.Add(new SelectListItem { Text = "Seleccione una localidad", Value = "-1", Selected = true });

            Cities.ForEach(City => CitiesSelect.Add(new SelectListItem { Text = City.Localidad, Value = City.Localidad, Selected = City.Localidad == city }));
            return CitiesSelect;
        }

    }
}