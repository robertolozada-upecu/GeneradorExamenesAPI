using AccesoData;
using AccesoData.Contexto;
using AutoMapper;
using Modelos;
using Negocio.Repositorio.IRepositorio;

namespace Negocio.Repositorio
{
    /// <summary>
    /// Métodos de la clase Respuesta
    /// </summary>
    public class RespuestaRepositorio : IRespuestaRepositorio
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;

        public RespuestaRepositorio(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<RespuestaDTO> RegistrarRespuesta(RespuestaDTO RespuestaDTO)
        {
            var Respuesta = _mapper.Map<Respuesta>(RespuestaDTO);
            var nuevoRespuesta = _db.RegistroRespuesta.Add(Respuesta);
            await _db.SaveChangesAsync();
            return _mapper.Map<RespuestaDTO>(nuevoRespuesta.Entity);
        }

        public async Task<IEnumerable<RespuestaDTO>> VerRegistroRespuesta()
        {
            return _mapper.Map<IEnumerable<RespuestaDTO>>(_db.RegistroRespuesta);
        }

        public async Task<List<RespuestaDTO>> RespuestaPorIdPregunta(List<int> IdPreguntas)
        {
            var listaRespuestas = _db.RegistroRespuesta.Where(Respuesta => IdPreguntas.Contains(Respuesta.PreguntaId));
            return _mapper.Map<List<RespuestaDTO>>(listaRespuestas);
        }
    }
}
