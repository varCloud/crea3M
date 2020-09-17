using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;

namespace CREA3M.Models
{
    public class Product
    {
       public string NP { get; set; }
       public string idProductoEcommerce { get; set; }
       public string Producto { get; set; }
       public string Descripcion { get; set; }
       public string IdCategoria { get; set; }
       public string IdMarca { get; set; }
       public string UnidadDeVenta { get; set; }
       public string PrecioDeVenta { get; set; }
       public string productoStatus { get; set; }
        public Boolean activo { get; set; }
    }

    public class detalleProducto
    {
        public int idProductoEcommerce { get; set; }
        public string identificador { get; set; }
        public string producto { get; set; }
        public string unidadVenta { get; set; }
        public decimal precioVenta { get; set; }
        public int idTipoProducto { get; set; }
        public string tipoProducto { get; set; }
        public int idCategoriaEcommerce { get; set; }
        public string CategoriaEcommerce { get; set; }
        public string descripcion { get; set; }
        public bool activo { get; set; }
    }

    public class respons
    {
        public string error_message { get; set; }
        public int status { get; set; }
       
    }

    public class Marca
    {
        public int idMarcaEcommerce { get; set; }
        public string marcaEcommerce { get; set; }

    }

    public class PathImgProduct
    {
        public int idProductoImagen { get; set; }
        public string productoImagen { get; set; }
        public DateTime fechaRegistro { get; set; }
        public int idProductoEcommerce { get; set; }

    }


}