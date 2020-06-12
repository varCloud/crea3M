using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CREA3M.Models
{
    public class SaleModel
    {
        public int idCliente { get; set; }
        public int idFactura { get; set; }
        public String Nombre { get; set; }
        public String Fecha { get; set; }
        public Double SubTotalConDescuento { get; set; }
        public Double Total { get; set; }

    }
}