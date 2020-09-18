using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CREA3M.Models
{
    public class ClientShort
    {
        public int idCliente { get; set; }
        public string Nombre { get; set; }
        public string RFC { get; set; }
        public string Localidad { get; set; }
    }
}