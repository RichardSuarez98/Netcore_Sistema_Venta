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
    public class PersonaController : ControllerBase
    {

        private readonly FacturaContext _context;
        private readonly InterfacePersona _ipersona;


        public PersonaController(FacturaContext context, InterfacePersona interfacePersona)
        {
            _context = context;
            _ipersona = interfacePersona;
        }

        // GET: api/Categoria
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var listPersona = await _ipersona.GetPersona();
            return Ok(listPersona);
            //return await _context.TblCategoria.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetPerso(string id)
        {
             GeneralResponse respuesta = new GeneralResponse();
            var listPersona = await _ipersona.GetPersonaBuscar(id);
          //  GeneralResponse resp = await _ipersona.GetPersonaBuscar(id);
            // var listPersona = await _ipersona.GetPersonaBuscar(id);
            if(listPersona==null)
            ///  if (resp.Exito == 1)
             {
                // resp.Data = listPersona;
                respuesta.Exito = 0;
                respuesta.Mensaje = "Cedula no existente";
                // return BadRequest(respuesta);
                return Ok(respuesta);
            }
            else
            {
                //respuesta.Exito = 1;
                //respuesta.Data = listPersona;
                //respuesta.Mensaje = "Registro de cedula ha sido encontrado";
                return Ok(listPersona);
            }
            
          //  return Ok(resp.Mensaje);
            //return await _context.TblCategoria.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] PersonaRequest persona)  //tbl_personxta es la tabla de la data base
        {
            GeneralResponse personaResponse = await _ipersona.PostPersona(persona);
            if (personaResponse.Exito==1)
            {
                return Ok(personaResponse);
            }
            else
            {
                return BadRequest(personaResponse.Mensaje);
            }
            /*if (exito) { return Ok(persona); }
            else //if (!exito)
                return BadRequest();*/
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody] PersonaRequest persona)
        {
            GeneralResponse personaResponse = await _ipersona.PutPersona(persona);
            if (personaResponse.Exito==1)
            {
                return Ok(personaResponse);
            }
            else
            {
                return BadRequest(personaResponse.Mensaje);
            }
           /* if (exito) { return Ok(id); }
            else //if (!exito)
                return BadRequest();*/
        }

        [HttpPut("eliminarPersona")]
        public async Task<ActionResult> Delete(PersonaRequest persona)
        {
            GeneralResponse personaResponse = await _ipersona.DeletePersona(persona);

            if (personaResponse.Exito==1)
            {
                return Ok(personaResponse);
            }
            else
            {
                return BadRequest(personaResponse.Mensaje);
            }
            /*if (exito) { return Ok(id); }
            else
                return BadRequest();*/
        }
    }
}
