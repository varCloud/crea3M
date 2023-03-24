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
        string database = "ecommerce";
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

            UsersSelect.Add(new SelectListItem { Text = "Seleccione un usuario", Value = "0", Selected = true });

            Users.ForEach(User => UsersSelect.Add(new SelectListItem { Text = User.NombreCompleto, Value = User.IdUsuario.ToString(), Selected = User.IdUsuario == Int32.Parse(UserSelected) }));
            return UsersSelect;
        }

        public Responce updateUser(UserCustomer userCustomer)
        {
            Responce respuesta = new Responce();

            using (IDbConnection db = new SqlConnection(ConfigurationManager.AppSettings[this.database].ToString()))
            {
                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@idUsuarioEcommerce", userCustomer.idUserEcommerce);
                parameter.Add("@nombre", userCustomer.nombre);
                parameter.Add("@celular", userCustomer.celular);
                parameter.Add("@email", userCustomer.email);
                parameter.Add("@contrasena", "YwBsAGkAZQBuAHQAZQA=");

                try
                {
                    respuesta = db.QuerySingle<Responce>("BC_SP_CREA_EDITAR_USUARIO_ADMIN", parameter, commandType: CommandType.StoredProcedure);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }

            return respuesta;
        }
    }
}