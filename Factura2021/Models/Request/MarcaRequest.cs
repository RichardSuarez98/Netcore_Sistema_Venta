using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Factura2021.Models.Request
{
    public class MarcaRequest
    {
        public int IdMarca { get; set; }
        public string NombreMarca { get; set; }
        public string DescripcionMarca { get; set; }
        public int? IdEstado { get; set; }
    }


}
