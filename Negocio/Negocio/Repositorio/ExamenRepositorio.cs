using AccesoData;
using AccesoData.Contexto;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Modelos;
using Negocio.Repositorio.IRepositorio;

namespace Negocio.Repositorio
{
    /// <summary>
    /// Métodos de la clase Examen
    /// </summary>
    public class ExamenRepositorio : IExamenRepositorio
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;

        public ExamenRepositorio(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<ExamenDTO> RegistrarExamen(ExamenDTO ExamenDTO)
        {
            var Examen = _mapper.Map<Examen>(ExamenDTO);
            var nuevoExamen = _db.RegistroExamen.Add(Examen);
            await _db.SaveChangesAsync();
            return _mapper.Map<ExamenDTO>(nuevoExamen.Entity);
        }

        public async Task<ExamenDTO> VerExamenCompleto(int idExamen)
        {
            var exa = await _db.RegistroExamen.Where(e => e.ExamenId == idExamen).FirstOrDefaultAsync();
            return _mapper.Map<ExamenDTO>(exa);
        }

        public async Task<IEnumerable<ExamenDTO>> VerRegistroExamen()
        {
            return _mapper.Map<IEnumerable<ExamenDTO>>(_db.RegistroExamen);
        }

        public async Task<IEnumerable<ExamenDTO>> VerRegistroExamen(string idUsuario)
        {
            var listaExamenes = _db.RegistroExamen.Where(Examen => Examen.UserId == idUsuario);
            return _mapper.Map<IEnumerable<ExamenDTO>>(listaExamenes);
        }
    }
}
