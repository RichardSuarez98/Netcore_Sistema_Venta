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
    public class ServiceProducto: InterfaceProducto
    {
        private readonly FacturaContext _context;

        public ServiceProducto(FacturaContext context)
        {
            this._context = context;
        }//////asasasasasasas
        public async Task<IEnumerable> GetProducto()
        {
            //List<TblProducto> listProducto = await _context.TblProductos.Where(x => x.IdEstado == 1).ToListAsync();

            dynamic res = await _context.TblProductos.Include(x => x.IdMarcaNavigation)
                                                                             .Where(i => i.IdEstado == 1)
                                                                             .Select(x => new
                                                                             {
                                                                               x.IdProducto,
                                                                               x.NombreProducto,
                                                                               x.DescripcionProducto,
                                                                               x.IdMarcaNavigation.NombreMarca,
                                                                               x.IdCategoriaNavigation.NombreCategoria,
                                                                               x.IdEstado

                                                                             }).ToListAsync();
            /*List<TblProducto> listProducto = await _context.TblProductos.Join(_context.TblMarcas, producto=>producto.IdMarca, marca=> marca.IdMarca,
            (producto,marca)=> new { 
            producto, marca
            }).Join(_context.TblCategoria,producto=>producto.producto.IdCategoria,categoria=>categoria.IdCategoria,
            (producto,categoria)=>new { 
            producto.producto,producto.marca,categoria
            }).ToListAsync();*/
            /* List<TblProducto> listProducto = await (from pro in _context.TblProductos
                                                     join cate in _context.TblCategoria on pro.IdCategoria equals cate.IdCategoria
                                                     join mar in _context.TblMarcas on pro.IdMarca equals mar.IdMarca
                                                    select new 
                                                    {
                                                        pro.IdProducto,
                                                        pro.NombreProducto,
                                                        pro.DescripcionProducto,
                                                        cate.NombreCategoria,
                                                        mar.NombreMarca
                                                    }).ToListAsync();*/

            return res;
           // return listProducto;
            /*  SELECT  NombreProducto,DescripcionProducto,NombreMarca,NombreCategoria
                FROM tbl_Producto pro
                JOIN tbl_Marca u ON u.IdMarca = IdProducto 
                JOIN tbl_Categoria b ON b.IdCategoria = IdProducto
                WHERE pro.IdEstado=1;*/

            // return await _context.TblCategoria.ToListAsync();
        }

        public async Task<GeneralResponse> GetProductoBuscar(int id)
        {
            GeneralResponse resp = new GeneralResponse();

            dynamic listProducto = await _context.TblInventarios.Include(x => x.IdProductoNavigation)
                                                                              .Where(i => i.IdEstado == 1 && i.IdProducto==id)
                                                                              .Select(x => new
                                                                              {
                                                                                  x.IdInventario,
                                                                                  x.IdProducto,
                                                                                  x.IdProductoNavigation.NombreProducto,
                                                                                  x.Iva,
                                                                                  x.PrecioUnitario,
                                                                                  x.Cantidad,
                                                                                  x.IdEstado

                                                                              }).FirstOrDefaultAsync();
            if (listProducto == null)
            {
                return null;
            }
            else
            {
                resp.Exito = 1;
                resp.Mensaje = "bien hecho";
                resp.Data = listProducto;
                return resp;
                // return listPersona;
            }      
        }

        public async Task<GeneralResponse> PostProducto([FromBody] ProductoRequest producto)
        {
            GeneralResponse resp = new GeneralResponse();
            try
            {             
                var pro = new TblProducto();
               // pro.IdProducto = producto.IdProducto;
                pro.NombreProducto = producto.NombreProducto;
                pro.DescripcionProducto = producto.DescripcionProducto;
                pro.IdMarca = producto.IdMarca;
                pro.IdCategoria = producto.IdCategoria;
                pro.IdEstado = 1;
                

                _context.Add(pro);
                await _context.SaveChangesAsync();
                resp.Exito = 1;
                resp.Mensaje = "BIEN HECHO";
                resp.Data = producto;
                return resp;
                //return true;
            }
            catch (Exception ex)
            {
                resp.Exito = 0;
                resp.Mensaje = "erro piloso piloso" + ex.Message;
                return resp;
                //return false;
            }
        }


        public async Task<GeneralResponse> PutProducto([FromBody] ProductoRequest producto)
        {
            GeneralResponse resp = new GeneralResponse();
            try
            {
                var pro = await _context.TblProductos.FindAsync(producto.IdProducto);
                pro.NombreProducto = producto.NombreProducto;
                pro.DescripcionProducto = producto.NombreProducto;
                pro.IdMarca = producto.IdMarca;
                pro.IdCategoria = producto.IdCategoria;
                pro.IdEstado = 1;

                // _context.Update(producto);
                _context.Entry(pro).State=Microsoft.EntityFrameworkCore.EntityState.Modified;
                await _context.SaveChangesAsync();
                resp.Exito = 1;
                resp.Mensaje = "piloso todo bien";
                resp.Data = producto;
                return resp;
               // return true;
            }
            catch (Exception ex)
            {
                resp.Exito = 0;
                resp.Mensaje = "error al actualizar"+ ex.Message;
                return resp;
               // return false;
            }
        }

        public async Task<GeneralResponse> DeleteProducto(ProductoRequest producto)
        {
            GeneralResponse resp = new GeneralResponse();
            try
            {

                var produc = await _context.TblProductos.FindAsync(producto.IdProducto);
                produc.IdEstado = 0;//estaba 2
                _context.Entry(produc).State=Microsoft.EntityFrameworkCore.EntityState.Modified;
                //_context.TblProductos.Remove(producto);

                await _context.SaveChangesAsync();
                resp.Exito = 1;
                resp.Mensaje = "Exito al guardar";
                return resp;
                //return true;
            }
            catch (Exception ex)
            {
                resp.Exito = 0;
                resp.Mensaje = "error al eliminar piloso"+ ex.Message;
                return resp;
                //return false;
            }
        }


    }
}
