using icarus.jwtManager.Models;
using AutoMapper;
using icarus.jwtManager.Data;
using Microsoft.AspNetCore.Identity;
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
        readonly IRepoAuthExtend _refreshTokenService;
        
        /*Definindo o construtor da calasse*/
        public RepoAuth(IMapper mapper, 
        DataContext db, 
        UserManager<AppUser> userManager, 
        SignInManager<AppUser> signInManager, 
        IConfiguration config,
        IRepoAuthExtend refreshTokenService)
        {
            _mapper = mapper;
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
            _refreshTokenService = refreshTokenService;
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
                UserName = request.Email,
                Email = request.Email,
                EmailConfirmed = true,
                
            };
            var usuario = await _userManager.FindByEmailAsync(request.Email);
            if(usuario == null) 
            {
                var result = await _userManager.CreateAsync(IdentityUser, request.Senha);

                if(result.Succeeded) await _userManager.SetLockoutEnabledAsync(IdentityUser, false);
                if(!result.Succeeded && result.Errors.Count() > 0) Console.WriteLine("Erro");
                
                var registroDTO = new RegistroDTO 
                {
                    Email = request.Email,
                    Mensagem = "Usuario criado com sucesso!"
                };
                return registroDTO;
            }
            return new RegistroDTO { Mensagem = "Este usuario já existe" };

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

            if (result.Succeeded) {
                token = await CriarToken(request.Email);
                var refreshToken = _refreshTokenService.GerarRefreshToken(request.Email);
                await _refreshTokenService.SalvarRefreshToken(refreshToken);

                var LoginDTO = new LogarDTO{
                    SucessoAoLogar = true,
                    Email = request.Email,
                    Token = token,
                    RefreshToken = refreshToken.TokenRefresh,
                    ExpiraEm = DateTime.Now.AddHours(1),
                    Mensagem = "Login realizado com sucesso!"
                };
                return LoginDTO;
            }

            var Falha = new LogarDTO{
                Mensagem = "Não foi possivel realizar o login"
            };
            return Falha;


        }


        public async Task<string> LogOut()
        {
            await _signInManager.SignOutAsync();
            return "Usuario deslogado!";
        }

        public async Task<LogarDTO> RefreshToken(RefreshTokenDTO request)
        {

            /*
                Este metodo ira gerar um novo AcessToken e um novo RefreshToken
                Para isso ele irá primeiramente pegar as claims do token atual
                irá verificar se ambos os tokens de refresh são iguais, 
                caso o resultado seja negativo ele irá gerar uma exception,
                em caso de sucesso será gerado um novo token assim como
                um novo refresh token e enviado na resposta
            */
            var principal = _refreshTokenService.PegarPincipalDoTokenAntigo(request.Token);
            var userName = principal.Identity.Name;
            var refreshTokenSalvo = await _refreshTokenService.BuscarRefreshToken(request.UserName, request.RefreshToken);
            
            if(refreshTokenSalvo.TokenRefresh != request.RefreshToken) throw new SecurityTokenException("Token invalido");
            if (refreshTokenSalvo.ExpiraEm <= DateTime.Now) throw new SecurityTokenException("Token Expirado");
            
            var novoToken = await CriarToken(request.UserName);
            var novoRefreshToken = _refreshTokenService.GerarRefreshToken(userName);
            
            var refreshTokenDTO = new RefreshToken {
                UserEmail = request.UserName,
                TokenRefresh = novoRefreshToken.TokenRefresh,
                CriadoEm = DateTime.UtcNow,
                ExpiraEm = DateTime.UtcNow.AddHours(1)
            };
            await _refreshTokenService.DeletarRefreshToken(request.UserName);
            await _refreshTokenService.SalvarRefreshToken(refreshTokenDTO);

            return new LogarDTO {
                SucessoAoLogar = true,
                Email = userName,
                Token = novoToken,
                RefreshToken = novoRefreshToken.TokenRefresh
            };

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

    }
}