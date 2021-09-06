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
    public class ServiceFactura: InterfaceFactura
    {
        private const double V = 0.01;
        private readonly FacturaContext _context;

        public ServiceFactura(FacturaContext context)
        {
            this._context = context;
        }
        public async Task<IEnumerable> GetFactura()
        {
            //  List<TblFactura> listFactura = await _context.TblFacturas.ToListAsync();
            /*  dynamic listFactura = await _context.TblFacturas.Include(x => x.IdUsuarioNavigation)
                                                                                 .Where(i => i.IdEstado == 1)
                                                                                 .Select(x => new
                                                                                 {
                                                                                     x.IdFactura,
                                                                                     x.IdUsuarioNavigation.NombreUsuario,
                                                                                     x.IdPersonaNavigation.Cedula,
                                                                                     x.FechaEmision,
                                                                                     x.TotalFactura,
                                                                                     x.TotalProducto
                                                                                 }).ToListAsync();*/
            dynamic listFactura = await (from f in _context.TblFacturas
                                   join u in _context.TblUsuarios on f.IdUsuario equals u.IdUsuario
                                   join p in _context.TblPersonas on u.IdPersona equals p.IdPersona
                                   join p2 in _context.TblPersonas on f.IdPersona equals p2.IdPersona
                                   select new
                                   {
                                       f.IdFactura,
                                       Cajero=p.NombrePersona,
                                       Cliente = p2.NombrePersona,
                                       Cedula=p2.Cedula,
                                       FechaEmision=f.FechaEmision,
                                       TotalFactura=f.TotalFactura
                                   }).ToListAsync();
            return listFactura;
            // return await _context.TblCategoria.ToListAsync();
        }

        public async Task<GeneralResponse> PostFactura([FromBody] RequestFactura factura)
        {
            //RESPONSE
            GeneralResponse resp = new GeneralResponse();
            using (FacturaContext db = new FacturaContext())
            {
                using(var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        int cantidad = 0;
                        decimal total = 0;
                        var venta = new TblFactura();
                        venta.FechaEmision = DateTime.Now;
                        venta.IdUsuario = factura.IdUsuario;
                        venta.IdPersona = factura.IdPersona;
                        venta.TotalProducto = 0;
                        venta.TotalProducto = 0;
                        venta.IdEstado = 1;
                        db.TblFacturas.Add(venta);
                        db.SaveChanges();

                        foreach (var detalle in factura.detalles)
                        {

                            var dt = new TblDetalle();
                            dt.Cantidad = detalle.Cantidad;
                            dt.IdInventario = detalle.IdInventario;
                           
                      
                            var iva = db.TblInventarios.Where(inv => inv.IdInventario == dt.IdInventario).FirstOrDefault();
                            dt.PrecioUnitario = iva.PrecioUnitario;
                           
                            var ivaF = iva.Iva;
                            dt.Subtotal = detalle.Cantidad * dt.PrecioUnitario;
                            dt.Total = ivaF * dt.Subtotal;
                            dt.IdFactura = venta.IdFactura;
                            dt.IdEstado = 1;
                            cantidad += (int)dt.Cantidad;
                            total += (decimal)dt.Total;
                            db.Add(dt);
                            db.SaveChanges();

                            TblInventario inv = db.TblInventarios.Find(dt.IdInventario);
                            var cant = inv.Cantidad - dt.Cantidad;
                            inv.Cantidad = cant;
                            db.Entry(inv).State = Microsoft.EntityFrameworkCore.EntityState.Modified; ;
                            db.SaveChanges();

                        }

                        TblFactura f = db.TblFacturas.Find(venta.IdFactura);
                        f.TotalProducto = cantidad;
                        f.TotalFactura = (decimal)total;
                        db.Entry(f).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        db.SaveChanges();
                        transaction.Commit();

                        //Response
                        //return true;
                        
                        resp.Exito = 1;
                        resp.Mensaje = "Insercion con exito";
                        resp.Data = factura;
                        return resp;


                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        //RESPONSE
                        resp.Exito = 0;
                        resp.Mensaje = "Insercion fallida"+e.Message;
                        
                        return resp;
                        //return false;
                    }
                   


                }
            }
            

        }



    }

}
