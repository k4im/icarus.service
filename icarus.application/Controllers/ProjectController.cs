/*
    Controlador de projetos irá realizar todas as interações com o micro serviço de projetos 
    sendo assim, estará acessando todos os endpoints da api que são necessarios para que a aba de projetos funcione.
*/
using System.Text;
using icarus.application.models;
using icarus.application.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace icarus.application.Controllers
{
    [Route("[controller]")]
    public class ProjectController : Controller
    {
        private readonly ILogger<ProjectController> _logger;
        private readonly HttpClient _http;
        public string url { get; set; }
        public ProjectController(ILogger<ProjectController> logger, HttpClient http)
        {
            _logger = logger;
            _http = http;
            url = "http://localhost:5222/api/v1/Project";
        }
        
        /*
            =============================================================
            |metodo da index onde estará realizando uma requisição      |
            |para o serviço onde o mesmo irá retornar todos os serviços |
            |que existem dentro do banco de dados.                      |
            =============================================================
        */
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {    //Requisição ao serviço
                 HttpResponseMessage response =  await _http.GetAsync($"{url}/projetos");
                 //Verficando que o status code que foi retornado é valido
                 response.EnsureSuccessStatusCode();
                 //realizando a leitura do reponse para string
                 var responseBody = await response.Content.ReadAsStringAsync();
                 //Conversão do conteúdo da resposta em um DTO.
                 ProjectResponseDTO responseJson = JsonConvert.DeserializeObject<ProjectResponseDTO>(responseBody);
                 return View(responseJson);
            }
            catch(Exception e ){
                 Console.WriteLine(e.Message);
            }
             return BadRequest();
        }

        /*
            =============================================================
            |metodo create envia um modelo para o serviço para que este |
            |seja criado no banco de dados com os campos validados tanto|
            |no lado do servidor como no lado do cliente.               |
            =============================================================
        */
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody]Project model) 
        {
            
            if(ModelState.IsValid) 
            {   
                //Criando um conteudo serializado em json com o modelo que foi repassado.
                HttpContent responseBody = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                //Realizando o post no serviço
                var response =  _http.PostAsync($"{url}/Create", responseBody).Result;
                if(response.IsSuccessStatusCode) 
                {   
                    //Lendo o conteudo como string
                    var content = await response.Content.ReadAsStringAsync();
                    //convertendo esse conteudo em um dto
                    var responseJson = JsonConvert.DeserializeObject<ProjectDTO>(content);
                    //Enviando um dado temporario para a view
                    TempData["CriadoProjeto"]= "Projeto criado com sucesso.";
                }
                else 
                {
                    return BadRequest();
                }
            }
            return RedirectToAction("Index");
        }
        /*
            =============================================================
            |metodo update realiza uma consulta com o id do projeto     |
            |e retorna os dados do projeto para a modal para que então  |
            |seja possivel atualizar o seu status.                      |
            =============================================================
        */
        [HttpGet("Update")]
        public async Task<IActionResult> Update(int? id)
        {   
            //Requisição para o serviço com o id do projeto em questão
            HttpResponseMessage response = await _http.GetAsync($"{url}/projeto/{id}");
            // Verificando que o status code é aceitavel
            response.EnsureSuccessStatusCode();
            //Leitura do conteudo em string
            var content = response.Content.ReadAsStringAsync().Result;  
            //Conversão do conteudo para projeto
            var responseJson = JsonConvert.DeserializeObject<Project>(content);
            //Enviando para um view parcial com o conteudo convertido em projeto     
            return PartialView("_Update", responseJson);
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update(ProjectUpdate model)
        {
            //Verificando que o conteudo é valido
            if(!ModelState.IsValid) return RedirectToAction("Index");
            
            try 
            {   //Criando um model com o conteudo repassado na requisição
                var modelUpddated = new ProjectUpdate {
                    Id = model.Id,
                    Name = model.Name,
                    Status = model.Status,
                    DataIncio = model.DataIncio,
                    DataEntrega = model.DataEntrega,
                    Descricao = model.Descricao,
                    Valor = model.Valor
                };
                
                //Serializando o conteudo em JSON
                var json = JsonConvert.SerializeObject(modelUpddated);
                //transformando o json em um conteudo de string
                var requestContent = new StringContent(json, Encoding.UTF8, "application/json");
                // realizando o update no serviço, repassando o id do projeto e o conteudo em string 
                HttpResponseMessage response = await _http.PutAsync($"{url}/update/{model.Id}", requestContent);
                //verificando o status code 
                response.EnsureSuccessStatusCode();
                // realizando a leitura do conteudo que foi respondido e enviando um dado temporario para a view.
                var content = response.Content.ReadAsStringAsync();
                TempData["Updated"] = "Projeto atualizado com sucesso";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return RedirectToAction("Index");

        }
        
        /*
            =============================================================
            |metodo delete realiza uma exclusão de um projeto no serviço|
            =============================================================
        */        
        [HttpPost("Delete")]
        public async Task<IActionResult> Delete(int? id) 
        {
            //Requisicao para o serviço
            var responseDelete = await _http.DeleteAsync($"{url}/delete/{id}");
            //Verificação da resposta
            responseDelete.EnsureSuccessStatusCode();
            //dado temporario para view
            TempData["DeletedMessage"] = "Projeto deletado com sucesso";
            return RedirectToAction("Index");

        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}
