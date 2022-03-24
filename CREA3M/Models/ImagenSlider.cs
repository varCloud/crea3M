using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CREA3M.Models
{
    public class ImagenSlider
    {

       public int idImagenSlider { get; set; }
      public string  path            { get; set; }
      public string  size            { get; set; }
      public string  nombre          { get; set; }
      public bool  activo          { get; set; }
      public DateTime fechaAlta { get; set; }
    }
}