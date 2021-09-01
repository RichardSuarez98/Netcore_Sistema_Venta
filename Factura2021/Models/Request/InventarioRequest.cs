using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Factura2021.Models.Request
{
    public class InventarioRequest
    {
        public int IdInventario { get; set; }
        public int IdProducto { get; set; }
        public decimal? Iva { get; set; }
        public decimal? PrecioUnitario { get; set; }
        public int? Cantidad { get; set; }
        public int IdEstado { get; set; }
    }


}
