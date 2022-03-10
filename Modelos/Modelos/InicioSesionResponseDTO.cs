using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos
{
    /// <summary>
    /// DTO de respuesta al Inicio de sesión
    /// </summary>
    public class InicioSesionResponseDTO
    {
        public bool AutenticacionExistosa { get; set; }
        public string MensajeError { get; set; }
        public string Token { get; set; }
        public UsuarioDTO Usuario { get; set; }
    }
}
