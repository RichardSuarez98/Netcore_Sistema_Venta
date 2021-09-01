using System;
using System.Collections.Generic;

#nullable disable

namespace Factura2021.Models
{
    public partial class TblMarca
    {
        public TblMarca()
        {
            TblProductos = new HashSet<TblProducto>();
        }

        public int IdMarca { get; set; }
        public string NombreMarca { get; set; }
        public string DescripcionMarca { get; set; }
        public int? IdEstado { get; set; }

        public virtual TblEstado IdEstadoNavigation { get; set; }
        public virtual ICollection<TblProducto> TblProductos { get; set; }
    }
}
