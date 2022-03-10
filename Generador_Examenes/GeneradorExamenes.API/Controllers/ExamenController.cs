using Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modelos;
using Negocio.Repositorio.IRepositorio;
using System.Security.Claims;

namespace GeneradorExamenes.API.Controllers
{
    /// <summary>
    /// Controlador Swagger de Examen
    /// </summary>
    [ApiController]
    [Authorize]
    [Route("[controller]/[action]")]
    public class ExamenController : ControllerBase
    {
        private readonly IExamenRepositorio _examenRepositorio;
        private readonly IPreguntaRepositorio _preguntaRepositorio;
        private readonly IRespuestaRepositorio _respuestaRepositorio;

        public ExamenController(IExamenRepositorio examenRepositorio, IPreguntaRepositorio preguntaRepositorio,
        IRespuestaRepositorio respuestaRepositorio)
        {
            _examenRepositorio = examenRepositorio;
            _preguntaRepositorio = preguntaRepositorio;
            _respuestaRepositorio = respuestaRepositorio;
        }

        /// <summary>
        /// Permite crear nuevos exámenes
        /// </summary>
        /// <param name="registroExamenDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Crear(ExamenDTO registroExamenDTO)
        {
            var idUsuario = (HttpContext.User.Identity as ClaimsIdentity)?
                                .FindFirst("Id")?.Value;

            if (idUsuario != registroExamenDTO.UserId && !User.IsInRole(Roles.Administrador))
                return Unauthorized("No tiene permisos para realizar esta operación");

            registroExamenDTO.FechaCreacionExamen = DateTime.Now;
            var resultado = await _examenRepositorio.RegistrarExamen(registroExamenDTO);
            return Ok(resultado);
        }
        
        [HttpGet]
        public async Task<IActionResult> ObtenerRegistroExamen()
        {
            return Ok(await _examenRepositorio.VerRegistroExamen());
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerExamenCompleto(int idExamen)
        {
            List<int> IdsPreguntas = new();
            var ExamenCompleto = await _examenRepositorio.VerExamenCompleto(idExamen);

            ExamenCompletoDTO examenCompletoDTO = new ExamenCompletoDTO()
            {
                Descripcion = ExamenCompleto.Descripcion,
                ExamenId = ExamenCompleto.ExamenId,
                FechaCreacionExamen = ExamenCompleto.FechaCreacionExamen,
                NombreExamen = ExamenCompleto.NombreExamen,
                TotalPreguntas = ExamenCompleto.TotalPreguntas,
                UserId = ExamenCompleto.UserId,
            };

            var preguntaDTOs = await _preguntaRepositorio.PreguntaPorIdExamen(idExamen);

            examenCompletoDTO.PreguntaCompletoDTOs = new List<PreguntaCompletoDTO>();

            foreach (var preg in preguntaDTOs)
            {
                var preguntaCompletoDTOs = new PreguntaCompletoDTO()
                {
                    Enunciado = preg.Enunciado,
                    ExamenId = preg.ExamenId,
                    FechaCreacionPregunta = preg.FechaCreacionPregunta,
                    Feedback = preg.Feedback,
                    NumeroOpcionesRespuesta = preg.NumeroOpcionesRespuesta,
                    PreguntaId = preg.PreguntaId,
                };

                examenCompletoDTO.PreguntaCompletoDTOs.Add(preguntaCompletoDTOs);

                IdsPreguntas.Add(preg.PreguntaId);
            }
            var respuestas = await _respuestaRepositorio.RespuestaPorIdPregunta(IdsPreguntas);

            if (examenCompletoDTO != null && examenCompletoDTO.PreguntaCompletoDTOs != null)
            {
                foreach (var resp in examenCompletoDTO.PreguntaCompletoDTOs)
                {
                    resp.RespuestaDTOs = respuestas.Where(e => e.PreguntaId == resp.PreguntaId).ToList();
                }
            }
            return Ok(examenCompletoDTO);
        }
    }
}
