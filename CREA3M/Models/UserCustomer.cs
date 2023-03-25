using System.ComponentModel.DataAnnotations;
using System.Configuration;

namespace CREA3M.Models
{
    public class UserCustomer
    {
    
        public int idUserEcommerce { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string nombre { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [RegularExpression("^[0-9]{10}$",ErrorMessage = "Favor de ingresar solamente 10 dígitos")]
        public string celular { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [EmailAddress(ErrorMessage = "Favor de ingresar una dirección de email correcta")]
        public string email { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [RegularExpression("^[0-9,A-Z,a-z]{5,}", ErrorMessage = "Favor de ingresar al menos 5 letras y/o dígitos")]
        public string contrasena { get; set; }


    }
}