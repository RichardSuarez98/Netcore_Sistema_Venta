using Factura2021.Interface;
using Factura2021.Models;
using Factura2021.Models.Common;
using Factura2021.Models.Request;
using Factura2021.Models.Response;
using Factura2021.Tools;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Factura2021.Service
{
    public class ServiceUsuario : InterfaceUsuario
    {
        private readonly AppSettings _appSettings;
        private readonly FacturaContext _context;
        public ServiceUsuario(IOptions<AppSettings> appSettings,FacturaContext context) 
        {
            _appSettings = appSettings.Value;
            this._context = context;
        }

   

        /*public ServiceUsuario(FacturaContext context)
        {
            this._context = context;
        }*/
        public async Task<IEnumerable> GetUsuario()
        {
           dynamic  listUsuario = await _context.TblUsuarios.Include(x => x.IdPersonaNavigation)
                                                                                       .Where(x => x.IdEstado == 1)
                                                                                       .Select(x => new
                                                                                       { 
                                                                                           x.IdUsuario,
                                                                                           x.NombreUsuario,
                                                                                           x.Password,
                                                                                           x.IdPersonaNavigation.NombrePersona,
                                                                                           x.IdEstado
                                                                                       }).ToListAsync();
            return listUsuario;
            // return await _context.TblCategoria.ToListAsync();
        }

        public async Task<GeneralResponse> PostUsuario([FromBody] UsuarioRequest usuario)
        {
            GeneralResponse resp = new GeneralResponse();
            try
            {
                string spassword = Encrypt.GetSHA256(usuario.password);
                var usu = new TblUsuario();
               // usu.IdUsuario = usuario.IdUsuario;
                usu.NombreUsuario = usuario.NombreUsuario;
                usu.Password = spassword;
              //  usu.Password = usuario.password;
                usu.IdPersona = usuario.IdPersona;
                usu.IdEstado = 1;

                _context.Add(usu);
                await _context.SaveChangesAsync();
                resp.Exito = 1;
                resp.Mensaje = "bien hecho";
                resp.Data = usuario;
                return resp;
               // return true;
            }
            catch (Exception ex)
            {
                resp.Exito = 0;
                resp.Mensaje = "error al insertar usuario"+ex.Message;
                return resp;
            }
        }


        /*public async Task<GeneralResponse> login([FromBody] AuthRequest usuario)esta no essssssssssssssssssssssssssssssssssss
        {
            GeneralResponse resp = new GeneralResponse();
            try
            {
                var user = await _context.TblUsuarios.Where(u => u.NombreUsuario == usuario.NombreUsuario && u.Password == usuario.password).FirstOrDefaultAsync();
                if (user == null)
                {
                    resp.Exito = 0;
                    resp.Mensaje = "Usuario o contraseña inconrrecto";
                    resp.Data = user;
                    return resp;
                }
                else
                {
                    resp.Exito = 1;
                    resp.Mensaje = "Ha ingresado correctamente";
                    resp.Data = user;
                    return resp;
                }

            }
            catch (Exception ex)
            {
                resp.Exito = 0;
                resp.Mensaje = "error al insertar usuario" + ex.Message;
                return resp;
            }

        }*/

        /*public async Task<GeneralResponse> login([FromBody] LoginREquest usuario)
         {
             GeneralResponse resp = new GeneralResponse();
              try
              {
                  var user = await _context.TblUsuarios.Where(u => u.NombreUsuario == usuario.NombreUsuario && u.Password == usuario.password).FirstOrDefaultAsync();
                 // return true;
                 //if (user==null){
                 //   resp.Exito = 0;
                 //   resp.Mensaje = "usuario mal ingresado";
                 //  // resp.Data = user;
                 //   return resp;
                 //}
                 //else
                 //{
                 //resp.Exito = 1;
                 //resp.Mensaje = "Ha ingresado correctamente";
                 //resp.Data = user;
                 //return resp;
                 if (user==null)
                 {
                     resp.Exito = 0;
                     resp.Mensaje = "Usuario o contraseña inconrrecto";
                     resp.Data = user;
                     return resp;
                     //return null;
                 }
                 else
                 {
                     resp.Exito = 1;
                     resp.Mensaje = "Ha ingresado correctamente";
                     resp.Data = user;
                     return resp;
                 }

             }
              catch (Exception ex)
              {
                  resp.Exito = 0;
                  resp.Mensaje = "error al insertar usuario" + ex.Message;
                  return resp;
              }

         }
         */


        public async Task<GeneralResponse> PutUsuario([FromBody] UsuarioRequest usuario)
        {
            GeneralResponse resp = new GeneralResponse();

            try
            {
                string spassword = Encrypt.GetSHA256(usuario.password);
                var usu = await _context.TblUsuarios.FindAsync(usuario.IdUsuario);
                usu.NombreUsuario = usuario.NombreUsuario;
                //usu.Password = usuario.password;
                usu.Password = spassword;
               // usu.IdEstado = usuario.IdEstado;
                usu.IdPersona = usuario.IdPersona;
                
                // _context.Update(usuario);
                _context.Entry(usu).State=Microsoft.EntityFrameworkCore.EntityState.Modified;
                await _context.SaveChangesAsync();
                resp.Exito = 1;
                resp.Mensaje = "bien hecho";
                resp.Data = usuario;
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

        public async Task<GeneralResponse> DeleteUsuario(UsuarioRequest usuario)
        {
            GeneralResponse resp = new GeneralResponse();
            try
            {
                var usu = await _context.TblUsuarios.FindAsync(usuario.IdUsuario);
                usu.IdEstado = 0;//estaba 2
                _context.Entry(usu).State=Microsoft.EntityFrameworkCore.EntityState.Modified;
                // _context.TblUsuarios.Remove(usuario);

                await _context.SaveChangesAsync();
                resp.Exito = 1;
                resp.Mensaje = "bien hecho";
                return resp;
                
                //return true;
            }
            catch (Exception ex)
            {
                resp.Exito = 0;
                resp.Mensaje = "No se elimino"+ex.Message;
                return resp;
                //return false;
            }
        }

      

        public UsuarioResponse Auth(AuthRequest model)
        {
            UsuarioResponse userresponse = new UsuarioResponse();
           // GeneralResponse resp = new GeneralResponse();

            using (var db = new FacturaContext()) { 
                string spassword = Encrypt.GetSHA256(model.password);

            var usuari = db.TblUsuarios.Where(u => u.NombreUsuario == model.NombreUsuario &&
                                                         u.Password == spassword).FirstOrDefault();
                if (usuari == null) {
                    return null;
                }
                var persona = db.TblPersonas.Where(p => p.IdPersona == usuari.IdPersona).FirstOrDefault();

                // userresponse.NombreUsuario = model.NombreUsuario;
                userresponse.NombreUsuario = persona.NombrePersona;
                userresponse.idUsuario = usuari.IdUsuario;
                // userresponse.NombreUsuario = usuari.NombreUsuario;
                userresponse.Token = GetToken(usuari);
               // return userresponse;
            }
            return userresponse;
        }

        private string GetToken(TblUsuario usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var llave = Encoding.ASCII.GetBytes(_appSettings.Secreto);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, usuario.IdUsuario.ToString()),
                        new Claim(ClaimTypes.Name,usuario.NombreUsuario)
                    }
                    ),
                Expires = DateTime.UtcNow.AddDays(60),
                SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(llave), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }





    }

}
