using Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modelos;
using Negocio.Repositorio.IRepositorio;
using System.Security.Claims;

namespace GeneradorExamenes.API.Controllers
{
    /// <summary>
    /// Controlador Swagger de Pregunta
    /// </summary>
    [ApiController]
    [Route("[controller]/[action]")]
    public class PreguntaController : ControllerBase
    {
        private readonly IPreguntaRepositorio _preguntaRepositorio;

        private readonly IExamenRepositorio _examenRepositorio;
        
        //Constructor 
        public PreguntaController(IPreguntaRepositorio preguntaRepositorio, IExamenRepositorio examenRepositorio)
        {
            _preguntaRepositorio = preguntaRepositorio;
            _examenRepositorio = examenRepositorio;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerPregunta()
        {
            return Ok(await _preguntaRepositorio.VerRegistroPregunta());
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Crear(PreguntaDTO registroPreguntaDTO)
        {
            var idExamen = await _examenRepositorio.VerRegistroExamen();
               var tmp = idExamen.Where(e => e.ExamenId == registroPreguntaDTO.ExamenId).FirstOrDefault();

            if (tmp != null && !User.IsInRole(Roles.Administrador))
                return Unauthorized("No tiene permisos para realizar esta operación");

            registroPreguntaDTO.FechaCreacionPregunta = DateTime.Now;
            var resultado = await _preguntaRepositorio.RegistrarPregunta(registroPreguntaDTO);
            return Ok(resultado);
        }

        //[HttpGet("{idUsuario}")]
        //public async Task<IActionResult> ObtenerPreguntas(string idUsuario)
        //{
        //    return Ok(await _preguntaRepositorio.VerRegistroPregunta(idUsuario));
        //}
    }
}
