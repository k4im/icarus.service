using icarus.jwtManager.Models;
using AutoMapper;
using icarus.jwtManager.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace icarus.jwtManager.Repository
{
    public class RepoAuth : IRepoAuth
    {
        /*Propriedades injetadas na classe*/
        readonly IMapper _mapper;
        readonly DataContext _db;
        readonly UserManager<AppUser> _userManager;
        readonly SignInManager<AppUser> _signInManager;
        readonly IConfiguration _config;
        private List<(string, string)> _refreshToken = new ();

        /*Definindo o construtor da calasse*/
        public RepoAuth(IMapper mapper, 
        DataContext db, 
        UserManager<AppUser> userManager, 
        SignInManager<AppUser> signInManager, 
        IConfiguration config)
        {
            _mapper = mapper;
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
        }

        public async Task<RegistroDTO> Registrar(UsuarioDTO request)
        {
            /*
                Este metodo irá registar o usuario ao banco de dados
                Realizando as confirmações de login assim como o já habilitando a conta
                a realizar o login de imediato.
            */
            var IdentityUser = new AppUser
            {
                UserName = request.UserName,
                Email = request.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(IdentityUser, request.Senha);

            if(result.Succeeded) await _userManager.SetLockoutEnabledAsync(IdentityUser, false);
            if(!result.Succeeded && result.Errors.Count() > 0) Console.WriteLine("Erro");
            
            var registroDTO = new RegistroDTO 
            {   UserName = request.UserName,
                Email = request.Email
            };
            return registroDTO;

        }
        public async Task<LogarDTO> Logar(UsuarioDTO request)
        {
            /*
                Este metodo irá receber o request do cliente
                tentará relaizar o login com as credencias fornecidas
                caso seja possivel realizar o login
                o metodo irá gerar um AcessToken e um refresh token
                repassando ambos ao cliente que requeriu,
                
                caso o metodo falhe o mesmo irá retornar o dto informando a falha.
            */
            var result = await _signInManager.PasswordSignInAsync(request.Email, request.Senha, false, true);
            var token = string.Empty;
            var refreshToken = string.Empty;

            if(result.Succeeded) {
                token = await CriarToken(request.Email);
                refreshToken = GerarRefreshToken();
                SalvarRefreshToken(request.Email, token);
                var LoginDTO = new LogarDTO{
                    SucessoAoLogar = true,
                    Email = request.Email,
                    Token = token,
                    RefreshToken = refreshToken
                };
                return LoginDTO;
            }

            var Falha = new LogarDTO{
                SucessoAoLogar = false,
                Email = request.Email,
                Token = "login falhou"
            };
            return Falha;


        }

        private async Task<string> CriarToken(string email)
        {
            /*
                Este metodo irá gerar um novo
                access Token
            */

            var usuario = await _userManager.FindByEmailAsync(email);
            
            var claims = GerarClaims(usuario);

            var secretKey = _config["Jwt:SecretKey"];
            var keyByte = Encoding.UTF8.GetBytes(secretKey);

            var key = new SymmetricSecurityKey(keyByte);
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds,
                audience : _config["Jwt:Audience"],
                issuer : _config["Jwt:Issuer"]
            );
        
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }


        private async void SalvarRefreshToken(string email, string refreshToken)
        {
            var dto = new RefreshToken {
                UserEmail = email,
                TokenRefresh = refreshToken
            };
            await _db.RefreshTokens.AddAsync(dto);
            await _db.SaveChangesAsync();
        }


        private string GerarRefreshToken() 
        {
            /*
                Este metodo irá gerar um novo refresh token para o usuario
                Para que o mesmo quando necessário realize
                o pedido para um novo acess token
            */
            var randomToken = new Guid().ToString();
            byte[] tokenBytes = Encoding.UTF8.GetBytes(randomToken);
            var refreshToken = Convert.ToBase64String(tokenBytes);
            return refreshToken;
        }


        private ClaimsPrincipal PegarPincipalDoTokenAntigo(string token)
        {
            /*
                Metodo que valida e extrai as claims de um token 
                que já está sendo utilizado pelo cliente
            */
            var secretKey = _config["Jwt:SecretKey"];
            var keyByte = Encoding.UTF8.GetBytes(secretKey);

            var key = new SymmetricSecurityKey(keyByte);
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            
            var parametrosValidosToken = new TokenValidationParameters {
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
        private List<Claim> GerarClaims(AppUser user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim("name", user.UserName),
                new Claim("email", user.Email),
                new Claim("key", _config["Jwt:Key"])
            };

            return claims;
        }

        public async Task<string> LogOut()
        {
            await _signInManager.SignOutAsync();
            return "Usuario deslogado!";
        }
    }
}