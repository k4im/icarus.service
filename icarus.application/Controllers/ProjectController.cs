using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using icarus.application.models;
using icarus.application.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
                    Console.WriteLine("Data Saved Successfully.");
                    RedirectToAction("Index");
                }
                else 
                {
                    return BadRequest();
                }
            }
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}
