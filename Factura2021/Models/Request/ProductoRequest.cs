using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Factura2021.Models.Request
{
    public class ProductoRequest
    {
        public int IdProducto { get; set; }
        public string NombreProducto { get; set; }
        public string DescripcionProducto { get; set; }
        public int IdEstado { get; set; }
        public int IdMarca { get; set; }
        public int IdCategoria { get; set; }
    }
}
