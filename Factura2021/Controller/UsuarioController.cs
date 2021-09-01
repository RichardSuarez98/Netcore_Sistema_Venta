using Factura2021.Interface;
using Factura2021.Models;
using Factura2021.Models.Request;
using Factura2021.Models.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Factura2021.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class UsuarioController : ControllerBase
    {
        private readonly FacturaContext _context;
        private readonly InterfaceUsuario _iusuario;


        public UsuarioController(FacturaContext context, InterfaceUsuario interfaceUsuario)
        {
            _context = context;
            _iusuario = interfaceUsuario;
        }

        // GET: api/Categoria
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var listUsuario = await _iusuario.GetUsuario();
            return Ok(listUsuario);
            //return await _context.TblCategoria.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] UsuarioRequest usuario)  //tbl_personxta es la tabla de la data base
        {
            GeneralResponse usuarioResponse = await _iusuario.PostUsuario(usuario);
            if (usuarioResponse.Exito==1)
            {
                return Ok(usuarioResponse);
            }
            else
            {
                return BadRequest(usuarioResponse.Mensaje);
            }
            /*if (exito) { return Ok(usuario); }
            else //if (!exito)
                return BadRequest();*/
        }

        [HttpPost("login")]// TOKEEEEEEENNNNNNNNNNNNNNNNN
        public IActionResult PostLogin([FromBody] AuthRequest model)  
        {
            GeneralResponse respuesta = new GeneralResponse();

            var userresponse = _iusuario.Auth(model);

            if (userresponse == null)
            {
                respuesta.Exito = 0;
                respuesta.Mensaje = "usuario o contraseña incorrecta";
                // return BadRequest(respuesta);
                return Ok(respuesta);
            }
            else
            {
                respuesta.Exito = 1;
                respuesta.Data = userresponse;
                respuesta.Mensaje = "BIENVENIDA";
                return Ok(respuesta);
            }
           // return BadRequest();

        }

        /*         //esta de aqui es la veridica ------------- esta asi ---------------
                [HttpPost("login")]
                public async Task<ActionResult> PostLogin([FromBody] LoginREquest usuario)  //tbl_personxta es la tabla de la data base
                {
                    GeneralResponse usuarioResponse = await _iusuario.login(usuario);
                    //if (usuarioResponse ==null)
                    //{
                    //    usuarioResponse.Exito = 0;
                    //    usuarioResponse.Mensaje = "no existe";

                    //    return Ok(usuarioResponse);
                    //}
                    //else
                    //{

                    //    return Ok(usuarioResponse);
                    //}
                    if (usuarioResponse == null)
                    {
                        usuarioResponse.Exito = 0;
                        usuarioResponse.Mensaje = "no existe";
                       // return null;
                        return Ok(usuarioResponse);
                    }
                    else
                    {
                        return Ok(usuarioResponse);
                    }
                    //if (exito) { return Ok(usuario); }
                    //else //if (!exito)
                    //    return BadRequest();
                }*/


        [HttpPut]
        public async Task<ActionResult> Put([FromBody] UsuarioRequest usuario)
        {
            GeneralResponse usuarioResponse = await _iusuario.PutUsuario(usuario);
            if (usuarioResponse.Exito==1)
            {
                return Ok(usuarioResponse);
            }
            else
            {
                //return BadRequest(usuarioResponse.Mensaje);
                return BadRequest(usuarioResponse.Mensaje);
            }
            /*if (exito) { return Ok(id); }
            else //if (!exito)
                return BadRequest();*/
        }

        [HttpPut("eliminarUsuario")]
        public async Task<ActionResult> Delete(UsuarioRequest usuario)
        {
            GeneralResponse usuarioResponse = await _iusuario.DeleteUsuario(usuario);
            if (usuarioResponse.Exito==1)
            {
                return Ok(usuarioResponse);
            }
            else
            {
                return BadRequest(usuarioResponse.Mensaje);
            }

           /* if (exito) { return Ok(id); }
            else
                return BadRequest();*/
        }


    }
}
