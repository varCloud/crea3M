using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CREA3M.Models
{
    public class UsuarioEcommerce
    {
        public int idUsuarioEcommerce { get; set; }
        public string nombre { get; set; }
        public string apellidoPaterno  { get; set; }
        public string apellidoMaterno  { get; set; }
        public string email            { get; set; }
        public string contrasena       { get; set; }
        public DateTime fechaRegistro    { get; set; }
        public string activo           { get; set; }
        public string celular          { get; set; }
        public string esSuscripcion    { get; set; }
        
        public string esCliente { get; set; }

        public string seRegistro { get; set; }
    }
}