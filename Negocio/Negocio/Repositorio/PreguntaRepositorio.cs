using AccesoData;
using AccesoData.Contexto;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Modelos;
using Negocio.Repositorio.IRepositorio;

namespace Negocio.Repositorio
{
    /// <summary>
    /// Métodos de la clase Pregunta
    /// </summary>
    public class PreguntaRepositorio : IPreguntaRepositorio
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;

        public PreguntaRepositorio(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<PreguntaDTO> RegistrarPregunta(PreguntaDTO PreguntaDTO)
        {
            var Pregunta = _mapper.Map<Pregunta>(PreguntaDTO);
            var nuevoPregunta = _db.RegistroPregunta.Add(Pregunta);
            await _db.SaveChangesAsync();
            return _mapper.Map<PreguntaDTO>(nuevoPregunta.Entity);
        }

        public async Task<IEnumerable<PreguntaDTO>> VerRegistroPregunta()
        {
            return _mapper.Map<IEnumerable<PreguntaDTO>>(_db.RegistroPregunta);
        }
        public async Task<List<PreguntaDTO>> PreguntaPorIdExamen(int idExamen)
        {
            var listaPreguntaes = await _db.RegistroPregunta.Where(Pregunta => Pregunta.ExamenId == idExamen).ToListAsync();
            return _mapper.Map<List<PreguntaDTO>>(listaPreguntaes);
        }
    }
}
