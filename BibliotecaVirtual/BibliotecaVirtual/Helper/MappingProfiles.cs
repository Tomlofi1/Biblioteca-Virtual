using AutoMapper;
using BibliotecaVirtual.DTO;
using BibliotecaVirtual.Models;

namespace BibliotecaVirtual.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() 
        {
            CreateMap<Livros, LivrosDTO>().ReverseMap();
            CreateMap<Cliente, ClienteDTO>().ReverseMap();

        }
    }
}
