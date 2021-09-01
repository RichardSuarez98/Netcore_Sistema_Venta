using Factura2021.Models;
using Factura2021.Models.Request;
using Factura2021.Models.Response;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Factura2021.Interface
{
    public interface InterfaceFactura
    {
        Task<IEnumerable> GetFactura();

        Task<GeneralResponse> PostFactura([FromBody] RequestFactura factura);
    }

}
