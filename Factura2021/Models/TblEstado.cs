using System;
using System.Collections.Generic;

#nullable disable

namespace Factura2021.Models
{
    public partial class TblEstado
    {
        public TblEstado()
        {
            TblCategoria = new HashSet<TblCategorium>();
            TblDetalles = new HashSet<TblDetalle>();
            TblFacturas = new HashSet<TblFactura>();
            TblInventarios = new HashSet<TblInventario>();
            TblMarcas = new HashSet<TblMarca>();
            TblPersonas = new HashSet<TblPersona>();
            TblProductos = new HashSet<TblProducto>();
            TblTipoPersonas = new HashSet<TblTipoPersona>();
            TblUsuarios = new HashSet<TblUsuario>();
        }

        public int IdEstado { get; set; }
        public string NombreEstado { get; set; }
        public string Descripcion { get; set; }
        public bool? Estado { get; set; }

        public virtual ICollection<TblCategorium> TblCategoria { get; set; }
        public virtual ICollection<TblDetalle> TblDetalles { get; set; }
        public virtual ICollection<TblFactura> TblFacturas { get; set; }
        public virtual ICollection<TblInventario> TblInventarios { get; set; }
        public virtual ICollection<TblMarca> TblMarcas { get; set; }
        public virtual ICollection<TblPersona> TblPersonas { get; set; }
        public virtual ICollection<TblProducto> TblProductos { get; set; }
        public virtual ICollection<TblTipoPersona> TblTipoPersonas { get; set; }
        public virtual ICollection<TblUsuario> TblUsuarios { get; set; }
    }
}
