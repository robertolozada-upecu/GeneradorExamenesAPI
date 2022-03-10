using AccesoData;

namespace Modelos
{
    /// <summary>
    /// DTO de Resouesta para visualización del usuario
    /// </summary>
    public class RespuestaDTO
    {
        public int RespuestaId { get; set; }
        public int PreguntaId { get; set; }
        public string OpcionRespuesta { get; set; }
        public byte OpcionCorrecta { get; set; }
        public DateTime FechaCreacionRespuesta { get; set; }
    }
}
