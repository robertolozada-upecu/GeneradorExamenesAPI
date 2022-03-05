using AccesoData;
using AccesoData.Contexto;
using AutoMapper;
using Modelos;
using Negocio.Repositorio.IRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Repositorio
{
    public class ExamenRepositorio : IExamenRepositorio
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;

        public ExamenRepositorio(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<RegistroExamenDTO> RegistrarExamen(RegistroExamenDTO ExamenDTO)
        {
            var Examen = _mapper.Map<RegistroExamen>(ExamenDTO);
            var nuevoExamen = _db.RegistroExamen.Add(Examen);
            await _db.SaveChangesAsync();
            return _mapper.Map<RegistroExamenDTO>(nuevoExamen.Entity);
        }

        public async Task<IEnumerable<RegistroExamenDTO>> VerRegistroExamen()
        {
            return _mapper.Map<IEnumerable<RegistroExamenDTO>>(_db.RegistroExamen);
        }

        public async Task<IEnumerable<RegistroExamenDTO>> VerRegistroExamen(string idUsuario)
        {
            var listaExamenes = _db.RegistroExamen.Where(Examen => Examen.UserId == idUsuario);
            return _mapper.Map<IEnumerable<RegistroExamenDTO>>(listaExamenes);
        }
    }
}
