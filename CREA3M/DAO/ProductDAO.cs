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
    public class ProductDAO
    {
        public ResponseList<Product> getProductos(String database, int idMarca, int idCategoria)
        {
            ResponseList<Product> response = new ResponseList<Product>();

            using (IDbConnection db = new SqlConnection(ConfigurationManager.AppSettings[database].ToString()))
            {
                DynamicParameters parameter = new DynamicParameters();


                parameter.Add("@IdMarca", idMarca);
                parameter.Add("@Categoria", idCategoria);

                var result = db.QueryMultiple("BC_SP_CREA_OBTENER_PRODUCTOS_MARCA_ADMIN", parameter, commandType: CommandType.StoredProcedure);
                var r1 = result.ReadFirst();
                if (r1.status == 200)
                {
                    int estatus = r1.status;
                    response.status = estatus.ToString();
                    response.msg = r1.error_message;
                    response.model = result.Read<Product>().ToList();
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

        public ResponseList<Categoria> getCatEcommerce(String database, int idMarca)
        {
            ResponseList<Categoria> response = new ResponseList<Categoria>();

            using (IDbConnection db = new SqlConnection(ConfigurationManager.AppSettings[database].ToString()))
            {
                DynamicParameters parameter = new DynamicParameters();


                parameter.Add("@IdMarca", idMarca);

                var result = db.QueryMultiple("BC_SP_CREA_OBTENER_CAT_ECOMMERCE", parameter, commandType: CommandType.StoredProcedure);
                var r1 = result.ReadFirst();
                if (r1.status == 200)
                {
                    int estatus = r1.status;
                    response.status = estatus.ToString();
                    response.msg = r1.error_message;
                    response.model = result.Read<Categoria>().ToList();
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

        public ResponseList<PathImgProduct> getProductosImg(string idProductoEcommerce, String database)
        {
            ResponseList<PathImgProduct> response = new ResponseList<PathImgProduct>();

            using (IDbConnection db = new SqlConnection(ConfigurationManager.AppSettings[database].ToString()))
            {
                DynamicParameters parameter = new DynamicParameters();


                parameter.Add("@IdProductoEcommerce", Convert.ToInt32(idProductoEcommerce));

                var result = db.QueryMultiple("BC_SP_CREA_OBTENER_PATH_IMAGENES_PRODUCTO", parameter, commandType: CommandType.StoredProcedure);
                var r1 = result.ReadFirst();
                if (r1.status == 200)
                {
                    int estatus = r1.status;
                    response.status = estatus.ToString();
                    response.msg = r1.error_message;
                    response.model = result.Read<PathImgProduct>().ToList();
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


        public ResponseList<Responce> insertImg(string pathImg, String database, string idProduct)
        {
            ResponseList<Responce> response = new ResponseList<Responce>();

            using (IDbConnection db = new SqlConnection(ConfigurationManager.AppSettings[database].ToString()))
            {
                DynamicParameters parameter = new DynamicParameters();

                parameter.Add("@PathImagen", pathImg);
                parameter.Add("@IdProductoEcommerce", Convert.ToInt32(idProduct));

                try
                {
                    var result = db.QueryMultiple("BC_SP_CREA_REGISTRAR_PATH_IMAGEN_PRODUCTO", parameter, commandType: CommandType.StoredProcedure);
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());

                }
            }

            return response;
        }

        public ResponseList<Responce> deleteImgProduct(string idProduct,string pathImg, string selectedDB)
        {
            ResponseList<Responce> response = new ResponseList<Responce>();

            using (IDbConnection db = new SqlConnection(ConfigurationManager.AppSettings[selectedDB].ToString()))
            {
                DynamicParameters parameter = new DynamicParameters();

                parameter.Add("@IdProductoEcommerce", Convert.ToInt32(idProduct));
                parameter.Add("@PathImagen", pathImg);

                try
                {
                    var result = db.QueryMultiple("BC_SP_CREA_ELIMINAR_PATH_IMAGEN_PRODUCTO", parameter, commandType: CommandType.StoredProcedure);
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());

                }
            }

            return response;
        }

        public ResponseList<detalleProducto>  getProduct(string id, String database)
        {
            ResponseList<detalleProducto> response = new ResponseList<detalleProducto>();

            using (IDbConnection db = new SqlConnection(ConfigurationManager.AppSettings[database].ToString()))
            {
                DynamicParameters parameter = new DynamicParameters();

                parameter.Add("@idProductoEcommerce", id);

                try
                {
                    
                    var result = db.QueryMultiple("BC_SP_CREA_OBTENER_DETALLE_PRODUCTO_ADMIN", parameter, commandType: CommandType.StoredProcedure);
                    var r1 = result.ReadFirst();
                    if (r1.status == 200)
                    {
                        int estatus = r1.status;
                        response.status = estatus.ToString();
                        response.msg = r1.error_message;
                        response.model = result.Read<detalleProducto>().ToList();
                    }
                    else
                    {
                        int estatus = r1.status;
                        response.status = estatus.ToString();
                        response.msg = r1.error_message;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }

            return response;
        }

        public ResponseList<Marca> getMarcas(String database)
        {
            ResponseList<Marca> response = new ResponseList<Marca>();

            using (IDbConnection db = new SqlConnection(ConfigurationManager.AppSettings[database].ToString()))
            {

                try

                {
                    var result = db.QueryMultiple("BC_SP_CREA_OBTENER_MARCAS", null, commandType: CommandType.StoredProcedure);
                    var r1 = result.ReadFirst();
                    if (r1.status == 200)
                    {
                        int estatus = r1.status;
                        response.status = estatus.ToString();
                        response.msg = r1.error_message;
                        response.model = result.Read<Marca>().ToList();
                    }
                    else
                    {
                        int estatus = r1.status;
                        response.status = estatus.ToString();
                        response.msg = r1.error_message;
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }

            return response;
        }

        public ResponseList<Marca> updateCambioEstado(int idProducto,String database, Boolean activo)
        {
            ResponseList<Marca> response = new ResponseList<Marca>();

            using (IDbConnection db = new SqlConnection(ConfigurationManager.AppSettings[database].ToString()))
            {
                DynamicParameters parameter = new DynamicParameters();

                parameter.Add("@idProductoEcommerce", idProducto);
                parameter.Add("@activo", activo);

                try
                {
                    var result = db.QueryMultiple("BC_SP_CREA_ACTIVAR_PRODUCTO_ECOMMERCE_ADMIN", parameter, commandType: CommandType.StoredProcedure);
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }

            return response;
        }

        public Responce insertProduct(string product, String database, int idMarca)
        {
            Responce response = new Responce();

            using (IDbConnection db = new SqlConnection(ConfigurationManager.AppSettings[database].ToString()))
            {
                DynamicParameters parameter = new DynamicParameters();

          
                parameter.Add("@Product", product);
                parameter.Add("@IdMarca", idMarca);

                try
                {
                    response = db.QueryFirst<Responce>("BC_SP_REGISTRAR_PRODUCTOS_ADMIN", parameter, commandType: CommandType.StoredProcedure);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }

            return response;
        }

        public Responce updateProduct(String database, detalleProducto producto)
        {
            Responce respuesta = new Responce();

            using (IDbConnection db = new SqlConnection(ConfigurationManager.AppSettings[database].ToString()))
            {
                DynamicParameters parameter = new DynamicParameters();

                parameter.Add("@idProductoEcommerce", producto.idProductoEcommerce);
                parameter.Add("@producto", producto.producto);
                parameter.Add("@descripcion", producto.descripcion);
                parameter.Add("@unidadVenta", producto.unidadVenta);
                parameter.Add("@precioVenta", producto.precioVenta);
                parameter.Add("@idCategoriaEcommerce", producto.idCategoriaEcommerce);
                //parameter.Add("@identificador", producto.identificador);


                try
                {
                   var  response = db.Query("BC_SP_CREA_EDITAR_PRODUCTO_ECOMMERCE_ADMIN", parameter, commandType: CommandType.StoredProcedure);

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