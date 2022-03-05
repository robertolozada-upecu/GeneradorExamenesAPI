using AccesoData.Contexto;
using AutoMapper;
using Modelos;
using Negocio.Repositorio.IRepositorio;

namespace Negocio.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;

        public UsuarioRepositorio(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UsuarioDTO>> ObtenerUsuarios()
        {
            return _mapper.Map<IEnumerable<UsuarioDTO>>(_db.Usuarios);
        }
    }
}
