using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CREA3M.Models
{
    public class ReporteProducto
    {
        public float cantidadTotalProductoVendido { get; set; }
        public float costoTotalProductoVendido { get; set; }
        public string descripcion { get; set; }
        public int idProductoEcommerce { get; set; }
        public string  estado { get; set; }

        public string nombreCliente { get; set; }
        public string telefonoCliente { get; set; }
        public string mailCliente { get; set; }

        public int vecesVendido { get; set; }
    }
}