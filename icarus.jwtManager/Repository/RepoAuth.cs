using icarus.jwtManager.Models;
using AutoMapper;
using icarus.jwtManager.Data;
using Microsoft.EntityFrameworkCore;

namespace icarus.jwtManager.Repository
{
    public class RepoAuth : IRepoAuth
    {
        private readonly IMapper _mapper;
        private readonly DataContext _db;

        public RepoAuth(IMapper mapper, DataContext db)
        {
            _mapper = mapper;
            _db = db;
        }

        public async Task<UsuarioDTO> Logar(UsuarioDTO request)
        {
            var usuario = await _db.Usuarios.FirstOrDefaultAsync(x => x.UserName == request.UserName);
            if(request.UserName == usuario)
        }

        public async Task<UsuarioDTO> Registrar(UsuarioDTO request)
        {
            Usuario user = new Usuario();

            string pwdHashed = BCrypt.Net.BCrypt.HashPassword(request.Password);

            user.UserName = request.UserName;
            user.Password = pwdHashed;

            await _db.Usuarios.AddAsync(user);
            await _db.SaveChangesAsync();

            var usuarioMapper = _mapper.Map<Usuario, UsuarioDTO>(user);

            return usuarioMapper;
        }
    }
}