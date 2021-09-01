using System;
using System.Collections.Generic;

#nullable disable

namespace Factura2021.Models
{
    public partial class TblFactura
    {
        public TblFactura()
        {
            TblDetalles = new HashSet<TblDetalle>();
        }

        public int IdFactura { get; set; }
        public int IdUsuario { get; set; }
        public int IdPersona { get; set; }
        public DateTime? FechaEmision { get; set; }
        public decimal? TotalFactura { get; set; }
        public int? TotalProducto { get; set; }
        public int IdEstado { get; set; }

        public virtual TblEstado IdEstadoNavigation { get; set; }
        public virtual TblPersona IdPersonaNavigation { get; set; }
        public virtual TblUsuario IdUsuarioNavigation { get; set; }
        public virtual ICollection<TblDetalle> TblDetalles { get; set; }
    }
}
