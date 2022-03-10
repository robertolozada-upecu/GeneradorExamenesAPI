using Modelos;

namespace Negocio.Repositorio.IRepositorio
{
    /// <summary>
    /// Interfaz del Repositorio Examen
    /// </summary>
    public interface IExamenRepositorio
    {
        Task<ExamenDTO> RegistrarExamen(ExamenDTO ExamenDTO);
        Task<IEnumerable<ExamenDTO>> VerRegistroExamen();
        Task<IEnumerable<ExamenDTO>> VerRegistroExamen(string idUsuario);
        Task<ExamenDTO> VerExamenCompleto(int idExamen);
    }
}
