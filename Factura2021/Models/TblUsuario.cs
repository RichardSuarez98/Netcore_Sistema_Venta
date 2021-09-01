using System;
using System.Collections.Generic;

#nullable disable

namespace Factura2021.Models
{
    public partial class TblUsuario
    {
        public TblUsuario()
        {
            TblFacturas = new HashSet<TblFactura>();
        }

        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public string Password { get; set; }
        public int IdPersona { get; set; }
        public int IdEstado { get; set; }

        public virtual TblEstado IdEstadoNavigation { get; set; }
        public virtual TblPersona IdPersonaNavigation { get; set; }
        public virtual ICollection<TblFactura> TblFacturas { get; set; }
    }
}
