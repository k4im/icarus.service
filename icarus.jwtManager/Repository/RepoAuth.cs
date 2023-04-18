using icarus.jwtManager.Models;
using AutoMapper;
using icarus.jwtManager.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

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

        public RepoAuth(IMapper mapper, 
        DataContext db, 
        UserManager<AppUser> userManager, 
        SignInManager<AppUser> signInManager, 
        RoleManager<IdentityUser> roleManager, 
        UserStore<AppUser> userStoreManager)
        {
            _mapper = mapper;
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
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

            /*Finalizar implementação Identity*/
            return new UsuarioDTO();
        }


    }
}