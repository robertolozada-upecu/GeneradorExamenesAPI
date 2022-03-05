using AccesoData;
using Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Modelos;
using Negocio.Repositorio.IRepositorio;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GeneradorExamenes.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CuentaController : ControllerBase
    {
        private readonly SignInManager<Usuario> _signInManager;
        private readonly UserManager<Usuario> _userManager;
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ConfiguracionJWT _configuracionJwt;

        public CuentaController(SignInManager<Usuario> signInManager, UserManager<Usuario> userManager, IUsuarioRepositorio usuarioRepositorio, IOptions<ConfiguracionJWT> opciones)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _usuarioRepositorio = usuarioRepositorio;
            _configuracionJwt = opciones.Value;
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarUsuario([FromBody] RegistroUsuarioRequestDTO registroUsuarioRequestDTO)
        {
            if (registroUsuarioRequestDTO is null)
                return BadRequest();

            var usuario = new Usuario
            {
                UserName = registroUsuarioRequestDTO.Email,
                Email = registroUsuarioRequestDTO.Email,
                Nombre = registroUsuarioRequestDTO.Nombre,
                Calificacion = registroUsuarioRequestDTO.Calificacion,
                EmailConfirmed = true
            };

            var resultadoCreacion = await _userManager.CreateAsync(usuario, registroUsuarioRequestDTO.Contrasenia);

            if (!resultadoCreacion.Succeeded)
                return BadRequest(new RegistroUsuarioResponseDTO
                {
                    RegistroSatisfactorio = false,
                    Errores = resultadoCreacion.Errors.Select(error => error.Description)
                });

            return Ok(new RegistroUsuarioResponseDTO { RegistroSatisfactorio = true });
        }

        [HttpPost]
        public async Task<IActionResult> IniciarSesion([FromBody] InicioSesionRequestDTO inicioSesionRequestDTO)
        {
            if (inicioSesionRequestDTO is null)
                return BadRequest();

            var resultadoInicioSesion = await _signInManager.PasswordSignInAsync(inicioSesionRequestDTO.Correo, inicioSesionRequestDTO.Contrasenia, false, false);

            if (resultadoInicioSesion.Succeeded)
            {
                var usuario = await _userManager.FindByNameAsync(inicioSesionRequestDTO.Correo);
                if (usuario is null)
                    return Unauthorized(new InicioSesionResponseDTO
                    {
                        AutenticacionExistosa = false,
                        MensajeError = "Error al autenticar"
                    });

                var credencialesInicioSesion = ObtenerCredenciales();
                var claims = await ObtenerClaims(usuario);

                var opcionesToken = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddDays(_configuracionJwt.DiasValidez),
                    signingCredentials: credencialesInicioSesion);

                var token = new JwtSecurityTokenHandler().WriteToken(opcionesToken);

                return Ok(new InicioSesionResponseDTO
                {
                    AutenticacionExistosa = true,
                    Token = token,
                    Usuario = new UsuarioDTO
                    {
                        Id = usuario.Id,
                        Nombre = usuario.Nombre,
                        Calificacion = usuario.Calificacion,
                    }
                });
            }

            return Unauthorized(new InicioSesionResponseDTO
            {
                AutenticacionExistosa = false,
                MensajeError = "Error al autenticar"
            });
        }

        //[Authorize(Roles = Roles.Administrador)]
        [HttpPut("{idUsuario}")]
        public async Task<IActionResult> ConvertirAdministrador(string idUsuario)
        {
            var usuario = await _userManager.FindByIdAsync(idUsuario);
            if (usuario == null)
                return BadRequest();

            _userManager.AddToRoleAsync(usuario, Roles.Administrador).GetAwaiter().GetResult();

            return Ok();
        }

        [Authorize(Roles = Roles.Administrador)]
        [HttpGet]
        public async Task<IActionResult> ObtenerUsuarios()
        {
            return Ok(await _usuarioRepositorio.ObtenerUsuarios());
        }

        private async Task<List<Claim>> ObtenerClaims(Usuario usuario)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, usuario.Nombre),
                new Claim(ClaimTypes.Email, usuario.Email),
                new Claim("Id", usuario.Id),
                new Claim("Calificacion", usuario.Calificacion.ToString())
            };

            var roles = await _userManager.GetRolesAsync(usuario);
            foreach (var rol in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, rol));
            }
            return claims;
        }

        private SigningCredentials ObtenerCredenciales()
        {
            var secreto = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuracionJwt.Secreto));
            return new SigningCredentials(secreto, SecurityAlgorithms.HmacSha256);
        }
    }
}
