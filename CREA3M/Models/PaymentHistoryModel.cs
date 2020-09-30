using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CREA3M.Models
{
    public class PaymentHistoryModel
    {
        public string FechaCobro { get; set; }
        public string Nombre { get; set; }
        public int idFactura { get; set; }
        public int Folio { get; set; }
        public string Observaciones { get; set; }
        public string Operacion { get; set; }
        public string NumeroCuenta { get; set; }
        public double Total { get; set; }
        public double Saldo { get; set; }
        public string Vendedor { get; set; }
    }
}