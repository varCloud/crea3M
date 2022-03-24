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
    public class SliderDAO
    {
        string database = "ecommerce";
        
        public ResponseGeneral<List<ImagenSlider>> obtenerImagenes(int idImagen)
        {
            ResponseGeneral<List<ImagenSlider>> response = new ResponseGeneral<List<ImagenSlider>>();
            using (IDbConnection db = new SqlConnection(ConfigurationManager.AppSettings[this.database].ToString()))
            {
                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@idImagen", Convert.ToInt32(idImagen) == 0 ? (object)null : (object)idImagen);
                try
                {
                    var result = db.QueryMultiple("BC_SP_OBTENER_IMAGENES", parameter, commandType: CommandType.StoredProcedure);
                    var r1 = result.ReadFirst();
                    response.estatus = r1.estatus;
                    response.mensaje = r1.mensaje;
                    if (r1.estatus == 200)
                    {
                        response.modelo = result.Read<ImagenSlider>().ToList();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    throw ex;

                }
            }

            return response;
        }

        public ResponseGeneral<List<ImagenSlider>> insertaImagen(ImagenSlider imagen)
        {
            ResponseGeneral<List<ImagenSlider>> response = new ResponseGeneral<List<ImagenSlider>>();
            using (IDbConnection db = new SqlConnection(ConfigurationManager.AppSettings[this.database].ToString()))
            {
                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@path", imagen.path);
                parameter.Add("@size", imagen.size);
                parameter.Add("@nombre", imagen.nombre);
                parameter.Add("@server", ConfigurationManager.AppSettings["server"].ToString());
                try
                {
                    var result = db.QueryMultiple("BC_SP_INSERTA_IMAGEN_SLIDER", parameter, commandType: CommandType.StoredProcedure);
                    var r1 = result.ReadFirst();
                    response.estatus = r1.estatus;
                    response.mensaje = r1.mensaje;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    throw ex;

                }
            }

            return response;
        }

        public ResponseGeneral<String> EliminarImagen(int idIamegn)
        {
            ResponseGeneral<String> response = new ResponseGeneral<String>();
            using (IDbConnection db = new SqlConnection(ConfigurationManager.AppSettings[this.database].ToString()))
            {
                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@idImagen", idIamegn);
                try
                {
                    var result = db.QueryMultiple("BC_SP_ELIMINAR_IMAGENES", parameter, commandType: CommandType.StoredProcedure);
                    var r1 = result.ReadFirst();
                    response.estatus = r1.estatus;
                    response.mensaje = r1.mensaje;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    throw ex;

                }
            }

            return response;
        }

        public ResponseGeneral<String> DesactivarImagen(int idIamegn ,Boolean estatus)
        {
            ResponseGeneral<String> response = new ResponseGeneral<String>();
            using (IDbConnection db = new SqlConnection(ConfigurationManager.AppSettings[this.database].ToString()))
            {
                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@idImagen", idIamegn);
                parameter.Add("@activo", estatus);
                try
                {
                    var result = db.QueryMultiple("BC_SP_DESACTIVAR_IMAGEN", parameter, commandType: CommandType.StoredProcedure);
                    var r1 = result.ReadFirst();
                    response.estatus = r1.estatus;
                    response.mensaje = r1.mensaje;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    throw ex;

                }
            }

            return response;
        }
    }
}