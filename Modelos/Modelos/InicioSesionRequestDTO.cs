namespace Modelos
{
    /// <summary>
    /// DTO de petición de inicio de sesión
    /// </summary>
    public class InicioSesionRequestDTO
    {
        public string Correo { get; set; }
        public string Contrasenia { get; set; }
    }
}
