using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using icarus.jwtManager.Models;

namespace icarus.jwtManager.Mapper
{
    public class ProfileMapper : Profile
    {
        public ProfileMapper()
        {
            CreateMap<Usuario, UsuarioDTO>();
        }
    }
}