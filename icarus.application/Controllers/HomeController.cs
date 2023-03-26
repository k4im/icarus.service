using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using icarus.application.Models;
using icarus.application.models;
using System.Text.Json;
using System.Net.Http.Json;
using Newtonsoft.Json;

namespace icarus.application.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly HttpClient _http;
    public HomeController(ILogger<HomeController> logger, HttpClient http)
    {
        _logger = logger;
        _http = http;
    }


    [HttpGet("{page = 1}")]
    public async Task<IActionResult> Index(int page = 1)
    {
       try
       {    
            HttpResponseMessage response =  await _http.GetAsync($"http://localhost:5222/api/v1/Project/projetos/{page}");
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            
            ProjectResponseDTO projetos = JsonConvert.DeserializeObject<ProjectResponseDTO>(responseBody);
            ViewBag.totalProjetos = projetos;
            return View();
       }
       catch(Exception e ){
            Console.WriteLine(e.Message);
       }
        return BadRequest();
    }
}
