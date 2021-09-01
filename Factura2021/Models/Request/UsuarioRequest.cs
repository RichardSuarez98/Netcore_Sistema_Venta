using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Factura2021.Models.Request
{
    public class UsuarioRequest
    {
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public string password { get; set; }
        public int IdPersona { get; set; }
        public int IdEstado { get; set; }

    }

}
