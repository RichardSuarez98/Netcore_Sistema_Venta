using Factura2021.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Factura2021.Interface
{
    public interface InterfaceDetalle
    {
        Task<IEnumerable> GetDetalle(int id);
        //Task<List<TblDetalle>> GetDetalle();
        //Task<bool> PostDetalle([FromBody] TblDetalle detalle);
        //Task<bool> PutDetalle(int id, [FromBody] TblDetalle detalle);
        //Task<bool> DeleteDetalle(int id);
    }
}
