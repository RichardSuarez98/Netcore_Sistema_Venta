using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Factura2021.Models.Request
{
    public class PersonaRequest
    {
        public int IdPersona { get; set; }
        public string NombrePersona { get; set; }
        public string Cedula { get; set; }
        public int? Edad { get; set; }
        public int IdTipoPersona { get; set; }
        public int IdEstado { get; set; }
    }
}
