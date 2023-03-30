using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using icarus.projetos.models;
using icarus.projetos.Repository;
using Microsoft.AspNetCore.Mvc;

namespace icarus.projetos.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectRepository _repo;

        public ProjectController(IProjectRepository repo)
        {
            _repo = repo;
        }
        
        [HttpGet("projetos")]
        public async Task<IActionResult> GetAllProjects() 
        {
            var projetos = await _repo.GetAllProjects();
            return Ok(projetos);
        }
    

        [HttpPost("Create")]
        public async Task<IActionResult> CreateProject(Project model)
        {
            
            if(ModelState.IsValid) await _repo.CreateProject(model);
            return StatusCode(201);
        }

        [HttpPost("Sample")]
        public async Task<IActionResult> SampleData()
        {
            List<Project> sampleData = new List<Project> ();
            
            sampleData.Add(new Project {
                Name = "Teste",
                Status = "Finalizado",
                DataIncio = DateTime.Now,
                DataEntrega = DateTime.Now,
                Descricao  = "String",
                Valor = 150
            });
            sampleData.Add(new Project {
                Name = "Teste",
                Status = "Finalizado",
                DataIncio = DateTime.Now,
                DataEntrega = DateTime.Now,
                Descricao  = "String",
                Valor = 150
            });
            sampleData.Add(new Project {
                Name = "Teste",
                Status = "Finalizado",
                DataIncio = DateTime.Now,
                DataEntrega = DateTime.Now,
                Descricao  = "String",
                Valor = 150
            });
            sampleData.Add(new Project {
                Name = "Teste",
                Status = "Produção",
                DataIncio = DateTime.Now,
                DataEntrega = DateTime.Now,
                Descricao  = "String",
                Valor = 150
            });

            sampleData.Add(new Project {
                Name = "Teste",
                Status = "Produção",
                DataIncio = DateTime.Now,
                DataEntrega = DateTime.Now,
                Descricao  = "String",
                Valor = 150
            });
            
            sampleData.Add(new Project {
                Name = "Teste",
                Status = "Produção",
                DataIncio = DateTime.Now,
                DataEntrega = DateTime.Now,
                Descricao  = "String",
                Valor = 150
            });
            
            sampleData.Add(new Project {
                Name = "Teste",
                Status = "Finalizado",
                DataIncio = DateTime.Now,
                DataEntrega = DateTime.Now,
                Descricao  = "String",
                Valor = 150
            });
            
            sampleData.Add(new Project {
                Name = "Teste",
                Status = "Finalizado",
                DataIncio = DateTime.Now,
                DataEntrega = DateTime.Now,
                Descricao  = "String",
                Valor = 150
            });
            
            sampleData.Add(new Project {
                Name = "Teste",
                Status = "Finalizado",
                DataIncio = DateTime.Now,
                DataEntrega = DateTime.Now,
                Descricao  = "String",
                Valor = 150
            });
            sampleData.Add(new Project {
                Name = "Teste",
                Status = "Produção",
                DataIncio = DateTime.Now,
                DataEntrega = DateTime.Now,
                Descricao  = "String",
                Valor = 150
            });            
            sampleData.Add(new Project {
                Name = "Teste",
                Status = "Produção",
                DataIncio = DateTime.Now,
                DataEntrega = DateTime.Now,
                Descricao  = "String",
                Valor = 150
            });
            sampleData.Add(new Project {
                Name = "Teste",
                Status = "Produção",
                DataIncio = DateTime.Now,
                DataEntrega = DateTime.Now,
                Descricao  = "String",
                Valor = 150
            });
            sampleData.Add(new Project {
                Name = "Teste",
                Status = "Produção",
                DataIncio = DateTime.Now,
                DataEntrega = DateTime.Now,
                Descricao  = "String",
                Valor = 150
            });
            sampleData.Add(new Project {
                Name = "Teste",
                Status = "Finalizado",
                DataIncio = DateTime.Now,
                DataEntrega = DateTime.Now,
                Descricao  = "String",
                Valor = 150
            });                                    
            foreach (var item in sampleData)
            {
                await _repo.CreateProject(item);
            }
            // if(model != null) await _repo.CreateProject(model);
            return StatusCode(201);
        }


        [HttpPatch("update/{id}")]
        public IActionResult UpdateProject(Project model, int id)
        {
            if(model != null) _repo.UpdateProject(model, id);
            return Ok();
        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteProject(int id)
        {
            _repo.DeleteProject(id);
            return StatusCode(204);
        }
    }
}