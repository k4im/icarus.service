/*
    1) Metodo superior que depende do metodo de abstração para realizar um registro novo
    para assim criar um novo usuario no sistema de auth.
    
    2) Metodo superiro que realiza o login no sistema, depende do metodo de
    abstração chamado logar para realizar a operação no sistema.
    
    3) Metodo para realizar LogOut.
    
    4)Metodo que ira realizar a requisição para um novo 
    refresh token e acessToken para o usuario.
    
    5) Metodo de abstração de criação de um AccessToken e também metodo de gerar novas claims.

    6) Metodo de checagem de usuario existente, verifica se o usuario já existe no banco
    baseando-se em sua chave de acesso.
    
    7) Metodo de abstração de criação de um usuario, irá realizar as operações lógicas
    para criar um novo usuario no banco de dados.
    
    8) Metodo de abstração de login de um usuario, irá realizar as operações lógicas
    para criar login de  um usuario.
    
    9) Metodo de abstração para realizar todas as operações lógicas referentes a gerar
    um novo RefreshToken.

    10) Metodo de abstração para gerar um nova chave de acesso.
    
    11) Metodo de abstração para criar os roles.
*/
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
        readonly IMapper _mapper;
        readonly DataContext _db;
        readonly UserManager<AppUser> _userManager;
        readonly SignInManager<AppUser> _signInManager;
        readonly IConfiguration _config;
        private List<(string, string)> _refreshToken = new ();
        readonly IRepoAuthExtend _refreshTokenService;
        readonly RoleManager<IdentityRole> _roleManager;
        
        public RepoAuth(IMapper mapper, 
        DataContext db, 
        UserManager<AppUser> userManager, 
        SignInManager<AppUser> signInManager, 
        IConfiguration config,
        IRepoAuthExtend refreshTokenService,
        RoleManager<IdentityRole> roleManager)
        {
            _mapper = mapper;
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
            _refreshTokenService = refreshTokenService;
            _roleManager = roleManager;
        }

        
        
        /*
            ==============
            1) Metodo superior para registrar um novo usuario.
            ==============
        */
        public async Task<RegistroDTO> Registrar(UsuarioDTO request)
        {
            var NovoUsuario = new AppUser
            {
                UserName = await GerarChaveDeAcesso(),
                Email = request.Email,
                EmailConfirmed = true,
                Role = request.Role.ToUpper()
            };

            var usuarioNovo = await CriarUsuario(NovoUsuario, request);
            return usuarioNovo;
        }

        /*
            ==============
            2) Metodo superior para logar um usuario.
            ==============
        */
        public async Task<LogadoDTO> Logar(LoginDTO request)
        {
            var result = await _signInManager.PasswordSignInAsync(request.ChaveDeAcesso, request.Senha, false, true);

            if (result.Succeeded) 
            {
                var logado = await LogarUsuario(request);
                return logado;
            }

            var Falha = new LogadoDTO{
                Mensagem = "Não foi possivel realizar o login"
            };
            return Falha;


        }

        /*
            ==============
            3) Metodo de logout.
            ==============
        */
        public async Task<string> LogOut()
        {
            await _signInManager.SignOutAsync();
            return "Usuario deslogado!";
        }


        /*
            ==============
            4) Metodo superior para gerar um refreshToken.
            ==============
        */
        public async Task<LogadoDTO> RefreshToken(RefreshTokenDTO request)
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
            var refreshTokenSalvo = await _refreshTokenService.BuscarRefreshToken(request.ChaveDeAcesso, request.RefreshToken);
            
            if(refreshTokenSalvo.TokenRefresh != request.RefreshToken) throw new SecurityTokenException("Token invalido");
            if (refreshTokenSalvo.ExpiraEm <= DateTime.Now) throw new SecurityTokenException("Token Expirado");
            
            var novoToken = await CriarToken(request.ChaveDeAcesso);
            var novoRefreshToken = CriarNovoRefreshToken(request);

            await _refreshTokenService.DeletarRefreshToken(request.ChaveDeAcesso);
            await _refreshTokenService.SalvarRefreshToken(novoRefreshToken);

            return new LogadoDTO {
                SucessoAoLogar = true,
                ChaveDeAcesso = request.ChaveDeAcesso,
                Token = novoToken,
                RefreshToken = novoRefreshToken.TokenRefresh
            };

        }

        
        /*
            ==============
            5) Metodo para criação de um AccessToken
            ==============
        */
        private async Task<string> CriarToken(string ChaveDeAcesso)
        {

            var usuario = await _userManager.FindByNameAsync(ChaveDeAcesso);
            
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
                new Claim("Role", user.Role),
                new Claim("key", _config["Jwt:Key"])
            };

            return claims;
        }



        /*
            ==============
            6) Metodo de abstração para checar se um usuario existe.
            ==============
        */
    
        private async Task<bool> ChecarUsuario(string ChaveDeAcesso)
        {
            var usuario  = await _userManager.FindByEmailAsync(ChaveDeAcesso);
            if(usuario != null) return true;
            return false;        
        }


        /*
            ==============
            7) Metodo de abstração para criar um usuario no banco de dados.
            ==============
        */
        private async Task<RegistroDTO> CriarUsuario(AppUser NovoUsuario, UsuarioDTO request)
        {
            var result = await _userManager.CreateAsync(NovoUsuario, request.Senha);
            if(result.Succeeded)
            {
                await CriarRoles();
                await _userManager.SetLockoutEnabledAsync(NovoUsuario, false);
                await _userManager.AddToRoleAsync(NovoUsuario, request.Role);     
            }
            if(!result.Succeeded && result.Errors.Count() > 0) Console.WriteLine("Erro");
            
            var registroDTO = new RegistroDTO 
            {
                ChaveDeAcesso = NovoUsuario.UserName,
                Mensagem = "Usuario criado com sucesso!"
            };
            return registroDTO;
        }

    
        /*
            ==============
            8) Metodo de abstração para logar um usuario existente.
            ==============
        */
        private async Task<LogadoDTO> LogarUsuario(LoginDTO request)
        {
            var token = await CriarToken(request.ChaveDeAcesso);
            var refreshToken = _refreshTokenService.GerarRefreshToken(request.ChaveDeAcesso);
            await _refreshTokenService.SalvarRefreshToken(refreshToken);
            var LoginDTO = new LogadoDTO{
                SucessoAoLogar = true,
                ChaveDeAcesso = request.ChaveDeAcesso,
                Token = token,
                RefreshToken = refreshToken.TokenRefresh,
                ExpiraEm = DateTime.Now.AddHours(1),
                Mensagem = "Login realizado com sucesso!"
            };
            return LoginDTO;
        } 
    

        /*
            ==============
            9) Metodo de abstração para gerar um novo RefreshToken.
            ==============
        */
        private RefreshToken CriarNovoRefreshToken(RefreshTokenDTO request)
        {
            var novoRefreshToken = _refreshTokenService.GerarRefreshToken(request.ChaveDeAcesso);
            
            var refreshTokenDTO = new RefreshToken {
                ChaveDeAcesso = request.ChaveDeAcesso,
                TokenRefresh = novoRefreshToken.TokenRefresh,
                CriadoEm = DateTime.UtcNow,
                ExpiraEm = DateTime.UtcNow.AddHours(1)
            };
            return refreshTokenDTO;
        }
    
       
        /*
            ==============
            10) Metodo de abstração para gerar uma chave de acesso.
            ==============
        */
        private async Task<string> GerarChaveDeAcesso()
        {
            var random = new Random(); 
            const string pool = "abcdefghijklmnopqrstuvwxyz0123456789";
            var chars = Enumerable.Range(0, 5).Select(x => pool[random.Next(0, pool.Length)]);
            var chaveRandom = new string(chars.ToArray());

            if(await ChecarUsuario(chaveRandom)) 
            {
                var chave = await GerarChaveDeAcesso();
                return chave;
            }     
            return chaveRandom;
        }
    
        
        /*
            ==============
            11) Metodo de abstração para criar os papeis de usuaros.
                Papeis pré existentes são: Admin e Supervisor.
                Admin será responsavel por tudo e poderá realizar tudo no sistema 
                enquanto o Supervisor tera apenas permissões de leitura.
            ==============
        */
        private async Task CriarRoles()
        {
            if (!_roleManager.RoleExistsAsync("Admin").GetAwaiter().GetResult())
            {
                await _roleManager.CreateAsync(new IdentityRole("ADMIN"));
                await _roleManager.CreateAsync(new IdentityRole("ATENDENTE"));
                await _roleManager.CreateAsync(new IdentityRole("FINANCEIRO"));
            }
        }
    }
}