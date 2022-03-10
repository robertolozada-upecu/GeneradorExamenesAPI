namespace Modelos
{
    /// <summary>
    /// DTO para la respuesta del registro de usuario
    /// </summary>
    public class RegistroUsuarioResponseDTO
    {
        public bool RegistroSatisfactorio { get; set; }
        public IEnumerable<string> Errores { get; set; }
    }
}
