using Factura2021.Interface;
using Factura2021.Models;
using Factura2021.Models.Request;
using Factura2021.Models.Response;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Factura2021.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturaController : ControllerBase
    {
        private readonly FacturaContext _context;
        private readonly InterfaceFactura _ifactura;


        public FacturaController(FacturaContext context, InterfaceFactura interfaceFactura)
        {
            _context = context;
            _ifactura = interfaceFactura;
        }

        // GET: api/Categoria
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var listFactura = await _ifactura.GetFactura();
            return Ok(listFactura);
            //return await _context.TblCategoria.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] RequestFactura factura)  //tbl_personxta es la tabla de la data base
        {
            GeneralResponse facturaResponse = await _ifactura.PostFactura(factura);
            if (facturaResponse.Exito==1)
            {
                return Ok(facturaResponse);
            }
            else
            {
                return BadRequest(facturaResponse.Mensaje);
            }
           /* if (exito) { return Ok(factura); }
            else //if (!exito)
                return BadRequest();*/
        }



    }


}
