using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccesoData
{
    /// <summary>
    /// Clase pregunta
    /// </summary>
    public class Pregunta
    {
        [Key]
        public int PreguntaId { get; set; }
        [ForeignKey("ExamenId")]
        public int ExamenId { get; set; }
        public string Enunciado { get; set; }
        public string Feedback { get; set; }
        public int NumeroOpcionesRespuesta { get; set; }
        public DateTime FechaCreacionPregunta { get; set; }
    }
}
