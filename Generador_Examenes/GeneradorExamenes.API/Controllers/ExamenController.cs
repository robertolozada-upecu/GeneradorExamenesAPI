using Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modelos;
using Negocio.Repositorio.IRepositorio;
using System.Security.Claims;

namespace GeneradorExamenes.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExamenController : ControllerBase
    {
        private readonly IExamenRepositorio _llamadaRepositorio;

        public ExamenController(IExamenRepositorio llamadaRepositorio)
        {
            _llamadaRepositorio = llamadaRepositorio;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerExamen()
        {
            return Ok(await _llamadaRepositorio.VerRegistroExamen());
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Crear(RegistroExamenDTO registroLlamadaDTO)
        {
            var idUsuario = (HttpContext.User.Identity as ClaimsIdentity)?
                                .FindFirst("Id")?.Value;

            if (idUsuario != registroLlamadaDTO.UserId && !User.IsInRole(Roles.Administrador))
                return Unauthorized("No tiene permisos para realizar esta operacion");

            registroLlamadaDTO.FechaExamen = DateTime.Now;
            var resultado = await _llamadaRepositorio.RegistrarExamen(registroLlamadaDTO);
            return Ok(resultado);
        }

        [HttpGet("{idUsuario}")]
        public async Task<IActionResult> ObtenerLlamadas(string idUsuario)
        {
            return Ok(await _llamadaRepositorio.VerRegistroExamen(idUsuario));
        }
    }
}
