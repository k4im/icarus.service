using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using icarus.jwtManager.Models;

namespace icarus.jwtManager.Repository
{
    public interface IRepoAuth
    {
        Task<UsuarioDTO> Registrar(UsuarioDTO request);
        Task<UsuarioDTO> Logar(UsuarioDTO request);
    }
}