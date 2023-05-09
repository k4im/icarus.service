using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using icarus.jwtManager.Models;

namespace icarus.jwtManager.Repository
{
    public interface IRepoAuth
    {
        Task<RegistroDTO> Registrar(UsuarioDTO request);
        Task<LogarDTO> Logar(LoginDTO request);
        Task<string> LogOut();
        Task<LogarDTO> RefreshToken(RefreshTokenDTO request);
    }
}