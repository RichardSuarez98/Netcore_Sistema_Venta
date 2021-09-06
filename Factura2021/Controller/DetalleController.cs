using Factura2021.Interface;
using Factura2021.Models;
using Factura2021.Models.Response;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Factura2021.Controller
{//ESTO NO SE DEBIO DE HABER CREADO
    [Route("api/[controller]")]
    [ApiController]
    public class DetalleController : ControllerBase
    {
        private readonly FacturaContext _context;
        private readonly InterfaceDetalle _idetalle;


        public DetalleController(FacturaContext context, InterfaceDetalle interfaceDetalle)
        {
            _context = context;
            _idetalle = interfaceDetalle;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            GeneralResponse resp = new GeneralResponse();
            var listDetalle = await _idetalle.GetDetalle(id);
            resp.Exito = 1;
            resp.Mensaje = "Bien hecho";
            resp.Data = listDetalle;

            return Ok(resp);
            //return await _context.TblCategoria.ToListAsync();
        }

      /*  [HttpPost]
        public async Task<ActionResult> Post([FromBody] TblDetalle detalle)  //tbl_personxta es la tabla de la data base
        {
            bool exito = await _idetalle.PostDetalle(detalle);
            if (exito) { return Ok(detalle); }
            else //if (!exito)
                return BadRequest();
        }*/

       /* [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] TblDetalle detalle)
        {
            bool exito = await _idetalle.PutDetalle(id, detalle);

            if (exito) { return Ok(id); }
            else //if (!exito)
                return BadRequest();
        }*/

       /* [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            bool exito = await _idetalle.DeleteDetalle(id);

            if (exito) { return Ok(id); }
            else
                return BadRequest();
        }*/


    }





}
