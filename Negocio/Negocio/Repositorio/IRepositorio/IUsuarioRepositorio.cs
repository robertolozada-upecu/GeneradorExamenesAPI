using Modelos;

namespace Negocio.Repositorio.IRepositorio
{
    /// <summary>
    /// Interfaz del Repositorio Usuario
    /// </summary>
    public interface IUsuarioRepositorio
    {
        Task<IEnumerable<UsuarioDTO>> ObtenerUsuarios();
    }
}
