using Factura2021.Models.Request;
using System;
using System.Collections.Generic;

#nullable disable

namespace Factura2021.Models
{
    public partial class TblCategorium
    {
        public TblCategorium()
        {
            TblProductos = new HashSet<TblProducto>();
        }

        public int IdCategoria { get; set; }
        public string NombreCategoria { get; set; }
        public string DescripcionCategoria { get; set; }
        public int? IdEstado { get; set; }

        public virtual TblEstado IdEstadoNavigation { get; set; }
        public virtual ICollection<TblProducto> TblProductos { get; set; }

        public static implicit operator TblCategorium(CategoriaRequest v)
        {
            throw new NotImplementedException();
        }
    }
}
