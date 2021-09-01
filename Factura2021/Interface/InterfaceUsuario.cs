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
    public interface InterfaceUsuario
    {
        Task<IEnumerable> GetUsuario();
        Task<GeneralResponse> PostUsuario([FromBody] UsuarioRequest usuario);
        //Task<GeneralResponse> login([FromBody] LoginREquest usuario);
        UsuarioResponse Auth(AuthRequest model);
        Task<GeneralResponse> PutUsuario([FromBody] UsuarioRequest usuario);
        Task<GeneralResponse> DeleteUsuario(UsuarioRequest usuario);
    }

}
