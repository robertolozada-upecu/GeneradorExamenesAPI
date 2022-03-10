using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccesoData
{
    /// <summary>
    /// Clase Respuesta
    /// </summary>
    public class Respuesta
    {
        [Key]
        public int RespuestaId { get; set; }
        [ForeignKey("PreguntaId")]
        public int PreguntaId { get; set; }
        public string OpcionRespuesta { get; set; }
        public bool OpcionCorrecta { get; set; }
        public DateTime FechaCreacionRespuesta { get; set; }
    }
}
