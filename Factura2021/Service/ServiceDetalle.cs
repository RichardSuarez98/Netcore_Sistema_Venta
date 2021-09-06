using Factura2021.Interface;
using Factura2021.Models;
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
    public class ServiceDetalle: InterfaceDetalle
    {
        private readonly FacturaContext _context;

        public ServiceDetalle(FacturaContext context)
        {
            this._context = context;
        }
       // public async Task<List<TblDetalle>> GetDetalle()
       public async Task<IEnumerable> GetDetalle(int id)
        {
            
            //List<TblDetalle> listDetalle = await _context.TblDetalles.ToListAsync();
            
            dynamic listDetalle =  await _context.TblDetalles.Include(x => x.IdFacturaNavigation)
                                                                           .Where(i => i.IdEstado == 1 && i.IdFactura==id)
                                                                           .Select(x => new
                                                                           {
                                                                               x.IdDetalle,
                                                                               x.IdFactura,
                                                                               x.IdInventarioNavigation.IdProductoNavigation.NombreProducto,
                                                                               x.Cantidad,
                                                                               x.PrecioUnitario,
                                                                               x.Subtotal,
                                                                               x.Total
                                                                           }).ToListAsync();
           
            return listDetalle;
              
            // listDetalle;
            // return await _context.TblCategoria.ToListAsync();
        }

        //public async Task<bool> PostDetalle([FromBody] TblDetalle detalle)
        //{
        //    try
        //    {
        //        _context.Add(detalle);
        //        await _context.SaveChangesAsync();
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //}


        //public async Task<bool> PutDetalle(int id, [FromBody] TblDetalle detalle)
        //{
        //    try
        //    {
        //        _context.Update(detalle);
        //        await _context.SaveChangesAsync();
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {

        //        return false;
        //    }
        //}

        //public async Task<bool> DeleteDetalle(int id)
        //{
        //    var detalle = await _context.TblDetalles.FindAsync(id);
        //    try
        //    {
        //        _context.TblDetalles.Remove(detalle);

        //        await _context.SaveChangesAsync();
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //}



    }
}
