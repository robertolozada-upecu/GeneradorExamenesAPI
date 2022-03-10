namespace Modelos
{
    /// <summary>
    /// DTO de Examen para visualización de usuario
    /// </summary>
    public class ExamenDTO
    {
        public int ExamenId { get; set; }
        public string UserId { get; set; }
        public string NombreExamen { get; set; }
        public string? Descripcion { get; set; }
        public int TotalPreguntas { get; set; }
        public DateTime? FechaCreacionExamen { get; set; }
    }
}
