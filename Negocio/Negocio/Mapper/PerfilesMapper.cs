using AccesoData;
using AutoMapper;
using Modelos;

namespace Negocio.Mapper
{
    public class PerfilesMapper : Profile
    {
        public PerfilesMapper()
        {
            CreateMap<RegistroExamen, RegistroExamenDTO>().ReverseMap();
            CreateMap<Usuario, UsuarioDTO>().ReverseMap();
        }
    }
}
