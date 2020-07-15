using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CREA3M.Models
{
    public class SaleModel
    {
        public String TipoDocumento { get; set; }
        public String Serie { get; set; }
        public int Folio { get; set; }
        public String Fecha { get; set; }
        public Double SubTotalAntesDescuento { get; set; }
        public Double ImporteDescuento { get; set; }
        public Double ImporteIVA { get; set; }
        public Double SubTotal { get; set; }
        public Double Total { get; set; }
        public String ClienteNombre { get; set; }
        public String RFC { get; set; }
        public String MetodoPago { get; set; }
        public String FormaPago { get; set; }
        public String CondicionesPago { get; set; }
        public String Usuario { get; set; }
        public int Facturado { get; set; }
        public int FolioFacturaNotaVentaFacturada { get; set; }
        public String Vendedor { get; set; }
        public String GiroComercial { get; set; }
        public String NumeroPedido { get; set; }
    }
}