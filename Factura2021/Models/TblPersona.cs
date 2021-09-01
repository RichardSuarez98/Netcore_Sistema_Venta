using System;
using System.Collections.Generic;

#nullable disable

namespace Factura2021.Models
{
    public partial class TblPersona
    {
        public TblPersona()
        {
            TblFacturas = new HashSet<TblFactura>();
            TblUsuarios = new HashSet<TblUsuario>();
        }

        public int IdPersona { get; set; }
        public string NombrePersona { get; set; }
        public string Cedula { get; set; }
        public int? Edad { get; set; }
        public int IdTipoPersona { get; set; }
        public int IdEstado { get; set; }

        public virtual TblEstado IdEstadoNavigation { get; set; }
        public virtual TblTipoPersona IdTipoPersonaNavigation { get; set; }
        public virtual ICollection<TblFactura> TblFacturas { get; set; }
        public virtual ICollection<TblUsuario> TblUsuarios { get; set; }
    }
}
