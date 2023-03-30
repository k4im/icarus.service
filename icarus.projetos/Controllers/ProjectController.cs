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
        public async Task<IActionResult> GetAllProjects(int page = 1) 
        {
            var projetos = await _repo.GetAllProjects();
            return Ok(projetos);
        }
        [HttpGet("projetos/{page}")]
        public async Task<IActionResult> GetProjects(int page = 1) 
        {
            var projetos = await _repo.GetProjects(page);
            return Ok(projetos);
        }

        [HttpGet("{SearchFilter}")]
        public async Task<IActionResult> GetProjectsFilter(string SearchFilter) 
        {
            var projetos = await _repo.GetProjectsFilter(SearchFilter);
            return Ok(projetos);
        }
    
    
        [HttpGet("{SearchFilter}/{page}")]
        public async Task<IActionResult> GetProjectsFilter(string SearchFilter, int page = 1) 
        {
            var projetos = await _repo.GetProjectsFilterPagination(SearchFilter, page);
            return Ok(projetos);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateProject(Project model)
        {
            if(model != null) await _repo.CreateProject(model);
            return StatusCode(201);
        }


        [HttpPatch("update/{id}")]
        public IActionResult UpdateProject(Project model, int id)
        {
            if(model != null) _repo.UpdateProject(model, id);
            return Ok();
        }

        [HttpDelete("update/{id}")]
        public IActionResult UpdateProject(int id)
        {
            _repo.DeleteProject(id);
            return StatusCode(204);
        }
    }
}