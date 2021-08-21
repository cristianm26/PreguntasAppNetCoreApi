using BackEnd.Domain.IServices;
using BackEnd.Domain.Model;
using BackEnd.DTO;
using BackEnd.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost]
        public async Task<IActionResult>Post([FromBody]Usuario usuario)
        {
            try
            {
                var ValidateExistence = await _usuarioService.ValidateExistence(usuario);
                if (ValidateExistence)
                {
                    return BadRequest(new { message = "El usuario" + usuario.NombreUsuario + "ya existe " });
                }
                usuario.Password = Encriptar.EncriptarPassword(usuario.Password);
                await _usuarioService.SaveUser(usuario);
                return Ok(new { message = "Usuario Registrado Correctamente" });
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        // localhost:xxx/api/usuario/CambiarPassword
        [Route("CambiarPassword")]
        [HttpPut]
        public async Task<IActionResult> CambiarPassword([FromBody] CambiarPasswordDTO cambiarPassword)
        {
            try
            {
                int idUsuario = 10;
                string passwordEncriptado = Encriptar.EncriptarPassword(cambiarPassword.passwordAnterior);
                var usuario = await _usuarioService.ValidatePassword(idUsuario, passwordEncriptado); 
                if(usuario == null)
                {
                    return BadRequest(new { message = "El password es Incorrecto" });
                }
                else
                {
                    usuario.Password = Encriptar.EncriptarPassword(cambiarPassword.nuevaPassword);
                    await _usuarioService.UpdatePassword(usuario);
                    return Ok(new { message = "El password fue actualizado con éxito! " });
                }
               
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
