using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CREA3M.Models
{
    public class LoginModel
    {
        public String Usuario { get; set; }
        public String Password { get; set; }
        public String NombreCompleto { get; set; }
        public String TargetServer { get; set; }
        public String defaultDB { get; set; }
        public String Token { get; set; }
        public int idUsuario { get; set; }
    }
}