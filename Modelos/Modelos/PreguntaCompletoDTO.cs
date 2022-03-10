namespace Modelos
{
    /// <summary>
    /// DTO para desplegar preguntas corresppndientes en Examen completo, se separa porque variables virtuales no se Mapean automáticamente
    /// </summary>
    public class PreguntaCompletoDTO
    {
        public int PreguntaId { get; set; }
        public int ExamenId { get; set; }
        public string Enunciado { get; set; }
        public string Feedback { get; set; }
        public int NumeroOpcionesRespuesta { get; set; }
        public DateTime FechaCreacionPregunta { get; set; }
        public virtual List<RespuestaDTO>? RespuestaDTOs { get; set; }
    }
}
