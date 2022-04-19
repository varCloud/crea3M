using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CREA3M.Models
{
    public class FiltroReporte
    {
        public int mes { get; set; }

        public int ano { get; set; }

        public enumTipoReporte tipoReporte { get; set; }

        public string fechaInicio { get; set; }

        public string fechaFin  { get; set; }
    }

    public enum enumTipoReporte {
        PRODUCTO_MAS_VENDIDO = 1, 
        PRODUCTO_MENOS_VENDIDO = 2,
        VENTAS_POR_ESTADO = 3,
        PROMEDIO_VENTA_CLIENTE = 4,
        USUARIOS_REGISTRADOS = 5,
        USUARIOS_SUSCRIPCIONES = 6

        
          
    }
}