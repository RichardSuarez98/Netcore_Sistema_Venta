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
    public interface InterfaceInventario
    {
       // Task<List<TblInventario>>
        Task<IEnumerable> GetInventario();
        Task<GeneralResponse> PostInventario([FromBody] InventarioRequest inventario);
        Task<GeneralResponse> PutInventario([FromBody] InventarioRequest inventario);
        Task<GeneralResponse> DeleteInventario(InventarioRequest inventario);
    }
}
