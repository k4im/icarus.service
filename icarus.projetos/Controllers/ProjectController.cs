using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using icarus.projetos.AsyncComm;
using icarus.projetos.models;
using icarus.projetos.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace icarus.projetos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    // [Authorize]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectRepository _repo;
        private readonly IMessageBusService _msgBus;
        private readonly IMapper _mapper;

        public ProjectController(IProjectRepository repo, 
        IMessageBusService msgBus, IMapper mapper)
        {
            _repo = repo;
            _msgBus = msgBus;
            _mapper = mapper;
        }

        [HttpGet("projetos/{pagina}")]
        public async Task<IActionResult> GetAllProjects(int pagina = 1) 
        {
            var projetos = await _repo.GetAllProjects(pagina);
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
            
            if(ModelState.IsValid) 
            {
                await _repo.CreateProject(model);
                try 
                {
                    var projeto = _mapper.Map<Project, ProjectDTO>(model);
                    _msgBus.publishNewProjeto(projeto);
                    Console.WriteLine("--> Mensagem enviado para o bus");
                }
                catch(Exception e)
                {
                    Console.WriteLine($"--> Não foi possivel enviar a mensagem para o bus: {e.Message}");
                }
            }
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