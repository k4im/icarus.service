using AutoMapper;
using icarus.fornecedores.Models;

namespace icarus.fornecedores.Mapper
{
    public class MapperProfile : Profile
    {
        protected MapperProfile()
        {
            CreateMap<FornecedorDTO, Fornecedor>();
            CreateMap<Fornecedor, FornecedorDTO>();
        }
    }
}