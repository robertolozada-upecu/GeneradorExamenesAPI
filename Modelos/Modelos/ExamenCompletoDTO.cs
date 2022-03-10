namespace Modelos
{
    /// <summary>
    /// DTO para desplegar examen completo, se separa porque variables virtuales no se Mapean automáticamente
    /// </summary>
    public class ExamenCompletoDTO
    {
        public int ExamenId { get; set; }
        public string UserId { get; set; }
        public string NombreExamen { get; set; }
        public string? Descripcion { get; set; }
        public int TotalPreguntas { get; set; }
        public DateTime? FechaCreacionExamen { get; set; }
        public virtual List<PreguntaCompletoDTO>? PreguntaCompletoDTOs { get; set; }
    }
}
