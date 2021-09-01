using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Factura2021.Models.Request
{
    public class CategoriaRequest
    {
        public int IdCategoria { get; set; }
        public string NombreCategoria { get; set; }
        public string DescripcionCategoria { get; set; }
        public int? IdEstado { get; set; }
    }
    //public List<Detalle> detalles { get; set; }




}
