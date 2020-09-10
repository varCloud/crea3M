using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CREA3M.Models
{
    public class Client
    {
        public int idCliente { get; set; }
        public int idEmpresa { get; set; }
        public string Nombre { get; set; }
        public string RFC { get; set; }
        public string Calle { get; set; }
        public string NumeroExterior { get; set; }
        public string NumeroInterior { get; set; }
        public string Colonia { get; set; }
        public string Localidad { get; set; }
        public string Referencia { get; set; }
        public string Municipio { get; set; }
        public string Estado { get; set; }
        public string Pais { get; set; }
        public string CodigoPostal { get; set; }
        public string Contacto { get; set; }
        public string Telefonos { get; set; }
        public string emai { get; set; }
        public string PaginaWeb { get; set; }
        public string CuentaContable { get; set; }
        public string idUsuario { get; set; }
    }
}