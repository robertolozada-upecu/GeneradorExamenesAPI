using AccesoData;
using AutoMapper;
using Modelos;

namespace Negocio.Mapper
{
    /// <summary>
    /// Clase y constructor de perfiles del Mapper
    /// </summary>
    public class PerfilesMapper : Profile
    {
        public PerfilesMapper()
        {
            CreateMap<Examen, ExamenDTO>().ReverseMap();
            CreateMap<Usuario, UsuarioDTO>().ReverseMap();
            CreateMap<Pregunta, PreguntaDTO>().ReverseMap();
            CreateMap<Respuesta, RespuestaDTO>().ReverseMap();
        }
    }
}
