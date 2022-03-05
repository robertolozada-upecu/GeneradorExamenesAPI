using Modelos;

namespace Negocio.Repositorio.IRepositorio
{
    public interface IExamenRepositorio
    {
        Task<RegistroExamenDTO> RegistrarExamen(RegistroExamenDTO ExamenDTO);
        Task<IEnumerable<RegistroExamenDTO>> VerRegistroExamen();
        Task<IEnumerable<RegistroExamenDTO>> VerRegistroExamen(string idUsuario);
    }
}
