namespace Modelos
{
    /// <summary>
    /// DTO de Pregunta para visualización de usuario
    /// </summary>
    public class PreguntaDTO
    {
        public int PreguntaId { get; set; }
        public int ExamenId { get; set; }
        public string Enunciado { get; set; }
        public string Feedback { get; set; }
        public int NumeroOpcionesRespuesta { get; set; }
        public DateTime FechaCreacionPregunta { get; set; }
    }
}
