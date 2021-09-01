using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Factura2021.Models.Response
{
    public class GeneralResponse
    {

        public int Exito { get; set; }
        public string Mensaje { get; set; }
        public object Data { get; set; }
    }
}
