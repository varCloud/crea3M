using CREA3M.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CREA3M.DAO
{
    public class Login
    {
        private DBManager db = null;

        public Response<LoginModel> validateUser(LoginModel Credentials)
        {
            Response<LoginModel> toReturn = null;
            try
            {
                using (db = new DBManager(ConfigurationManager.AppSettings["conexionString"].ToString()))
                {
                    db.Open();
                    db.CreateParameters(2);
                    db.AddParameters(0, "@Usuario", Credentials.username);
                    db.AddParameters(1, "@Pass", Credentials.password);
                    db.ExecuteReader(System.Data.CommandType.StoredProcedure, "SP_VALIDAR_USUARIO");
                    if (db.DataReader.Read())
                    {
                        if (Convert.ToInt32(db.DataReader["estatus"].ToString()) == 200)
                        {
                            toReturn = new Response<LoginModel>();
                            toReturn.status = Convert.ToInt32(db.DataReader["estatus"]);
                            toReturn.msg = db.DataReader["mensaje"].ToString();
                            toReturn.alertType = "success";
                            //n.Model = sesion;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}