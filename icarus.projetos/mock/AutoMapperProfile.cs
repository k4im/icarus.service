using AutoMapper;
using icarus.projetos.models;
using icarus.projetos.models.ModelsShared;

namespace icarus.projetos.mock
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Project, ProjectDTO>();
            CreateMap<Project, PublishProject>();
        }
    }
}