using Factura2021.Interface;
using Factura2021.Models;
using Factura2021.Models.Request;
using Factura2021.Models.Response;
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
    public class MarcaController : ControllerBase
    {
        private readonly FacturaContext _context;
        private readonly InterfaceMarca _iMarca;

        public MarcaController(FacturaContext context, InterfaceMarca interfaceMarca)
        {
            _context = context;
            _iMarca = interfaceMarca;

        }

        // GET: api/<MarcaController>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var listMarca = await _iMarca.GetMarca();
            return Ok(listMarca);
            //return await _context.TblCategoria.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] MarcaRequest marca)  //tbl_personxta es la tabla de la data base
        {
            GeneralResponse marcaResponse = await _iMarca.PostMarca(marca);
            if (marcaResponse.Exito==1)
            {
                return Ok(marcaResponse);
            }
            else
            {
                return BadRequest(marcaResponse.Mensaje);
            }
            /*if (exito) { return Ok(marca); }
            else //if (!exito)
                return BadRequest();*/
        }

        [HttpPut()]
        public async Task<ActionResult> Put([FromBody] MarcaRequest marca)
        {
            GeneralResponse marcaResponse = await _iMarca.PutMarca(marca);
            if (marcaResponse.Exito==1)
            {
                return Ok(marcaResponse);
            }
            else
            {
                return BadRequest(marcaResponse.Mensaje);
            }
           /* if (exito) { return Ok(id); }
            else //if (!exito)
                return BadRequest();*/
        }

        [HttpPut("eliminarMarca")]
        public async Task<ActionResult> Delete(MarcaRequest marca)
        {
            GeneralResponse marcaResponse = await _iMarca.DeleteMarca(marca);
            if (marcaResponse.Exito==1)
            {
                return Ok(marcaResponse);
            }else if (marcaResponse.Exito == 3)
            {
                return Ok(marcaResponse);
            }
            else
            {
                return BadRequest(marcaResponse.Mensaje);
            }

            /*if (exito) { return Ok(id); }
            else
                return BadRequest();*/
        }












        /*
        // GET api/<MarcaController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<MarcaController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<MarcaController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<MarcaController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }*/




    }
}
