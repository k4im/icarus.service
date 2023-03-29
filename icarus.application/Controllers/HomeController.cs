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
    public string LastSearchText { get; private set; }
    public HomeController(ILogger<HomeController> logger, HttpClient http)
    {
        _logger = logger;
        _http = http;
    }


    [HttpGet]
    [Route("{page = 1}")]
    [Route("/")]
    [Route("Index/{page = 1}")]
    public async Task<IActionResult> Index(int page = 1)
    {
       try
       {    
            HttpResponseMessage response =  await _http.GetAsync($"http://localhost:5222/api/v1/Project/projetos/{page}");
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

    [HttpPost("Index")]
    public async Task<IActionResult> Index(string search, int page = 1) 
    {   
        if(!String.IsNullOrEmpty(search))  
        {
            // var searchString = Request.["search"];
            LastSearchText = search;
        }
        else {
            HttpResponseMessage response =  await _http.GetAsync($"http://localhost:5222/api/v1/Project/projetos/1");
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            
            ProjectResponseDTO responseJson = JsonConvert.DeserializeObject<ProjectResponseDTO>(responseBody);
            return View(responseJson);
        }
        try 
        {
            HttpResponseMessage  reponse = await _http.GetAsync($"http://localhost:5222/api/v1/Project/{LastSearchText}");
            reponse.EnsureSuccessStatusCode();
            var responseBody = await reponse.Content.ReadAsStringAsync();
            ProjectResponseDTO responseJson = JsonConvert.DeserializeObject<ProjectResponseDTO>(responseBody);
            return View(responseJson);
            
        }
        catch(Exception e) 
        {
            Console.WriteLine(e);
            // _logger.LogError();
        }
        return BadRequest();
    }

}
