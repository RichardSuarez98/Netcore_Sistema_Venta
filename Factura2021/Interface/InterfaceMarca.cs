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
    public interface InterfaceMarca
    {
        Task<List<TblMarca>> GetMarca();
        Task<GeneralResponse> PostMarca([FromBody] MarcaRequest marca);
        Task<GeneralResponse> PutMarca([FromBody] MarcaRequest marca);
        Task<GeneralResponse> DeleteMarca(MarcaRequest marca);



    }




}
