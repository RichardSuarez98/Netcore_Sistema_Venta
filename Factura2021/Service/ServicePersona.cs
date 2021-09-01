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
    public class ServicePersona: InterfacePersona
    {
        private readonly FacturaContext _context;

        public ServicePersona(FacturaContext context)
        {
            this._context = context;
        }
        public async Task<IEnumerable> GetPersona()
        {
            dynamic listPersona = await _context.TblPersonas.Include(x => x.IdTipoPersonaNavigation)
                                                                                     .Where(x => x.IdEstado == 1)
                                                                                     .Select(x => new
                                                                                     {
                                                                                         x.IdPersona,
                                                                                         x.NombrePersona,
                                                                                         x.Cedula,
                                                                                         x.Edad,
                                                                                         x.IdTipoPersonaNavigation.NombreTipoPersona,
                                                                                         x.IdEstado
                                                                                     }).ToListAsync();
            return listPersona;
            /*List<TblPersona> listPersona = await _context.TblPersonas.Where(x => x.IdEstado == 1).ToListAsync();
            return listPersona;*/
            // return await _context.TblCategoria.ToListAsync();
        }

        public async Task<GeneralResponse> PostPersona([FromBody] PersonaRequest persona)
        {
            GeneralResponse resp = new GeneralResponse();
            try
            {
                var per = new TblPersona();
               // per.IdPersona = persona.IdPersona;
                per.NombrePersona = persona.NombrePersona;
                per.Cedula = persona.Cedula;
                per.Edad = persona.Edad;
                per.IdTipoPersona = persona.IdTipoPersona;
                per.IdEstado = 1;

                 _context.TblPersonas.Add(per);
               await _context.SaveChangesAsync();
                resp.Exito = 1;
                resp.Mensaje = "bien hecho";
                resp.Data = persona;
                return resp;
                //return true;
            }
            catch (Exception ex)
            {
                resp.Exito = 0;
                resp.Mensaje = "error al isnertar"+ex.Message;
                return resp;
               // return false;
            }
        }


        public async Task<GeneralResponse> PutPersona([FromBody] PersonaRequest persona)
        {
            GeneralResponse resp = new GeneralResponse();
            try
            {
                var per = await _context.TblPersonas.FindAsync(persona.IdPersona);
                per.NombrePersona = persona.NombrePersona;
                per.Cedula = persona.Cedula;
                per.Edad = persona.Edad;
                per.IdTipoPersona = persona.IdTipoPersona;
                per.IdEstado = 1;
                _context.Entry(per).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                //_context.Update(persona);
                await _context.SaveChangesAsync();
                resp.Exito = 1;
                resp.Mensaje = "exito al actualizar";
                resp.Data = persona;
                return resp;
                
               // return true;
            }
            catch (Exception ex)
            {
                resp.Exito = 0;
                resp.Mensaje = "error al actualizar" + ex.Message;
                return resp;
               // return false;
            }
        }
        // ELIMINACION LOGICA
        public async Task<GeneralResponse> DeletePersona(PersonaRequest persona)
        {
            GeneralResponse resp = new GeneralResponse();
           
            try
            {
                var per = await _context.TblPersonas.FindAsync(persona);
                per.IdEstado = 0;//estaba 2
                _context.Entry(per).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                //_context.TblPersonas.Remove(persona);
                await _context.SaveChangesAsync();
                resp.Exito = 1;
                resp.Mensaje = "Exito al eliminar";
                return resp;
                
                //return true;
            }
            catch (Exception ex)
            {
                resp.Exito = 0;
                resp.Mensaje = "error al eliminar pilososososos"+ ex.Message;
                return resp;
                //return false;
            }
        }
    }



}
