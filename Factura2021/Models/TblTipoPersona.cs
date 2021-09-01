using System;
using System.Collections.Generic;

#nullable disable

namespace Factura2021.Models
{
    public partial class TblTipoPersona
    {
        public TblTipoPersona()
        {
            TblPersonas = new HashSet<TblPersona>();
        }

        public int IdTipoPersona { get; set; }
        public string NombreTipoPersona { get; set; }
        public int IdEstado { get; set; }

        public virtual TblEstado IdEstadoNavigation { get; set; }
        public virtual ICollection<TblPersona> TblPersonas { get; set; }
    }
}
