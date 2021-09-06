using Factura2021.Interface;
using Factura2021.Models;
using Factura2021.Models.Request;
using Factura2021.Models.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Factura2021.Service
{
    public class ServiceInventario : InterfaceInventario
    {
        private readonly FacturaContext _context;

        public ServiceInventario(FacturaContext context)
        {
            this._context = context;
        }

        //Task<List<TblInventario>> le cambio a ienumerable
        public async Task<IEnumerable> GetInventario()
        {
            // List<TblInventario> listInventario = await _context.TblInventarios.Where(x => x.IdEstado == 1).ToListAsync();
            //List<TblInventario> listInventario =
            //modelo de respuesta que tiene regresar
            dynamic res = await _context.TblInventarios.Include(x => x.IdProductoNavigation)
                                                                              .Where(i => i.IdEstado == 1)
                                                                              .Select(x => new
                                                                              {
                                                                                  x.IdInventario,
                                                                                  x.IdProductoNavigation.NombreProducto,
                                                                                  x.Iva,
                                                                                  x.PrecioUnitario,
                                                                                  x.Cantidad,
                                                                                  x.IdEstado
                                                                              }).ToListAsync();
            // List<TblInventario> listInventario = await _context.TblInventarios.FromSqlRaw("select IdInventario,NombreProducto,Iva,PrecioUnitario,Cantidad from tbl_Producto p INNER JOIN tbl_Inventario i ON p.IdProducto = i.IdProducto").ToListAsync();
            /*List<TblInventario> listInventario = (List<TblInventario>)(from pro in _context.TblProductos
                                                 join inv in _context.TblInventarios on pro.IdProducto equals inv.IdProducto

                                       select new  {  pro ,inv});*/


            return res;
            // return await _context.TblCategoria.ToListAsync();
        }

        public async Task<GeneralResponse> PostInventario([FromBody] InventarioRequest inventario)
        {
            GeneralResponse resp = new GeneralResponse();
            try
            {
                var inv = new TblInventario();
                inv.IdProducto = inventario.IdProducto;
                inv.Iva = inventario.Iva;
                inv.PrecioUnitario = inventario.PrecioUnitario;
                inv.Cantidad = inventario.Cantidad;
                inv.IdEstado = 1;


                _context.Add(inv);
                await _context.SaveChangesAsync();
                //return true;
                //RESPONSE
                resp.Exito = 1;
                resp.Mensaje = "Exito al Insertar";
                resp.Data = inventario;
                return resp;

            }
            catch (Exception ex)
            {
                resp.Exito = 0;
                resp.Mensaje = "Error al Insertar " + ex.Message;
                return resp;
                //return false;
            }
        }


        public async Task<GeneralResponse> PutInventario([FromBody] InventarioRequest inventario)
        {
            GeneralResponse resp = new GeneralResponse();
            try
            {
                //_context.Update(inventario); hay una manera mas actual que es el con el ENTRY
                var inv = await _context.TblInventarios.FindAsync(inventario.IdInventario);
                inv.IdProducto = inventario.IdProducto;
                inv.Iva = inventario.Iva;
                inv.PrecioUnitario = inventario.PrecioUnitario;
                inv.Cantidad = inventario.Cantidad;
                 inv.IdEstado = 1;

                _context.Entry(inv).State = Microsoft.EntityFrameworkCore.EntityState.Modified;//para actualizar
                await _context.SaveChangesAsync();
                // return true;
                //RESPONSE
                resp.Exito = 1;
                resp.Mensaje = "Exito al Actualizar";
                resp.Data = inventario;
                return resp;
            }
            catch (Exception ex)
            {
                resp.Exito = 0;
                resp.Mensaje = "error al actualizar" + ex.Message;
                return resp;
                // return false;
            }
        }

        public async Task<GeneralResponse> DeleteInventario(InventarioRequest inventario)
        {
            GeneralResponse resp = new GeneralResponse();

            try
            {
                var inve = await _context.TblInventarios.FindAsync(inventario.IdInventario);
                inve.IdEstado = 0;//estaba 2
                //_context.Entry(categoria).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.Entry(inve).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                //_context.TblInventarios.Remove(inventario);
                await _context.SaveChangesAsync();
                resp.Exito = 1;
                resp.Mensaje = "exito al eliminar";
                return resp;
                //return true;
            }
            catch (Exception ex)
            {
                resp.Exito = 0;
                resp.Mensaje = "error al eliminar  " + ex.Message;
                return resp;
                //return false;
            }
        }




    }
}
