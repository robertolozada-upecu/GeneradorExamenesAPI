using Modelos;

namespace Negocio.Repositorio.IRepositorio
{
    /// <summary>
    /// Interfaz del Repositorio Pregunta
    /// </summary>
    public interface IPreguntaRepositorio
    {
        Task<PreguntaDTO> RegistrarPregunta(PreguntaDTO PreguntaDTO);
        Task<IEnumerable<PreguntaDTO>> VerRegistroPregunta();
        Task<List<PreguntaDTO>> PreguntaPorIdExamen(int idExamen);
    }
}