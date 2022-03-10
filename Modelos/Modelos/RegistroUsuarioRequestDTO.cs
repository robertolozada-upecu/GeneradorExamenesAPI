namespace Modelos
{
    /// <summary>
    /// DTO para registro de usuario
    /// </summary>
    public class RegistroUsuarioRequestDTO
    {
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Contrasenia { get; set; }
    }
}
