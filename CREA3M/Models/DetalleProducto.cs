using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CREA3M.Models
{
    public class DetalleProducto
    {
        public int idProductoEcommerce { get; set; }

        [Required(ErrorMessage = "Esta campo es obligatorio")]
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

        public float cantidad { get; set; }

        public string descripcionSubcat { get; set; }

        public int idSubcategoriaEcommerce { get; set; }

        public float cantidadAgregarInventario { get; set; }

        public float costoEnvio { get; set; }

        public string SKU { get; set; }
        public string codigoBarras { get; set; }

    }
}