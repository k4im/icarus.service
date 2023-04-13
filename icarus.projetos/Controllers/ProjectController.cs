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
    [Route("api/[controller]")]
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
        
        [HttpGet("projeto/{id?}")]
        public async Task<IActionResult> GetById(int? id) 
        {
            var item = await _repo.GetById(id);
            if(item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateProject(Project model)
        {
            
            if(ModelState.IsValid) await _repo.CreateProject(model);
            return StatusCode(201);
        }


        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateProject([FromBody]ProjectUpdate model, [FromRoute]int? id)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState); 
            if(id == null) return NotFound();
            try 
            {
                await _repo.UpdateProject(model, id);
                return Ok();
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
            return BadRequest(); 
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteProject(int? id)
        {
            if(id == null) return NotFound();
            await _repo.DeleteProject(id);
            return StatusCode(204);
        }
    

    }
}