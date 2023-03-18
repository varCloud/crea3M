using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
       public string CategoriaEcommerce { get; set; }
       public int IdCategoria { get; set; }
       public string IdMarca { get; set; }
       public string UnidadDeVenta { get; set; }
       public string PrecioDeVenta { get; set; }
       public string productoStatus { get; set; }

        public int CantidadAgregarInventario { get; set; }

        public int IdSubcategoria { get; set; }
        public int CostoEnvio { get; set; }
        public Boolean activo { get; set; }

        public float cantidad { get; set; }
        public string descripcionSubcat { get; set; }
        public int idSubcategoriaEcommerce { get; set; }
        public string marca { get; set; }

        public string codigoBarras { get; set; }
        public string SKU { get; set; }
        public string productoUrl { get; set; }
        public string imagenUrl { get; set; }

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

    public class Categoria
    {
        public int idCategoriaEcommerce { get; set; }
        public int idMarcaEcommerce { get; set; }
        public int idLineaCategoriaEcommerce { get; set; }
        public string descripcion { get; set; }

        public string CategoriaEcommerce { get; set; }
        public List<SubCategoria> subCategorias { get; set; }

        public Categoria()
        {
            this.subCategorias = new List<SubCategoria>();
        }
    }

    public class SubCategoria
    {
        public int idSubcategoriaEcommerce { get; set; }
        public string SubcategoriaEcommerce { get; set; }
        public int idCategoriaSubCategoria { get; set; }

    }

    public class PathImgProduct
    {
        public int idProductoImagen { get; set; }
        public string productoImagen { get; set; }
        public DateTime fechaRegistro { get; set; }
        public int idProductoEcommerce { get; set; }

    }





}