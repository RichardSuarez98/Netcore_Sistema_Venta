using System;
using System.Collections.Generic;

#nullable disable

namespace Factura2021.Models
{
    public partial class TblDetalle
    {
        public int IdDetalle { get; set; }
        public int IdFactura { get; set; }
        public int IdInventario { get; set; }
        public int? Cantidad { get; set; }
        public decimal? PrecioUnitario { get; set; }
        public decimal? Subtotal { get; set; }
        public decimal? Total { get; set; }
        public int IdEstado { get; set; }

        public virtual TblEstado IdEstadoNavigation { get; set; }
        public virtual TblFactura IdFacturaNavigation { get; set; }
        public virtual TblInventario IdInventarioNavigation { get; set; }
    }
}
