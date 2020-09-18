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
    public class UsersDAO
    {
        public List<User> getUsers(string database)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.AppSettings[database].ToString()))
            {
                try
                {
                    return (List<User>)db.Query<User>(
                        sql: "SELECT idUsuario, Nombre, Apellido_Paterno, Apellido_Materno, NombreCompleto, Usuario FROM Usuarios ORDER BY NombreCompleto",
                        commandType: CommandType.Text
                    );
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public List<SelectListItem> getUsersSelect(string database, string UserSelected = null)
        {
            List<User> Users = getUsers(database);
            List<SelectListItem> UsersSelect = new List<SelectListItem>();

            UserSelected = UserSelected == null ? "-1" : UserSelected;

            UsersSelect.Add(new SelectListItem { Text = "Seleccione un usuario", Value = "-1", Selected = true });

            Users.ForEach(User => UsersSelect.Add(new SelectListItem { Text = User.NombreCompleto, Value = User.IdUsuario.ToString(), Selected = User.IdUsuario == Int32.Parse(UserSelected) }));
            return UsersSelect;
        }
    }
}