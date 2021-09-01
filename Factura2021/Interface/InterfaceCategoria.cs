using Factura2021.Models;
using Factura2021.Models.Request;
using Factura2021.Models.Response;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Factura2021.Interface
{
    public interface InterfaceCategoria
    {
        Task<List<TblCategorium>> GetCategoria();
        Task<GeneralResponse> PostCategoria([FromBody] CategoriaRequest categoria);
        Task<GeneralResponse> PutCategoria([FromBody] CategoriaRequest categoria);
        Task<GeneralResponse> DeleteCategoria([FromBody] CategoriaRequest categoria);
      

    }
}
