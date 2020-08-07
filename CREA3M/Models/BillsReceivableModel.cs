using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CREA3M.Models
{
    public class BillsReceivableModel
    {
        public int idFactura { get; set; }
        public string Fecha { get; set; }
        public string Nombre { get; set; }
        public string RFC { get; set; }
        public string TipoDocumento { get; set; }
        public string Serie { get; set; }
        public int Folio { get; set; }
        public double Cobrado { get; set; }
        public double Saldo { get; set; }
        public double Total { get; set; }
        public string MetodoPago { get; set; }
        public string FormaPago { get; set; }
        public string CondicionesPago { get; set; }
        public string EmisorNombre { get; set; }
        public string Logotipo { get; set; }
        public int Dias { get; set; }
        public string Vencimiento { get; set; }
        public string FechaInicial { get; set; }
        public string FechaFinal { get; set; }
        public string RutaXML { get; set; }
        public int idUsuario { get; set; }
        public string Usuario { get; set; }
        public string Vendedor { get; set; }
        public string UUID { get; set; }
        public string eMail { get; set; }
        public int idCliente { get; set; }
        public string RFCEmisor { get; set; }
        public string Abreviatura { get; set; }
    }
}