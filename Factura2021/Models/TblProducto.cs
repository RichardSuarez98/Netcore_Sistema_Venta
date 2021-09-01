using System;
using System.Collections.Generic;

#nullable disable

namespace Factura2021.Models
{
    public partial class TblProducto
    {
        public TblProducto()
        {
            TblInventarios = new HashSet<TblInventario>();
        }

        public int IdProducto { get; set; }
        public string NombreProducto { get; set; }
        public string DescripcionProducto { get; set; }
        public int IdEstado { get; set; }
        public int IdMarca { get; set; }
        public int IdCategoria { get; set; }

        public virtual TblCategorium IdCategoriaNavigation { get; set; }
        public virtual TblEstado IdEstadoNavigation { get; set; }
        public virtual TblMarca IdMarcaNavigation { get; set; }
        public virtual ICollection<TblInventario> TblInventarios { get; set; }
    }
}
