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
            /*Gera um objeto do padrão IdentityUser após isto seta os valores para os valores do request*/
            var IdentityUser = new AppUser
            {
                UserName = request.UserName,
                Email = request.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(IdentityUser, request.Senha);

            /*Realizando a liberação do usuario criado, neste metodo é possivel criar uma logica
            para realizar a autenticação via email*/
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
            var result = await _signInManager.PasswordSignInAsync(request.Email, request.Senha, false, true);
            var token = string.Empty;
            /*Implementar Token*/
            if(result.Succeeded) {
                token = await CriarToken(request.Email);
                
                /*Finalizar implementação Identity*/
                var LoginDTO = new LogarDTO{
                    SucessoAoLogar = true,
                    Email = request.Email,
                    Token = token
                };
                return LoginDTO;
            }

            var Falha = new LogarDTO{
                SucessoAoLogar = true,
                Email = request.Email,
                Token = "login falhou"
            };
            return Falha;


        }


        private async Task<string> CriarToken(string email)
        {
            var usuario = await _userManager.FindByEmailAsync(email);
            var claims = GerarClaims(usuario);

            var secretKey = _config["Jwt:SecretKey"];
            var keyByte = Encoding.UTF8.GetBytes(secretKey);

            var key = new SymmetricSecurityKey(keyByte);
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds,
                audience : _config["Jwt:Audience"],
                issuer : _config["Jwt:Issuer"]
            );
        
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
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