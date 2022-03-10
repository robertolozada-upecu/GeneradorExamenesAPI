using Modelos;

namespace Negocio.Repositorio.IRepositorio
{
    /// <summary>
    /// Interfaz del Repositorio Respuesta
    /// </summary>
    public interface IRespuestaRepositorio
    {
        Task<RespuestaDTO> RegistrarRespuesta(RespuestaDTO RespuestaDTO);
        Task<IEnumerable<RespuestaDTO>> VerRegistroRespuesta();
        Task<List<RespuestaDTO>> RespuestaPorIdPregunta(List<int> IdPreguntas);
    }
}