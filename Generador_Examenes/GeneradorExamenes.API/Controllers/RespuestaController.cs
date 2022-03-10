using Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modelos;
using Negocio.Repositorio.IRepositorio;
using System.Security.Claims;

namespace GeneradorExamenes.API.Controllers
{
    /// <summary>
    /// Controlador Swagger de Respuesta
    /// </summary>
    [ApiController]
    [Route("[controller]/[action]")]
    public class RespuestaController : ControllerBase
    {
        private readonly IRespuestaRepositorio _respuestaRepositorio;

        private readonly IPreguntaRepositorio _preguntaRepositorio;

        //Constructor 
        public RespuestaController(IRespuestaRepositorio respuestaRepositorio, IPreguntaRepositorio preguntaRepositorio)
        {
            _respuestaRepositorio = respuestaRepositorio;
            _preguntaRepositorio = preguntaRepositorio;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerRespuesta()
        {
            return Ok(await _respuestaRepositorio.VerRegistroRespuesta());
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Crear(RespuestaDTO registroRespuestaDTO)
        {
            var idPregunta = await _preguntaRepositorio.VerRegistroPregunta();
            var tmp = idPregunta.Where(e => e.PreguntaId == registroRespuestaDTO.PreguntaId).FirstOrDefault();

            if (tmp != null && !User.IsInRole(Roles.Administrador))
                return Unauthorized("No tiene permisos para realizar esta operación");

            registroRespuestaDTO.FechaCreacionRespuesta = DateTime.Now;
            var resultado = await _respuestaRepositorio.RegistrarRespuesta(registroRespuestaDTO);
            return Ok(resultado);
        }

        //[HttpGet("{idUsuario}")]
        //public async Task<IActionResult> ObtenerRespuestas(string idUsuario)
        //{
        //    return Ok(await _respuestaRepositorio.VerRegistroRespuesta(idUsuario));
        //}
    }
}
