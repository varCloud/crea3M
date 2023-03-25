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
                parameter.Add("@contrasena", Seguridad.Encriptar(userCustomer.contrasena));

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

        public Response<UserCustomer> getUser(int id)
        {
            Response<UserCustomer> response = new Response<UserCustomer>();

            using (IDbConnection db = new SqlConnection(ConfigurationManager.AppSettings[this.database].ToString()))
            {
                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@idUsuarioEcommerce", id);
                try
                {
                    var result = db.QueryMultiple("BC_SP_CREA_OBTENER_DETALLE_USUARIO", parameter, commandType: CommandType.StoredProcedure);
                    var r1 = result.ReadFirst();
                    if (r1.status == 200)
                    {
                        int estatus = r1.status;
                        response.status = estatus.ToString();
                        response.msg = r1.error_message;
                        response.model = result.ReadFirst<UserCustomer>();
                        response.model.contrasena = Seguridad.DesEncriptar(response.model.contrasena);
                    }
                    else
                    {
                        int estatus = r1.status;
                        response.status = estatus.ToString();
                        response.msg = r1.error_message;
                        response.model = null;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }

            return response;
        }

        public Response<Responce> updateStatusUser(int idUsuarioEcommerce, bool activo)
        {
            Response<Responce> response = new Response<Responce>();
            using (IDbConnection db = new SqlConnection(ConfigurationManager.AppSettings[this.database].ToString()))
            {
                DynamicParameters parameter = new DynamicParameters();

                parameter.Add("@idUsuarioEcommerce", idUsuarioEcommerce);
                parameter.Add("@activo", activo);

                try
                {
                    var result = db.QueryMultiple("BC_SP_CREA_ACTIVAR_USUARIO_ECOMMERCE_ADMIN", parameter, commandType: CommandType.StoredProcedure);
                    var r1 = result.ReadFirst();

                    int estatus = r1.status;
                    response.status = estatus.ToString();
                    response.msg = r1.error_message;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }

            return response;

        }
    }
}
