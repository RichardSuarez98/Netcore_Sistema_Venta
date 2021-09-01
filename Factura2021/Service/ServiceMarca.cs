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
    public class ServiceMarca : InterfaceMarca
    {
        private readonly FacturaContext _context;

        public ServiceMarca(FacturaContext context)
        {
            this._context = context;
        }

        public async Task<List<TblMarca>> GetMarca()
        {   //aqui solo me trae a los estados activo
            List<TblMarca> listMarca = await _context.TblMarcas.Where(estado => estado.IdEstado == 1).ToListAsync();
            return listMarca;
        }

        public async Task<GeneralResponse> PostMarca([FromBody] MarcaRequest marca)
        {
            GeneralResponse resp = new GeneralResponse();
            try
            {//le especifico los campos para que los inserte
                var mar = new TblMarca();
                mar.NombreMarca = marca.NombreMarca;
                mar.IdEstado = 1;
                mar.DescripcionMarca = marca.DescripcionMarca;

                _context.Add(mar);
                await _context.SaveChangesAsync();

                resp.Exito = 1;
                resp.Mensaje = "Exito al insetar";
                resp.Data = marca;
                return resp;
               // return true;
            }
            catch (Exception ex)
            {
                resp.Exito = 0;
                resp.Mensaje = "error al insertar "+ex.Message;
                return resp;
                //return false;
            }
        }

        public async Task<GeneralResponse> PutMarca([FromBody] MarcaRequest marca)
        {
            GeneralResponse resp = new GeneralResponse();
            try
            {
                /*  var mar = new TblMarca();
                  mar.IdMarca = marca.IdMarca;
                  mar.NombreMarca = marca.NombreMarca;
                  mar.DescripcionMarca = marca.DescripcionMarca;*/

                /* var cat = await _context.TblCategoria.FindAsync(categoria.IdCategoria);
                 cat.NombreCategoria = categoria.NombreCategoria;
                 cat.DescripcionCategoria = categoria.DescripcionCategoria;
                 _context.Entry(cat).State = Microsoft.EntityFrameworkCore.EntityState.Modified;*/
                var mar = await _context.TblMarcas.FindAsync(marca.IdMarca);
                mar.NombreMarca = marca.NombreMarca;
                mar.DescripcionMarca = marca.DescripcionMarca;
                mar.IdEstado = 1;
                _context.Entry(mar).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

               // _context.Update(mar);
                await _context.SaveChangesAsync();
                resp.Exito = 1;
                resp.Mensaje = "Buen trabajo, se actualizo correctamente";
                resp.Data = marca;
                return resp;

               // return true;
            }
            catch (Exception ex)
            {
                resp.Exito = 0;
                resp.Mensaje = "error al actualiar"+ex.Message;
                return resp;
              //  return false;
            }
        }

        public async Task<GeneralResponse> DeleteMarca(MarcaRequest marca)
        {
            GeneralResponse resp = new GeneralResponse();
            
            try
            {
                var marcaProduc = _context.TblProductos.Where(m => m.IdMarca == marca.IdMarca).Count();
                if (marcaProduc > 0)
                {
                    resp.Exito = 3;
                    resp.Mensaje = "La marca que desea eliminar esta asociada a :  " + marcaProduc;

                    return resp;
                }

                var mar = await _context.TblMarcas.FindAsync(marca.IdMarca);
                mar.IdEstado = 0;//estaba 2
                _context.Entry(mar).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
              //  _context.Update(marca);

                // _context.TblMarcas.Remove(marca);
                await _context.SaveChangesAsync();
                resp.Exito = 1;
                resp.Mensaje = "Exito al eliminar";
                return resp;
                //return true;
            }
            catch (Exception ex)
            {
                resp.Exito = 0;
                resp.Mensaje="Error al eliminar PILOSOS"+ex.Message;
                return resp;
                //return false;
            }
        }


    }




}
