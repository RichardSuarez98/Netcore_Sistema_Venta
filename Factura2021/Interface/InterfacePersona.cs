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
    public interface InterfacePersona
    {
       // Task<List<TblPersona>> GetPersona();
        Task<IEnumerable> GetPersona();
        Task<GeneralResponse> PostPersona([FromBody] PersonaRequest persona);
        Task<GeneralResponse> PutPersona([FromBody] PersonaRequest persona);
        Task<GeneralResponse> DeletePersona(PersonaRequest persona);
    }



}
