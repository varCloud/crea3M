using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CREA3M.Models
{


    public class Busqueda
    {
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public String tipoBusqueda { get; set; }
      
    }

    public class Order
    {
        public int idUsuarioEcommerce { get; set; }
        public int idOrdenCompra { get; set; }
        public int idStatusOrdenCompra { get; set; }
        public String cliente { get; set; }
        public String telefono { get; set; }
        public decimal monto { get; set; }
        public DateTime fechaRegistro { get; set; }
        public String guia { get; set; }
        public String statusOrdenCompra { get; set; }
    }

    public class DetalleOrder
    {
        public int cantidad { get; set; }
        public String producto { get; set; }
        public String descripcion { get; set; }
        public String productoImagen { get; set; }

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

    public class CatStatusOrden
    {
        public int idStatusOrdenCompra { get; set; }
        public String statusOrdenCompra { get; set; }

    }


    public class Result
    {
        public bool status { get; set; }
        public string mensaje { get; set; }
   
    }

}