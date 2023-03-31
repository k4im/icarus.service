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

        public ProjectController(ILogger<ProjectController> logger, HttpClient http)
        {
            _logger = logger;
            _http = http;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {    
                 HttpResponseMessage response =  await _http.GetAsync($"http://localhost:5222/api/v1/Project/projetos");
                 response.EnsureSuccessStatusCode();
                 var responseBody = await response.Content.ReadAsStringAsync();

                 ProjectResponseDTO responseJson = JsonConvert.DeserializeObject<ProjectResponseDTO>(responseBody);
                 return View(responseJson);
            }
            catch(Exception e ){
                 Console.WriteLine(e.Message);
            }
             return BadRequest();
        }

        [HttpGet("Create")]
        public IActionResult Create() {
            return View();
        }
        [HttpPost("Create")]
        public async Task<IActionResult> Create(Project model) 
        {
            
            if(ModelState.IsValid) 
            {
                HttpContent responseBody = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                var response =  _http.PostAsync("http://localhost:5222/api/v1/Project/Create", responseBody).Result;
                if(response.IsSuccessStatusCode) 
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var responseJson = JsonConvert.DeserializeObject<ProjectDTO>(content);
                    TempData["CriadoProjeto"]= "Projeto criado com sucesso.";
                }
                else 
                {
                    return BadRequest();
                }
            }
            return RedirectToAction("Index");
        }

        [HttpGet("Update")]
        public async Task<IActionResult> Update([FromRoute]int? id)
        {   
            HttpResponseMessage response = await _http.GetAsync($"http://localhost:5222/api/v1/Project/projetos/{id}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            Project responseJson = JsonConvert.DeserializeObject<Project>(content);       
            return View(responseJson);
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromForm]Project model)
        {
            if(!ModelState.IsValid) return RedirectToAction("Index");
            
            try 
            {
                HttpResponseMessage response = await _http.PutAsJsonAsync($"http://localhost:5222/api/v1/Project/update/{model.Id}", model);
                response.EnsureSuccessStatusCode();
                var content = response.Content.ReadAsStringAsync();
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            TempData["Updated"] = "Projeto atualizado com sucesso";
            return RedirectToAction("Index");

        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}
