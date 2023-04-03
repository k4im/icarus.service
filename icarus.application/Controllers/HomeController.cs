/*
    Controlador da Home, onde o mesmo irá realizar uma contagem de todos os projeto que existem dentro do banco de dados do micro serviço
    sendo assim, será realizado apenas uma unica request para api, onde a mesma irá retornar todos projetos existentes para então
    realizar a filtragem de cada campo em especifico e ser passado para as ViewBags.
*/

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
    public string url { get; set; }

    public HomeController(ILogger<HomeController> logger, HttpClient http)
    {
        _logger = logger;
        _http = http;
        url = "http://localhost:5222/api/v1/Project";
    }

    /*
        =============================================================
        |metodo da index de home consulta o serviço e realiza uma   |
        |filtragem utilizando os campos que é desejado para         |
        |apresentação ao cliente a home page do dashboard.          |
        =============================================================
    */
    public async Task<IActionResult> Index() 
    {
        HttpResponseMessage request = await _http.GetAsync($"{url}/projetos");
        request.EnsureSuccessStatusCode();
        var responseBody = await request.Content.ReadAsStringAsync();
        ProjectResponseDTO reponseJson = JsonConvert.DeserializeObject<ProjectResponseDTO>(responseBody);
        ViewBag.projetosFinalizados = reponseJson.Projects.Where(status => status.Status.ToLower().Contains("finalizado") || status.Status.ToLower().Contains("confirmado")).Count();
        ViewBag.projetosPendentes = reponseJson.Projects.Where(status => status.Status.ToLower().Contains("prod")).Count();
        ViewBag.totalProjetos = reponseJson.Projects.Count();
        return View();
    }



}
