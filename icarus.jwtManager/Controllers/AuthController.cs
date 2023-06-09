using icarus.jwtManager.Models;
using icarus.jwtManager.Repository;
using Microsoft.AspNetCore.Mvc;

namespace icarus.jwtManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IRepoAuth _repo;

        public AuthController(IRepoAuth repo)
        {
            _repo = repo;
        }

        [HttpPost("registrar")]
        public async Task<IActionResult> Registro(UsuarioDTO request)
        {
            try
            {
                var checarRole = (request.Role.Perfil.ToLower() != "admin" && request.Role.Perfil.ToLower() != "supervisor") ?  true : false;
                if(checarRole) return BadRequest("O role precisa ser (admin) ou (supervisor)");
                var usuarioCriado = await _repo.Registrar(request);
                return Ok(usuarioCriado);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ocorreu um erro na execução: {e.Message}");
            } 
            return StatusCode(500);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO request)
        {
            try
            {
                var usuarioLogado = await _repo.Logar(request);
                return Ok(usuarioLogado);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ocorreu um erro na execução: {e.Message}");
            } 
            return StatusCode(500);
        }



        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken(RefreshTokenDTO request)
        {
            try
            {
                var refreshToken = await _repo.RefreshToken(request);
                return Ok(refreshToken);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ocorreu um erro na execução: {e.Message}");
            } 
            return StatusCode(500);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> LogOut()
        {
            try
            {
                var logOut = await _repo.LogOut();
                return Ok(logOut);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Não foi possivel realizar Logout: {e.Message}");
            }
            return StatusCode(500);

        }
    }
}