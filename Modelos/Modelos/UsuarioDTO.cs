namespace Modelos
{
    /// <summary>
    /// DTO Usuario para visualización del usuario final
    /// </summary>
    public class UsuarioDTO
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaCreacionUsuario { get; set; }
    }
}