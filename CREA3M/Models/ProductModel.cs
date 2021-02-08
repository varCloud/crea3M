﻿using System;
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
       public string DescripcionCat { get; set; }
       public int IdCategoria { get; set; }
       public string IdMarca { get; set; }
       public string UnidadDeVenta { get; set; }
       public string PrecioDeVenta { get; set; }
       public string productoStatus { get; set; }
        public Boolean activo { get; set; }
    }

    public class detalleProducto
    {
        public int idProductoEcommerce { get; set; }

        [Required(ErrorMessage ="Esta campo es obligatorio")]
        public string identificador { get; set; }

        [Required(ErrorMessage = "Esta campo es obligatorio")]
        public string producto { get; set; }

        [Required(ErrorMessage = "Esta campo es obligatorio")]
        public string unidadVenta { get; set; }

        [Required(ErrorMessage = "Esta campo es obligatorio")]
        public decimal precioVenta { get; set; }

        [Required(ErrorMessage = "Seleccione una categoria por favor")]
        public int idCategoriaEcommerce { get; set; }

        [Required(ErrorMessage = "Seleccione una marca por favor")]
        public int idMarcaEcommerce { get; set; }

        [Required(ErrorMessage = "Esta campo es obligatorio")]
        public string descripcion { get; set; }

        public string CategoriaEcommerce { get; set; }

        public bool activo { get; set; }
        
        public int idTipoProducto { get; set; }

        public string tipoProducto { get; set; }
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
    }

    public class PathImgProduct
    {
        public int idProductoImagen { get; set; }
        public string productoImagen { get; set; }
        public DateTime fechaRegistro { get; set; }
        public int idProductoEcommerce { get; set; }

    }





}