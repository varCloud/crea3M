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
    public class LoginDAO
    {
    
        public Response<LoginModel> login(LoginModel Credentials)
        {
            Response<LoginModel> response = new Response<LoginModel>();

            using (IDbConnection db = new SqlConnection(ConfigurationManager.AppSettings["sucursal"+Credentials.defaultDB].ToString()))
            {
                DynamicParameters parameter = new DynamicParameters();

                parameter.Add("@Usuario", Credentials.Usuario);
                parameter.Add("@Pass", Credentials.Password);
                try {
                    LoginModel result = db.QuerySingle<LoginModel>("SPUsuarioLogin", parameter, commandType: CommandType.StoredProcedure);

                    response.msg = "success";
                    response.status = "success";
                    response.alertType = "success";
                    response.model = result;
                    response.model.Token = Credentials.Token;
                }
                catch (Exception)
                {
                    response.msg = "Credenciales incorrectas";
                    response.status = "failure";
                    response.alertType = "error";
                }
            }

            return response;
        }
    }
}