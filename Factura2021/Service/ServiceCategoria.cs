using Factura2021.Interface;
using Factura2021.Models;
using Factura2021.Models.Request;
using Factura2021.Models.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Factura2021.Service
{
    public class ServiceCategoria : InterfaceCategoria
    {
        private readonly FacturaContext _context;

        public ServiceCategoria(FacturaContext context)
        {
            this._context = context;
        }
        public async Task<List<TblCategorium>> GetCategoria()
        {
            List<TblCategorium> listCategoria = await _context.TblCategoria.Where(x=>x.IdEstado==1).ToListAsync();
            return listCategoria;
            // return await _context.TblCategoria.ToListAsync();
        }

        public async Task<GeneralResponse> PostCategoria([FromBody] CategoriaRequest categoria)
        {
            GeneralResponse resp = new GeneralResponse();
            try
            {
                
             
                var cat = new TblCategorium();
                cat.NombreCategoria = categoria.NombreCategoria;
                cat.IdEstado = 1;
                cat.DescripcionCategoria = categoria.DescripcionCategoria;

                _context.TblCategoria.Add(cat);// Add();
                await _context.SaveChangesAsync();
                resp.Exito = 1;
                resp.Mensaje = "Bien hecho";
                resp.Data =categoria;
 
                return resp;
            }
            catch (Exception ex)
            {
                resp.Exito = 0;
                resp.Mensaje ="No sé pudo registrar"+ex.Message;
               
                return resp;

            }
        }


        public async Task<GeneralResponse> PutCategoria([FromBody] CategoriaRequest categoria)
        {//manera correcta de actualizar
            GeneralResponse resp = new GeneralResponse();        
            try
            {
                var cat = await _context.TblCategoria.FindAsync(categoria.IdCategoria);          
                cat.NombreCategoria = categoria.NombreCategoria;
                cat.DescripcionCategoria = categoria.DescripcionCategoria;
                _context.Entry(cat).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                await _context.SaveChangesAsync();
                resp.Exito = 1;
                resp.Mensaje = "Actualizado con exito";
                resp.Data = categoria;

                return resp;
            }
            catch (Exception ex)
            {
                resp.Exito = 0;
                resp.Mensaje = "No sé pudo realizar su actualizacion" + ex.Message;
                return resp;
                //return false;
            }
        }

        public async Task<GeneralResponse> DeleteCategoria([FromBody] CategoriaRequest categoria)
        {//manera correcta y logica de eliminar, pasa de activo a inactivo
            GeneralResponse resp = new GeneralResponse();
            
            try
            {
                var cateProdu = _context.TblProductos.Where(x => x.IdCategoria == categoria.IdCategoria).Count();

                if (cateProdu > 0)
                {
                    resp.Exito = 3;
                    resp.Mensaje = "LA CATEGORIA QUE DESEA ELIMINAR ESTA ASOCIADA A :  "+cateProdu;

                    return resp;
                }
                var cate = await _context.TblCategoria.FindAsync(categoria.IdCategoria);
                // _context.TblCategoria.Remove(categoria);
                cate.IdEstado = 0;// estaba 2
                _context.Entry(cate).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                await _context.SaveChangesAsync();
                resp.Exito = 1;
                resp.Mensaje = "Eliminado con exito";
             
                return resp;
            }
            catch (Exception ex)
            {
                resp.Exito = 0;
                resp.Mensaje = "error al eliminar"+ex.Message;

                return resp;
            }
        }





    }

}
