using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Factura2021.Models.Request
{
    public class RequestFactura
    {
        public int IdUsuario { get; set; }
        public int IdPersona { get; set; }
        //public DateTime FechaEmision { get; set; }
        //public decimal TotalFactura { get; set; }
        //public int? TotalProducto { get; set; }
       // public int IdEstado { get; set; }


        public List<Detalle> detalles { get; set; }

    }

    public class Detalle
    {
        public int IdInventario { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
       // public decimal Subtotal { get; set; }
       // public decimal Total { get; set; }
      //  public int IdEstado { get; set; }
    }

}
