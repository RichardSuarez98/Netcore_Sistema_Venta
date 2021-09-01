using System;
using System.Collections.Generic;

#nullable disable

namespace Factura2021.Models
{
    public partial class TblInventario
    {
        public TblInventario()
        {
            TblDetalles = new HashSet<TblDetalle>();
        }

        public int IdInventario { get; set; }
        public int IdProducto { get; set; }
        public decimal? Iva { get; set; }
        public decimal? PrecioUnitario { get; set; }
        public int? Cantidad { get; set; }
        public int IdEstado { get; set; }

        public virtual TblEstado IdEstadoNavigation { get; set; }
        public virtual TblProducto IdProductoNavigation { get; set; }
        public virtual ICollection<TblDetalle> TblDetalles { get; set; }
    }
}
