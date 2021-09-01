using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Factura2021.Models;
using Factura2021.Interface;
using Factura2021.Models.Request;
using Factura2021.Models.Response;
using Microsoft.AspNetCore.Authorization;

namespace Factura2021.Controller
{
    [Route("api/[controller]")]
    [ApiController]
  
    public class CategoriaController : ControllerBase
    {
        private readonly FacturaContext _context;
        private readonly InterfaceCategoria _icategoria;


        public CategoriaController(FacturaContext context, InterfaceCategoria interfaceCategoria)
        {
            _context = context;
            _icategoria = interfaceCategoria;
        }

        // GET: api/Categoria
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var listCategoria = await _icategoria.GetCategoria();
            return Ok(listCategoria);
            //return await _context.TblCategoria.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CategoriaRequest categoria)  //tbl_personxta es la tabla de la b d
        {
            GeneralResponse categoriaResponse = await _icategoria.PostCategoria(categoria);
            if (categoriaResponse.Exito == 1) { return Ok(categoriaResponse); }

            else
            {
                return BadRequest(categoriaResponse.Mensaje);
            }
            //if (exito) { return Ok(categoria); }
            //else //if (!exito)
            //    return BadRequest();
      //  return ok(categoriaResponse;
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody] CategoriaRequest categoria)
        {
            GeneralResponse categoriaResponse = await _icategoria.PutCategoria(categoria);
            if (categoriaResponse.Exito == 1)
            {
                return Ok(categoriaResponse);
            }
            else
            {
                return BadRequest(categoriaResponse.Mensaje);
            }

            /*if (exito) { return Ok(id); }
            else //if (!exito)
                return BadRequest();*/
        }

        [HttpPut("eliminarCategoria")]
        public async Task<ActionResult> Delete(CategoriaRequest categoria)
        {
            GeneralResponse categoriResponse = await _icategoria.DeleteCategoria(categoria);

            /*if (exito) { return Ok(id); }
            else
                return BadRequest();*/
            if (categoriResponse.Exito == 1)
            {
                return Ok(categoriResponse);
            }
            else if (categoriResponse.Exito == 3)
            {
                return Ok(categoriResponse);
            }
            else
            {
                return BadRequest(categoriResponse.Mensaje);
            }
        }


           



    }
}
