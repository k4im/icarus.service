using System.Security.Claims;
using System.Text;
using icarus.jwtManager.Data;
using icarus.jwtManager.Models;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;

namespace icarus.jwtManager.Repository
{
    public class RepoAuthExtend :IRepoAuthExtend
    {

        readonly IConfiguration _config;
        readonly DataContext _db;

        public RepoAuthExtend(IConfiguration config, DataContext db)
        {
            _config = config;
            _db = db;
        }

        public async Task DeletarRefreshToken(string username)
        {
            var refreshToken = await _db.RefreshTokens.FirstOrDefaultAsync(x => x.ChaveDeAcesso == username);
            if(refreshToken == null) Results.NotFound();
            _db.RefreshTokens.Remove(refreshToken);
            await _db.SaveChangesAsync();
        }


        public async Task SalvarRefreshToken(RefreshToken request)
        {
           
            var dto = new RefreshToken {
                ChaveDeAcesso = request.ChaveDeAcesso,
                TokenRefresh = request.TokenRefresh,
                CriadoEm = request.CriadoEm,
                ExpiraEm = request.ExpiraEm
            };
            _db.RefreshTokens.Add(dto);
            await _db.SaveChangesAsync();    
        }


        public RefreshToken GerarRefreshToken(string usuario) 
        {
            /*
                Este metodo irá gerar um novo refresh token para o usuario
                Para que o mesmo quando necessário realize
                o pedido para um novo acess token
            */
            var random = new Random(); 
            const string pool = "abcdefghijklmnopqrstuvwxyz0123456789";
            var chars = Enumerable.Range(0, 150).Select(x => pool[random.Next(0, pool.Length)]);
            var randomToken = new string(chars.ToArray());          
            byte[] tokenBytes = Encoding.UTF8.GetBytes(randomToken.ToString());
            var tokenConverted = Convert.ToBase64String(tokenBytes);
            
            var refreshToken = new RefreshToken
            {
                ChaveDeAcesso = usuario,
                TokenRefresh = tokenConverted,
                CriadoEm = DateTime.UtcNow,
                ExpiraEm = DateTime.UtcNow.AddHours(1)
            };
            return refreshToken;
        }



        public async Task<RefreshToken> BuscarRefreshToken(string username, string refreshToken)
        {
            var token = await _db.RefreshTokens.FirstOrDefaultAsync(x => x.ChaveDeAcesso == username && x.TokenRefresh == refreshToken);
            if(token == null) Results.NotFound("Não existe um refresh token para esse usuario");
            return token;
        }


        public ClaimsPrincipal PegarPincipalDoTokenAntigo(string token)
        {
            /*
                Metodo estará realizando a validação do token antigo;
            */
            var secretKey = _config["Jwt:SecretKey"];
            var keyByte = Encoding.UTF8.GetBytes(secretKey);

            var key = new SymmetricSecurityKey(keyByte);
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var parametrosValidosToken = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, parametrosValidosToken, out var securityToken);
        
            return principal;
        }
        
    }
}