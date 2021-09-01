using Factura2021.Interface;
using Factura2021.Models;
using Factura2021.Models.Request;
using Factura2021.Models.Response;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Factura2021.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventarioController : ControllerBase
    {
        private readonly FacturaContext _context;
        private readonly InterfaceInventario _iinventario;


        public InventarioController(FacturaContext context, InterfaceInventario interfaceInventario)
        {
            _context = context;
            _iinventario = interfaceInventario;
        }

        // GET: api/Categoria
        [HttpGet]

        //esto queda igual con el task<actionresult>
        public async Task<ActionResult> Get()
            // Task<ActionResult>
        {
            var listInventario = await _iinventario.GetInventario();
            return Ok(listInventario);
            //return await _context.TblCategoria.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] InventarioRequest inventario)  //tbl_personxta es la tabla de la data base
        {
            GeneralResponse inventarioResponse = await _iinventario.PostInventario(inventario);
            if (inventarioResponse.Exito==1)
            {
                return Ok(inventarioResponse);
            }
            else
            {
                return BadRequest(inventarioResponse.Mensaje);
            }
           /* if (exito) { return Ok(inventario); }
            else //if (!exito)
                return BadRequest();*/
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody] InventarioRequest inventario)
        {
            //bool exito = await _iinventario.PutInventario(id, inventario);
            GeneralResponse inventarioResponse = await _iinventario.PutInventario(inventario);
            if (inventarioResponse.Exito==1) 
            {
                return Ok(inventarioResponse);
            }
            else
            {
                return BadRequest(inventarioResponse.Mensaje);
            }

            /*if (exito) { return Ok(id); }
            else //if (!exito)
                return BadRequest();*/
        }

        [HttpPut("eliminarInventario")]
        public async Task<ActionResult> Delete(InventarioRequest inventario)
        {
            GeneralResponse inventarioResponse = await _iinventario.DeleteInventario(inventario);
            if (inventarioResponse.Exito==1)
            {
                return Ok(inventarioResponse);
            }
            else
            {
                return BadRequest(inventarioResponse.Mensaje);
            }
            /*if (exito) { return Ok(id); }
            else
                return BadRequest();*/
        }
    }
}
