using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CREA3M.Models
{
  
    public class Order
    {
        public int idUsuarioEcommerce { get; set; }
        public int idOrdenCompra { get; set; }
        public String cliente { get; set; }
        public String telefono { get; set; }
        public decimal monto { get; set; }
        public DateTime fechaRegistro { get; set; }
        public String guia { get; set; }
        public String statusOrdenCompra { get; set; }
    }

    public class Responce
    {
        public int status { get; set; }
        public String message { get; set; }
        public String error_message { get; set; }
        public String error_line { get; set; }
        public String error_severity { get; set; }
        public String error_procedure { get; set; }

    }

}