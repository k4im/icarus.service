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
        readonly IMapper _mapper;
        readonly DataContext _db;
        readonly UserManager<AppUser> _userManager;
        readonly SignInManager<AppUser> _signInManager;
        readonly RoleManager<IdentityUser> _roleManager;
        readonly UserStore<AppUser> _userStoreManager;
        readonly IConfiguration _config;
        public RepoAuth(IMapper mapper, 
        DataContext db, 
        UserManager<AppUser> userManager, 
        SignInManager<AppUser> signInManager, 
        RoleManager<IdentityUser> roleManager, 
        UserStore<AppUser> userStoreManager,
        IConfiguration config)
        {
            _mapper = mapper;
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _config = config;
            _userStoreManager = new UserStore<AppUser>(_db);
        }
        public async Task<UsuarioDTO> Registrar(UsuarioDTO request)
        {
            var IdentityUser = new AppUser
            {
                UserName = request.UserName,
                Email = request.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(IdentityUser, request.Senha);
            if(result.Succeeded) await _userManager.SetLockoutEnabledAsync(IdentityUser, false);
            var usuarioResponse = new UsuarioDTO
            {
                UserName = request.UserName,
                Email = request.Email
            };

            if(!result.Succeeded && result.Errors.Count() > 0) Console.WriteLine("Erro");
            
            return usuarioResponse;

        }
        public async Task<UsuarioDTO> Logar(UsuarioDTO request)
        {
            var result = await _signInManager.PasswordSignInAsync(request.Email, request.Senha, false, true);
            /*Implementar Token*/
            if(result.Succeeded) await CriarToken(request.Email);

            /*Finalizar implementação Identity*/
            return new UsuarioDTO();
        }


        private async Task<string> CriarToken(string email)
        {
            var usuario = await _userManager.FindByEmailAsync(email);
            var claims = GerarClaims(usuario);

            var secretKey = _config["Jwt:SecretKey"];
            var keyByte = Encoding.UTF8.GetBytes(secretKey);

            var key = new SymmetricSecurityKey(keyByte);
            var creds = new SigningCredentials(key, SecurityAlgorithms.RsaSha512);

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
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email)
            };

            return claims;
        }
    }
}