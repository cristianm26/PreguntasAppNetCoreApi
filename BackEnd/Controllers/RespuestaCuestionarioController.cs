﻿using BackEnd.Domain.IServices;
using BackEnd.Domain.Model;
using BackEnd.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RespuestaCuestionarioController : ControllerBase
    {
        private readonly IRespuestaCuestionarioService _respuestaCuestionarioService;
        private readonly ICuestionarioService _cuestionarioService; 
        public RespuestaCuestionarioController(IRespuestaCuestionarioService respuestaCuestionarioService, 
            ICuestionarioService cuestionarioService)
        {
            _respuestaCuestionarioService = respuestaCuestionarioService;
            _cuestionarioService = cuestionarioService;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] RespuestaCuestionario respuestaCuestionario)
        {
            try
            {
                await _respuestaCuestionarioService.SaveRespuestaCuestionario(respuestaCuestionario);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{idCuestionario}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Get(int idCuestionario)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;

                int idUsuario = JwtConfigurator.GetTokenIdUsuario(identity);
                var listRespuestaCuestionario = await _respuestaCuestionarioService.ListRespuestaCuestionario(idCuestionario, idUsuario);
                if (listRespuestaCuestionario == null)
                {
                    return BadRequest(new { message = "Error al buscar el listado de Respuestas" });

                }
                return Ok(listRespuestaCuestionario);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                int idUsuario = JwtConfigurator.GetTokenIdUsuario(identity);

                // Creamos un metodo para obtener la respuesta al cuestionario
                var respuestaCuestionario = await _respuestaCuestionarioService.BuscarRespuestaCuestionario(id, idUsuario);
                if (respuestaCuestionario== null)
                {
                    return BadRequest(new { message = "Error al buscar la respuesta al cuestionario" });
                }
                await _respuestaCuestionarioService.EliminarRespuestaCuestionario(respuestaCuestionario);
                return Ok(new { message = "La respuesta al cuestionario fue eliminada con éxito" });


            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [Route("GetCuestionarioByIdRespuesta/{idRespuesta}")]
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetCuestionarioByIdRespuesta(int idRespuesta)
        {
            try
            {
                // Obtenemos el idCuestionario dado un idRespuesta
                int idCuestionario = await _respuestaCuestionarioService.GetIdCuestionarioByIdRespuesta(idRespuesta);

                //Buscamos el cuestionario (Ya lo tenemos)
                var cuestionario = await _cuestionarioService.GetCuestionario(idCuestionario);

                // Buscamos las respuestas seleccionadas dado un idRespuesta 
                var listRespuestas = await _respuestaCuestionarioService.GetListRespuestas(idRespuesta);
                return Ok(new { cuestionario = cuestionario, respuestas = listRespuestas });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
