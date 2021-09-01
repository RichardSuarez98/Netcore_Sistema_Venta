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
    public class ProductoController : ControllerBase
    {
        private readonly FacturaContext _context;
        private readonly InterfaceProducto _iproducto;


        public ProductoController(FacturaContext context, InterfaceProducto interfaceProducto)
        {
            _context = context;
            _iproducto = interfaceProducto;
        }

        // GET: api/Categoria
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var listProducto = await _iproducto.GetProducto();
            return Ok(listProducto);
            //return await _context.TblCategoria.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ProductoRequest producto)  //tbl_personxta es la tabla de la data base
        {
            GeneralResponse productoResponse = await _iproducto.PostProducto(producto);
            if (productoResponse.Exito==1)
            {
                return Ok(productoResponse);
            }
            else
            {
                return BadRequest(productoResponse.Mensaje);
            }
           /* if (exito) { return Ok(producto); }
            else //if (!exito)
                return BadRequest();*/
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody] ProductoRequest producto)
        {
            GeneralResponse productoResponse = await _iproducto.PutProducto(producto);
            if (productoResponse.Exito==1)
            {
                return Ok(productoResponse);
            }
            else
            {
                return BadRequest(productoResponse.Mensaje);
            }

            /*if (exito) { return Ok(id); }
            else //if (!exito)
                return BadRequest();*/
        }

        [HttpPut("eliminarProducto")]
        public async Task<ActionResult> Delete(ProductoRequest producto)
        {
            GeneralResponse productoResponse = await _iproducto.DeleteProducto(producto);
            if (productoResponse.Exito==1)
            {
                return Ok(productoResponse);
            }
            else
            {
                return BadRequest(productoResponse.Mensaje);
            }
            /*if (exito) { return Ok(id); }
            else
                return BadRequest();*/
        }



    }
}
