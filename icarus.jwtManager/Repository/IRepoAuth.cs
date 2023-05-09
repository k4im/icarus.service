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
        Task<LogadoDTO> Logar(LoginDTO request);
        Task<string> LogOut();
        Task<LogadoDTO> RefreshToken(RefreshTokenDTO request);
    }
}