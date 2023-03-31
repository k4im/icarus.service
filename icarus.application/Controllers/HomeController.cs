using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using icarus.application.Models;
using icarus.application.models;
using System.Text.Json;
using System.Net.Http.Json;
using Newtonsoft.Json;
using System.Text;

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

    public async Task<IActionResult> Index() 
    {
        HttpResponseMessage request = await _http.GetAsync("http://localhost:5222/api/v1/Project/projetos");
        request.EnsureSuccessStatusCode();
        var responseBody = await request.Content.ReadAsStringAsync();
        ProjectResponseDTO reponseJson = JsonConvert.DeserializeObject<ProjectResponseDTO>(responseBody);
        ViewBag.projetosFinalizados = reponseJson.Projects.Where(status => status.Status.ToLower().Contains("finalizado") || status.Status.ToLower().Contains("confirmado")).Count();
        ViewBag.projetosPendentes = reponseJson.Projects.Where(status => status.Status.ToLower().Contains("prod")).Count();
        ViewBag.totalProjetos = reponseJson.Projects.Count();
        return View();
    }



}
