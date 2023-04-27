using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using icarus.jwtManager.Models;

namespace icarus.jwtManager.Repository
{
    public interface IRepoAuthExtend
    {
        Task<RefreshToken> BuscarRefreshToken(string username);

        ClaimsPrincipal PegarPincipalDoTokenAntigo(string token);

        RefreshToken GerarRefreshToken(string usuario);

        Task SalvarRefreshToken(RefreshToken request);

        Task DeletarRefreshToken(string username);
    }
}